using ArtGalleryWebsite.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class PaymentExtension
    {
        public class TransactionParams
        {
            public string Remark { get; set; }
            public AddressDTO ShippingAddress { get; set; }
            public PaymenMethodDTO PaymenMethod { get; set; }
            public List<CartItemDTO> CartItems { get; set; }
        }

        public static void CreateTransaction(this UnitOfWork unitOfWork, int user_id)
        {

        }

        //public static void CreatePaymentMethod(this UnitOfWork unitOfWork, )
    }
}