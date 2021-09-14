using System;
using System.Collections.Generic;
using System.Linq;
using ArtGalleryWebsite.Models;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class ArtsExtension
    {
        public static ArtDetailDTO GetArtDetailById(this UnitOfWork unitOfWork, int art_id)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            return dbContext.Arts
                .Where(art => art.Id == art_id)
                .Join(
                    dbContext.Users,
                    art => art.AuthorId,
                    user => user.AuthorId,
                    (art, user) => new ArtDetailDTO
                    {
                        Id = art.Id,
                        Style = art.Style,
                        Description = art.Description,
                        Price = art.Price,
                        Stock = art.Stock,
                        Likes = art.Likes,
                        Url = art.Url,
                        Author = new ArtDetailDTO.ArtDetailDTOAuthor
                        {
                            Id = art.Author.Id,
                            Description = art.Author.Description,
                            Verified = art.Author.Verified,
                            Username = user.UserName,
                            Name = user.Name,
                            Ic = user.Ic,
                            Dob = user.Dob,
                            ContactNo = user.PhoneNumber,
                            Email = user.Email,
                            AvatarUrl = user.AvatarUrl
                        }
                    }
                )
                .FirstOrDefault();
        }

        public static List<ArtDetailDTO> GetArtDetails(this UnitOfWork unitOfWork, int skip = 0, int limit = 100)
        {
            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            return dbContext.Arts
                .Join(
                    dbContext.Authors,
                    art => art.AuthorId,
                    author => author.Id,
                    (art, author) => new { art = art, author = author }
                )
                .Join(
                    dbContext.Users,
                    a => a.author.Id,
                    user => user.AuthorId,
                    (a, user) => new ArtDetailDTO
                    {
                        Id = a.art.Id,
                        Style = a.art.Style,
                        Description = a.art.Description,
                        Price = a.art.Price,
                        Stock = a.art.Stock,
                        Likes = a.art.Likes,
                        Url = a.art.Url,
                        Author = new ArtDetailDTO.ArtDetailDTOAuthor
                        {
                            Id = a.author.Id,
                            Description = a.author.Description,
                            Verified = a.author.Verified,
                            Username = user.UserName,
                            Name = user.Name,
                            Ic = user.Ic,
                            Dob = user.Dob,
                            ContactNo = user.PhoneNumber,
                            Email = user.Email,
                            AvatarUrl = user.AvatarUrl
                        }
                    }
                )
                .OrderByDescending(artDetailQuery => artDetailQuery.Likes)
                .Skip(skip)
                .Take(limit)
                .ToList();
        }
    }
}