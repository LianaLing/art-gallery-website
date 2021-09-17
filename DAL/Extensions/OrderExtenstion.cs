using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.DTO;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class OrderExtension
    {
        public static List<OrderDetailDTO> GetOrderDetailsByUserId(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            // Fetch the order details with Art from db
            var data = dbContext.Orders
                .Where(order => order.UserId == user_id)
                .Join(
                    dbContext.OrderArts,
                    order => order.Id,
                    orderArt => orderArt.OrderId,
                    (order, orderArt) => new { order = order, orderArt = orderArt }
                )
                .Join(
                    dbContext.Arts,
                    a => a.orderArt.ArtId,
                    art => art.Id,
                    (a, art) => new { 
                        Id = a.order.Id,
                        Status = a.order.Status,
                        Remark = a.order.Remark,
                        CreatedAt = a.order.CreatedAt,
                        UpdatedAt = a.order.UpdatedAt,
                        PaymentId = a.order.PaymentId,
                        AddressId = a.order.AddressId,
                        Address = new OrderDetailDTO.OrderDetailDTOAddress 
                        {
                            Id = a.order.Address.Id,
                            City = a.order.Address.City,
                            Country = a.order.Address.Country,
                            Line1 = a.order.Address.Line1,
                            Line2 = a.order.Address.Line2,
                            PostalCode = a.order.Address.PostalCode,
                            State = a.order.Address.State,
                            CreatedAt = a.order.Address.CreatedAt,
                            UpdatedAt = a.order.Address.UpdatedAt,
                        },
                        UserId = a.order.UserId,
                        Art = new ArtDetailDTO { 
                            Id = art.Id,
                            Description = art.Description,
                            Likes = art.Likes,
                            Price = art.Price,
                            Stock = art.Stock,
                            Url = art.Url,
                            Style = art.Style,
                            Author = new ArtDetailDTO.ArtDetailDTOAuthor
                            {
                                Id = (int)art.AuthorId,
                                Description = art.Author.Description,
                                Verified = art.Author.Verified
                            }
                        } 
                    }
                )
                .ToList();

            // Construct a dictionary to filter data
            Dictionary<int, KeyValuePair<OrderDetailDTO, List<ArtDetailDTO>>> dict = new Dictionary<int, KeyValuePair<OrderDetailDTO, List<ArtDetailDTO>>>();

            // Put art into its own order (non-repeating)
            foreach(var d in data)
            {
                if (dict.ContainsKey(d.Id))
                    dict[d.Id].Value.Add(d.Art);
                else
                    dict[d.Id] = new KeyValuePair<OrderDetailDTO, List<ArtDetailDTO>>(
                        new OrderDetailDTO
                        {
                            Id = d.Id,
                            Status = d.Status,
                            Remark = d.Remark,
                            CreatedAt = d.CreatedAt,
                            UpdatedAt = d.UpdatedAt,
                            PaymentId = d.PaymentId,
                            AddressId = d.AddressId,
                            Address = new OrderDetailDTO.OrderDetailDTOAddress
                            {
                                Id = d.Address.Id,
                                City = d.Address.City,
                                Country = d.Address.Country,
                                Line1 = d.Address.Line1,
                                Line2 = d.Address.Line2,
                                PostalCode = d.Address.PostalCode,
                                State = d.Address.State,
                                CreatedAt = d.Address.CreatedAt,
                                UpdatedAt = d.Address.UpdatedAt,
                            },
                            UserId = d.UserId,
                            Arts = null
                        }, new List<ArtDetailDTO> { d.Art }
                        );
            }

            // Construct the result as a list
            List<OrderDetailDTO> result = new List<OrderDetailDTO>();

            foreach (var d in dict)
            {
                OrderDetailDTO toAdd = d.Value.Key;
                toAdd.Arts = d.Value.Value;
                result.Add(toAdd);
            }

            return result;
        }

        public static OrderDetailDTO GetOrderDetail(this UnitOfWork unitOfWork, int user_id, int order_id)
        {
            List<OrderDetailDTO> orders = unitOfWork.GetOrderDetailsByUserId(user_id);

            foreach( var order in orders)
            {
                System.Diagnostics.Trace.WriteLine(order.Id);
            }

            return orders
                .Where(res => res.Id == order_id)
                .FirstOrDefault();
        }
    }
}
