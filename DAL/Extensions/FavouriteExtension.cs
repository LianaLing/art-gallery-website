using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class FavouriteExtension
    {
        public static IEnumerable<FavDTO> GetUserFavourites(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            return (from art in dbContext.Arts
                    join author in dbContext.Authors on art.AuthorId equals author.Id
                    join favArt in dbContext.FavArts on art.Id equals favArt.ArtId
                    join fav in dbContext.Favourites on favArt.FavId equals fav.Id
                    join user in dbContext.Users on fav.UserId equals user.Id
                    where user.Id == user_id
                    orderby fav.Id ascending
                    select new
                    {
                        Id = fav.Id,
                        Name = fav.Name,
                        Art = new
                        {
                            Id = art.Id,
                            Style = art.Style,
                            Description = art.Description,
                            Price = art.Price,
                            Stock = art.Stock,
                            Likes = art.Likes,
                            Url = art.Url
                        },
                        Author = new
                        {
                            Id = author.Id,
                            Description = author.Description,
                            Verified = author.Verified
                        }
                    })
                    .ToList()
                    .Select(x => new FavDTO { 
                        Id = x.Id,
                        Name = x.Name,
                        Art = new Art
                        {
                            Id = x.Art.Id,
                            Style = x.Art.Style,
                            Description = x.Art.Description,
                            Price = x.Art.Price,
                            Stock = x.Art.Stock,
                            Likes = x.Art.Likes,
                            Url = x.Art.Url
                        },
                        Author = new Author
                        {
                            Id = x.Author.Id,
                            Description = x.Author.Description,
                            Verified = x.Author.Verified
                        }
                    });
        }

        public static List<ArtCountInFavDTO> ArtCountInUserFavourites(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            return dbContext.Users
                .Where(user => user.Id == user_id)
                .Join(
                    dbContext.Favourites,
                    user => user.Id,
                    fav => fav.UserId,
                    (user, fav) => fav.Id
                )
                .Join(
                    dbContext.FavArts,
                    favId => favId,
                    favArt => favArt.FavId,
                    (favId, favArt) => new { art_id = favArt.ArtId, fav_id = favArt.FavId }
                )
                .GroupBy(res => res.fav_id)
                .Select(res => new ArtCountInFavDTO { FavId = res.Key, TotalArt = res.Count() })
                .ToList();
        }
    }
}