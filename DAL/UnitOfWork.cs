using System;
using System.Data.Entity;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Art> artRepository;
        private GenericRepository<Author> authorRepository;
        private GenericRepository<ApplicationUser> userRepository;
        private GenericRepository<Favourite> favouriteRepository;
        private GenericRepository<FavArt> favArtRepository;
        private GenericRepository<UserLikes> userLikesRepository;
        private GenericRepository<ShoppingCart> shoppingCartRepository;
        private GenericRepository<CartItem> cartItemRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<BillingDetails> billingDetailsRepository;
        private GenericRepository<Address> addressRepository;
        private GenericRepository<Card> cardRepository;
        private GenericRepository<PaymentMethod> paymentMethodRepository;
        private GenericRepository<Payment> paymentRepository;
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

        public GenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new GenericRepository<ApplicationUser>(context);
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

        public GenericRepository<ShoppingCart> ShoppingCartRepository
        {
            get
            {
                if (shoppingCartRepository == null) shoppingCartRepository = new GenericRepository<ShoppingCart>(context);
                return shoppingCartRepository;
            }
        }

        public GenericRepository<CartItem> CartItemRepository
        {
            get
            {
                if (cartItemRepository == null) cartItemRepository = new GenericRepository<CartItem>(context);
                return cartItemRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null) orderRepository = new GenericRepository<Order>(context);
                return orderRepository;
            }
        }

        public GenericRepository<BillingDetails> BillingDetailsRepository
        {
            get
            {
                if (billingDetailsRepository == null) billingDetailsRepository = new GenericRepository<BillingDetails>(context);
                return billingDetailsRepository;
            }
        }

        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (addressRepository == null) addressRepository = new GenericRepository<Address>(context);
                return addressRepository;
            }
        }

        public GenericRepository<Card> CardRepository
        {
            get
            {
                if (cardRepository == null) cardRepository = new GenericRepository<Card>(context);
                return cardRepository;
            }
        }

        public GenericRepository<PaymentMethod> PaymentMethodRepository
        {
            get
            {
                if (paymentMethodRepository == null) paymentMethodRepository = new GenericRepository<PaymentMethod>(context);
                return paymentMethodRepository;
            }
        }

        public GenericRepository<Payment> PaymentRepository
        {
            get
            {
                if (paymentRepository == null) paymentRepository = new GenericRepository<Payment>(context);
                return paymentRepository;
            }
        }

        public DbContext GetContext()
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