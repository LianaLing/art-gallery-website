using System.Data.Entity;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL
{
    public class ArtGalleryDbContext : ApplicationDbContext
    {
        public ArtGalleryDbContext() : base() { }

        public DbSet<Art> Arts { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}