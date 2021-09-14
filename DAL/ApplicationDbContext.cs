using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.Models.Identity;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entities to respective tables
            modelBuilder.Entity<Models.Identity.User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<Art>().ToTable("Art");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Favourite>().ToTable("Favourite");
            modelBuilder.Entity<FavArt>().ToTable("FavArt");
            modelBuilder.Entity<UserLikes>().ToTable("UserLikes");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");

            // Set auto-increment properties
            modelBuilder.Entity<Models.Identity.User>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Role>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Art>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Author>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Favourite>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ShoppingCart>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CartItem>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}