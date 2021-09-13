using System;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ArtGalleryDbContext context = new ArtGalleryDbContext();
        private GenericRepository<Art> artRepository;
        private GenericRepository<Author> authorRepository;
        private bool disposed = false;

        public GenericRepository<Art> ArtRepository
        {
            get
            {
                if (this.artRepository == null)
                {
                    this.artRepository = new GenericRepository<Art>(context);
                }
                return artRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (this.authorRepository == null)
                {
                    this.authorRepository = new GenericRepository<Author>(context);
                }
                return authorRepository;
            }
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
            if (!this.disposed)
            {
                if (dispoing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}