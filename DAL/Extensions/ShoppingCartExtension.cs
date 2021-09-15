using System;
using System.Linq;
using System.Collections.Generic;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.Models.DTO;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class ShoppingCartExtension
    {
        public static ShoppingCart AddArtToShoppingCart(this UnitOfWork unitOfWork, int cart_id, int art_id, int quantity = 1)
        {
            // Check if art exists and has stock
            Art art = unitOfWork.ArtRepository.GetById(art_id);
            if (art == null) throw new Exception($"Art {art_id} does not exist");
            if (art.Stock <= 0) throw new Exception($"Art {art_id} has no stock left");

            // Check if cart exists
            ShoppingCart cart = unitOfWork.ShoppingCartRepository.GetById(cart_id);
            if (cart == null) throw new Exception($"Cart {cart_id} does not exist");

            // Create the cart item
            CartItem cartItem = new CartItem { Quantity = quantity, ArtId = art.Id, CartId = cart.Id };

            // Insert the new cart item
            if (unitOfWork.CartItemRepository.Insert(cartItem) == null)
                throw new Exception($"Unable to add Art {art_id} to Cart {cart_id} (Can't insert into CartItem)");

            // Update the cart total
            cart.Total += 1;
            unitOfWork.ShoppingCartRepository.Update(cart);

            // Save transaction
            unitOfWork.Save();

            return cart;
        }

        public static ShoppingCart GetShoppingCartByUserId(this UnitOfWork unitOfWork, int user_id)
        {
            // Check if user exists
            Models.Identity.User user = unitOfWork.UserRepository.GetById(user_id);
            if (user == null) throw new Exception($"User {user_id} does not exist");

            // Get the cart
            List<ShoppingCart> cart = (List<ShoppingCart>)unitOfWork.ShoppingCartRepository.Get(c => c.UserId == user_id);

            // Return the first result or null
            return cart.FirstOrDefault();
        }

        public static ShoppingCart CreateShoppingCart(this UnitOfWork unitOfWork, int user_id)
        {
            // Check if user exists
            Models.Identity.User user = unitOfWork.UserRepository.GetById(user_id);
            if (user == null) throw new Exception($"User {user_id} does not exist");

            // Check if user already has a cart
            List<ShoppingCart> cart = (List<ShoppingCart>)unitOfWork.ShoppingCartRepository.Get(c => c.UserId == user_id);
            if (cart.Count != 0) throw new Exception($"User {user_id} already has a shopping cart");

            // Create a cart
            ShoppingCart newCart = new ShoppingCart { UserId = user_id };

            // Insert the new cart
            if (unitOfWork.ShoppingCartRepository.Insert(newCart) == null)
                throw new Exception($"Unable to create shopping cart for User {user_id}");

            // Save transaction
            unitOfWork.Save();

            return newCart;
        }

        public static List<CartItemDTO> GetCartItems(this UnitOfWork unitOfWork, int cart_id)
        {
            // Check if cart exists
            ShoppingCart cart = unitOfWork.ShoppingCartRepository.GetById(cart_id);
            if (cart == null) throw new Exception($"Cart {cart_id} does not exist");

            ApplicationDbContext dbContext = (ApplicationDbContext)unitOfWork.GetContext();

            return dbContext.CartItems
                .Where(ci => ci.CartId == cart_id)
                .Join(
                    dbContext.Users,
                    ci => ci.Art.AuthorId,
                    u => u.AuthorId,
                    (ci, u) => new CartItemDTO
                    {
                        Id = ci.Id,
                        Quantity = ci.Quantity,
                        ArtId = ci.ArtId,
                        Art = new ArtDetailDTO
                        {
                            Id = ci.Art.Id,
                            Description = ci.Art.Description,
                            Likes = ci.Art.Likes,
                            Price = ci.Art.Price,
                            Stock = ci.Art.Stock,
                            Style = ci.Art.Style,
                            Url = ci.Art.Url,
                            Author = new ArtDetailDTO.ArtDetailDTOAuthor
                            {
                                Id = (int)ci.Art.Author.Id,
                                Verified = ci.Art.Author.Verified,
                                Description = ci.Art.Author.Description,
                                Name = u.Name,
                                AvatarUrl = u.AvatarUrl,
                                ContactNo = u.PhoneNumber,
                                Dob = u.Dob,
                                Email = u.Email,
                                Ic = u.Ic,
                                Username = u.UserName
                            }
                        },
                        CartId = ci.CartId,
                        Cart = ci.Cart
                    }
                ).ToList();
        }
    }
}
