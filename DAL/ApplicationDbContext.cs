using System.Data.Entity;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL
{
    public class ApplicationDbContext : Models.IdentityDbContext
    {
        public ApplicationDbContext() : base("ArtGalleryDB") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Art> Arts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<FavArt> FavArts { get; set; }
        public DbSet<UserLikes> UserLikes { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}