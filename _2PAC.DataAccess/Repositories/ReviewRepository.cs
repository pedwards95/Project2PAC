using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using _2PAC.DataAccess.Context;
using _2PAC.DataAccess.Logic;
using _2PAC.Domain.Interfaces;
using _2PAC.Domain.LogicModel;

namespace _2PAC.DataAccess.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<GameRepository> _logger;

        public ReviewRepository( _2PACdbContext dbContext, ILogger<GameRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        /// <summary> Fetches all reviews.
        /// <returns> All reviews. </returns>
        /// </summary>
        public async Task<List<L_Review>> GetAllReviews()
        {
            _logger.LogInformation($"Retrieving all reviews.");
            List<D_Review> returnReviews = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .ToListAsync();
            return returnReviews.Select(Mapper.MapReview).ToList();
        }
        /// <summary> Fetches one review related to its id.
        /// <param name="reviewId"> int (review id) </param>
        /// <returns> A single review related to input id </returns>
        /// </summary>
        public async Task<L_Review> GetReviewById(int reviewId)
        {
            _logger.LogInformation($"Retrieving review with id: {reviewId}");
            D_Review returnReview = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ReviewId == reviewId);
            return Mapper.MapReview(returnReview);
        }
        /// <summary> Adds a new review to the database.
        /// <param name="inputReview"> object L_Review (name of object) - This is a logic object of type review. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddReview(L_Review inputReview)
        {
            if (inputReview.ReviewId != 0)
            {
                _logger.LogWarning($"Review to be added has an ID ({inputReview.ReviewId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new game!",$"{inputReview.ReviewId}");
            }

            _logger.LogInformation("Adding review.");

            D_Review entity = Mapper.UnMapReview(inputReview);
            entity.ReviewId = 0;
            _dbContext.Add(entity);
            Save();
        }
        /// <summary> Deletes one review related to a review id.
        /// <param name="reviewId"> int (review id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteReviewById(int reviewId)
        {
            _logger.LogInformation($"Deleting review with ID {reviewId}");
            D_Review entity = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ReviewId == reviewId);
            if (entity == null)
            {
                _logger.LogInformation($"Review ID {reviewId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
            Save();
        }
        /// <summary> Changes all review related to a particular existing review.
        /// <param name="reviewData"> object L_Review (name of object) - This is a logic object of type review. </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task UpdateReview(L_Review inputReview)
        {
            _logger.LogInformation($"Updating review with ID {inputReview.ReviewId}");
            D_Review currentEntity = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ReviewId == inputReview.ReviewId);
            D_Review newEntity = Mapper.UnMapReview(inputReview);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            Save();
        }
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all reviews related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All reviews related to input game </returns>
        /// </summary>
        public async Task<List<L_Review>> GetReviewsByGameId(int gameId)
        {
            _logger.LogInformation($"Retrieving reviews with game id: {gameId}");
            IQueryable<D_Review> returnReviews = _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game);
            List<D_Review> reviews = await returnReviews
                .ToListAsync();
            reviews = reviews
                .FindAll(p => p.GameId == gameId);
            return returnReviews.Select(Mapper.MapReview).ToList();
        }
        /// <summary> Delete all reviews related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteReviewsByGameId(int gameId)
        {
            _logger.LogInformation($"Deleting reviews with game ID {gameId}");
            List<D_Review> entity = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .ToListAsync();
            entity = entity
                .FindAll(p => p.GameId == gameId);
            if (entity.Count == 0)
            {
                _logger.LogInformation($"Reviews with Game ID {gameId} not found to delete! : Returning.");
                return;
            }
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
            Save();
        }
        /// <summary> Fetches all reviews related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> All reviews related to input user </returns>
        /// </summary>
        public async Task<List<L_Review>> GetReviewsByUserId(int userId)
        {
            _logger.LogInformation($"Retrieving reviews with user id: {userId}");
            List<D_Review> returnReviews = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .ToListAsync();
            returnReviews
                .FindAll(p => p.UserId == userId);
            return returnReviews.Select(Mapper.MapReview).ToList();
        }
        /// <summary> Delete all reviews related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteReviewsByUserId(int userId)
        {
            _logger.LogInformation($"Deleting reviews with game ID {userId}");
            List<D_Review> entity = await _dbContext.Reviews
                .Include(p => p.User)
                .Include(p => p.Game)
                .ToListAsync();
            entity = entity
                .FindAll(p => p.UserId == userId);
            if (entity.Count == 0)
            {
                _logger.LogInformation($"Reviews with user ID {userId} not found to delete! : Returning.");
                return;
            }
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
            Save();
        }
// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        public void Save()
        {
            _logger.LogInformation("Saving changes to the database");
            _dbContext.SaveChanges();
        }
    }
}