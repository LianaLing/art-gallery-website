using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class FavouriteExtension
    {
        public static List<FavDTO> GetUserFavourites(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            // Fetch favourite with populated arts
            IEnumerable<FavDTO> populatedFav = (from art in dbContext.Arts
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
                    .Select(x => new FavDTO
                    {
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

            // Fetch all Favourite
            IEnumerable<Favourite> favs = unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == user_id, orderBy: fav => fav.OrderBy(f => f.UserId));
            // Convert all Favourite to FavDTO
            IEnumerable<FavDTO> converted = favs.Select(fav => new FavDTO { Id = fav.Id, Name = fav.Name });

            // Merge two lists to return a full list of favourites (remove duplicates)
            return populatedFav
                .Concat(converted)
                .GroupBy(fav => fav.Id)
                .Select(group => group.First())
                .ToList();
        }

        public static List<ArtCountInFavDTO> ArtCountInUserFavourites(this UnitOfWork unitOfWork, int user_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            // Fetch favourite count with populated arts
            IEnumerable<ArtCountInFavDTO> populatedFavCount = dbContext.Users
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
                .AsEnumerable();

            // Fetch all Favourite
            IEnumerable<Favourite> favs = unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == user_id, orderBy: fav => fav.OrderBy(f => f.UserId));
            // Convert all Favourite to ArtCount of 0
            IEnumerable<ArtCountInFavDTO> converted = favs.Select(fav => new ArtCountInFavDTO { FavId = fav.Id, TotalArt = 0 });

            // Merge two lists to return a full list of favourites (remove duplicates)
            return populatedFavCount
                .Concat(converted)
                .GroupBy(favCount => favCount.FavId)
                .Select(group => group.First())
                .ToList();
        }
    }
}