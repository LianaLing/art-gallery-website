using System;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ArtGalleryDbContext context = new ArtGalleryDbContext();
        private GenericRepository<Art> artRepository;
        private GenericRepository<Author> authorRepository;
        private GenericRepository<Models.Identity.User> userRepository;
        private GenericRepository<Favourite> favouriteRepository;
        private GenericRepository<FavArt> favArtRepository;
        private GenericRepository<UserLikes> userLikesRepository;
        private bool disposed = false;

        public GenericRepository<Art> ArtRepository
        {
            get
            {
                if (artRepository == null) artRepository = new GenericRepository<Art>(context);
                return artRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (authorRepository == null) authorRepository = new GenericRepository<Author>(context);
                return authorRepository;
            }
        }

        public GenericRepository<Models.Identity.User> UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new GenericRepository<Models.Identity.User>(context);
                return userRepository;
            }
        }

        public GenericRepository<Favourite> FavouriteRepository
        {
            get
            {
                if (favouriteRepository == null) favouriteRepository = new GenericRepository<Favourite>(context);
                return favouriteRepository;
            }
        }

        public GenericRepository<FavArt> FavArtRepository
        {
            get
            {
                if (favArtRepository == null) favArtRepository = new GenericRepository<FavArt>(context);
                return favArtRepository;
            }
        }
        public GenericRepository<UserLikes> UserLikesRepository
        {
            get
            {
                if (userLikesRepository == null) userLikesRepository = new GenericRepository<UserLikes>(context);
                return userLikesRepository;
            }
        }

        public ArtGalleryDbContext GetContext()
        {
            return context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispoing)
        {
            if (!disposed)
            {
                if (dispoing) context.Dispose();
            }

            disposed = true;
        }
    }
}