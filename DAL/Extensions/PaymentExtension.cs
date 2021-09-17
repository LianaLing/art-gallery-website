using System;
using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class PaymentExtension
    {
        public class TransactionParams
        {
            public string Remark { get; set; }
            public string Currency { get; set; }
            public string PaymentDescription { get; set; }
            public decimal TaxRate { get; set; }
            public AddressDTO ShippingAddress { get; set; }
            public PaymentMethodDTO PaymentMethod { get; set; }
            public List<CartItemDTO> CartItems { get; set; }
        }

        public static Order CreateTransaction(this UnitOfWork unitOfWork, int user_id, TransactionParams param)
        {
            // Create order
            Order order = new Order
            {
                Status = "processing",
                Remark = param.Remark,
                UserId = user_id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            order = unitOfWork.OrderRepository.Insert(order);
            if (order == null)
                throw new Exception($"Unable to create order for User {user_id}");

            unitOfWork.Save();

            // Create corresponding order 
            // Count quantity in a dictionary
            Dictionary<int, int> quantity = new Dictionary<int, int>();

            foreach (var ci in param.CartItems)
            {
                if (quantity.ContainsKey(ci.ArtId))
                    quantity[ci.ArtId]++;
                else
                    quantity[ci.ArtId] = 1;
            }

            // Merge the list 
            List<CartItem> cartItems = param.CartItems
                .GroupBy(ci => ci.ArtId)
                .Select(group => new CartItem 
                    { 
                        ArtId = group.Key, 
                        CartId = group.First().CartId, 
                        Quantity = quantity[group.Key]
                    }
                )
                .ToList();

            // Then create the orderArt
            foreach (var ci in cartItems)
            {
                OrderArt res = unitOfWork.OrderArtRepository.Insert(
                    new OrderArt { ArtId = ci.ArtId, OrderId = order.Id, Quantity = ci.Quantity }
                );

                if (res == null) throw new Exception($"Unable to insert OrderArt for Art {ci.ArtId} and Order {order.Id}");
            }

            // Create address
            Address address = new Address
            {
                Country = param.ShippingAddress.Country,
                City = param.ShippingAddress.City,
                State = param.ShippingAddress.State,
                Line1 = param.ShippingAddress.Line1,
                Line2 = param.ShippingAddress.Line2,
                PostalCode = param.ShippingAddress.PostalCode,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            address = unitOfWork.AddressRepository.Insert(address);
            if (address == null) throw new Exception($"Unable to create address for User {user_id}");

            unitOfWork.Save();

            // Create billing details
            BillingDetails billingDetails = new BillingDetails
            {
                Name = param.PaymentMethod.BillingDetails.Name,
                Email = param.PaymentMethod.BillingDetails.Email,
                Phone = param.PaymentMethod.BillingDetails.Phone,
                AddressId = address.Id,
                Address = address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            billingDetails = unitOfWork.BillingDetailsRepository.Insert(billingDetails);
            if (billingDetails == null) throw new Exception($"Unable to create billing details for User {user_id}");

            unitOfWork.Save();

            // Create card
            Card card = new Card
            {
                Brand = param.PaymentMethod.Card.Brand,
                Name = param.PaymentMethod.Card.Name,
                Last4 = param.PaymentMethod.Card.Last4,
                ExpYear = param.PaymentMethod.Card.ExpYear,
                ExpMonth = param.PaymentMethod.Card.ExpMonth,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            card = unitOfWork.CardRepository.Insert(card);
            if (card == null) throw new Exception($"Unable to create card for User {user_id}");

            unitOfWork.Save();

            // Create payment method
            PaymentMethod paymentMethod = new PaymentMethod
            {
                Type = param.PaymentMethod.Type,
                CardId = card.Id,
                Card = card,
                UserId = user_id,
                BillingDetailsId = billingDetails.Id,
                BillingDetails = billingDetails,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            paymentMethod = unitOfWork.PaymentMethodRepository.Insert(paymentMethod);
            if (paymentMethod == null) throw new Exception($"Unable to create payment method for User {user_id}");

            unitOfWork.Save();

            // Create payment
            Payment payment = new Payment
            {
                Amount = param.CartItems.Sum(c => c.Art.Price),
                Currency = param.Currency,
                Description = param.PaymentDescription,
                Status = "processing",
                Tax = param.CartItems.Sum(c => c.Art.Price) * param.TaxRate,
                PaymentMethodId = paymentMethod.Id,
                PaymentMethod = paymentMethod,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            payment = unitOfWork.PaymentRepository.Insert(payment);
            if (payment == null) throw new Exception($"Unable to create payment for User {user_id}");

            unitOfWork.Save();

            // Update order
            order.Address = address;
            order.AddressId = address.Id;
            order.PaymentId = payment.Id;
            unitOfWork.OrderRepository.Update(order);

            // Save transaction
            unitOfWork.Save();

            return order;
        }

        public static void ConfirmPaymentAndOrder(this UnitOfWork unitOfWork, int order_id)
        {
            // Get the order and payment
            Order order = unitOfWork.OrderRepository.GetById(order_id);
            if (order == null) throw new Exception($"Unable to fetch Order {order_id}");

            Payment payment = unitOfWork.PaymentRepository.GetById(order.PaymentId);
            if (payment == null) throw new Exception($"Unable to fetch Payment {order.PaymentId}");

            // Update them
            payment.Status = "succeeded";
            payment.SucceededAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;
            order.Status = "succeeded";
            order.UpdatedAt = DateTime.Now;

            unitOfWork.PaymentRepository.Update(payment);
            unitOfWork.OrderRepository.Update(order);

            // Save transaction
            unitOfWork.Save();
        }

        public static void CancelPaymentAndOrder(this UnitOfWork unitOfWork, int order_id)
        {
            // Get the order and payment
            Order order = unitOfWork.OrderRepository.GetById(order_id);
            if (order == null) throw new Exception($"Unable to fetch Order {order_id}");

            Payment payment = unitOfWork.PaymentRepository.GetById(order.PaymentId);
            if (payment == null) throw new Exception($"Unable to fetch Payment {order.PaymentId}");

            // Update them
            payment.Status = "canceled";
            payment.SucceededAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;
            order.Status = "canceled";
            order.UpdatedAt = DateTime.Now;

            unitOfWork.PaymentRepository.Update(payment);
            unitOfWork.OrderRepository.Update(order);

            // Save transaction
            unitOfWork.Save();
        }

        //public static void CreatePaymentMethod(this UnitOfWork unitOfWork, )
    }
}