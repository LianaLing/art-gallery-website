using System;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.DAL.Extensions
{
    public static class UserLikesExtension
    {
        /// <summary>
        /// Get a UserLikes record from database. 
        /// (Use this over the default `GetById` because the parameter's order is important)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="user_id"></param>
        /// <param name="art_id"></param>
        /// <returns></returns>
        public static UserLikes GetUserLikesById(this UnitOfWork unitOfWork, int user_id, int art_id)
        {
            return unitOfWork.UserLikesRepository.GetById(user_id, art_id);
        }

        /// <summary>
        /// Check if an artwork is already liked by the specified user.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="user_id"></param>
        /// <param name="art_id"></param>
        /// <returns></returns>
        public static bool isArtLiked(this UnitOfWork unitOfWork, int user_id, int art_id)
        {
            return unitOfWork.GetUserLikesById(user_id, art_id) != null;
        }

        /// <summary>
        /// Add a like to the specified art.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="user_id"></param>
        /// <param name="art_id"></param>
        public static void addLikeToArt(this UnitOfWork unitOfWork, int user_id, int art_id)
        {
            // Check if already liked
            if (unitOfWork.isArtLiked(user_id, art_id)) throw new Exception($"User {user_id} has already liked Art {art_id}");

            // Get the art
            Art art = unitOfWork.ArtRepository.GetById(art_id);
            if (art == null) throw new Exception($"Art {art_id} does not exist");

            // Increment the like count
            art.Likes++;
            unitOfWork.ArtRepository.Update(art);

            // Insert a UserLikes record
            UserLikes inserted = unitOfWork.UserLikesRepository.Insert(new UserLikes { UserId = user_id, ArtId = art_id });
            if (inserted == null) throw new Exception($"Unable to insert a new UserLikes record with User {user_id} and Art {art_id}");

            // Save transaction
            unitOfWork.Save();
        }

        /// <summary>
        /// Remove a like from the specified art.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="user_id"></param>
        /// <param name="art_id"></param>
        public static void removeLikeFromArt(this UnitOfWork unitOfWork, int user_id, int art_id)
        {
            // Check if not liked
            if (!unitOfWork.isArtLiked(user_id, art_id)) throw new Exception($"User {user_id} has not liked Art {art_id}");

            // Get the art
            Art art = unitOfWork.ArtRepository.GetById(art_id);
            if (art == null) throw new Exception($"Art {art_id} doest not exist");

            // Decrement the like count
            art.Likes--;
            unitOfWork.ArtRepository.Update(art);

            // Remove the UserLikes record
            UserLikes removed = unitOfWork.UserLikesRepository.Delete(user_id, art_id);
            if (removed == null) throw new Exception($"Unable to remove a UserLikes record with User {user_id} and Art {art_id}");

            // Save transaction
            unitOfWork.Save();
        }
    }
}