using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class OrderExtension
    {
        public static void GetOrderDetailsByUserId(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

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

            foreach(var d in data)
            {
                System.Diagnostics.Trace.WriteLine(d);
            }
        }
    }
}
