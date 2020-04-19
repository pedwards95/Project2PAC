using System.Threading.Tasks;
using System.Collections.Generic;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface IReviewRepository
    {
// ! CLASS SPECIFIC
        /// <summary> Fetches all reviews.
        /// <returns> All reviews. </returns>
        /// </summary>
        Task<List<L_Review>> GetAllReviews();
        /// <summary> Fetches one review related to its id.
        /// <param name="reviewId"> int (review id) </param>
        /// <returns> A single review related to input id </returns>
        /// </summary>
        Task<L_Review> GetReviewById(int reviewId);
        /// <summary> Adds a new review to the database.
        /// <param name="inputReview"> object L_Review (name of object) - This is a logic object of type review. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddReview(L_Review inputReview);
        /// <summary> Deletes one review related to a review id.
        /// <param name="reviewId"> int (review id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteReviewById(int reviewId);
        /// <summary> Changes all review related to a particular existing review.
        /// <param name="reviewData"> object L_Review (name of object) - This is a logic object of type review. </param>
        /// <returns> void </returns>
        /// </summary>
        Task UpdateReview(L_Review inputReview);
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all reviews related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All reviews related to input game </returns>
        /// </summary>
        Task<List<L_Review>> GetReviewsByGameId(int gameId);
        /// <summary> Delete all reviews related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteReviewsByGameId(int gameId);
        /// <summary> Fetches all reviews related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> All reviews related to input user </returns>
        /// </summary>
        Task<List<L_Review>> GetReviewsByUserId(int userId);
        /// <summary> Delete all reviews related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteReviewsByUserId(int userId);
// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}