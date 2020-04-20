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
    public class ScoreRepository : IScoreRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<ScoreRepository> _logger;

        public ScoreRepository( _2PACdbContext dbContext, ILogger<ScoreRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        public async Task<List<L_Score>> GetAllScores()
        {
            _logger.LogInformation($"Retrieving all scores.");
            IQueryable<D_Score> returnScores = _dbContext.Scores;
            List<D_Score> scores = await returnScores
                .Include(p => p.User)
                .Include(p => p.Game)
                .ToListAsync();
            return scores.Select(Mapper.MapScore).ToList();
        }
        /// <summary> Fetches one score related to its id.
        /// <param name="scoreId"> int (score id) </param>
        /// <returns> A single score related to input id </returns>
        /// </summary>
        public async Task<L_Score> GetScoreById(int scoreId)
        {
            _logger.LogInformation($"Retrieving score with id: {scoreId}");
            D_Score returnScore = await _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ScoreId == scoreId);
            return Mapper.MapScore(returnScore);
        }
        /// <summary> Adds a new game data to the database.
        /// <param name="inputScore"> object L_Score (name of object) - This is a logic object of type score. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddScore(L_Score inputScore)
        {
            if (inputScore.ScoreId != 0)
            {
                _logger.LogWarning($"Score to be added has an ID ({inputScore.ScoreId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new score!",$"{inputScore.ScoreId}");
            }

            _logger.LogInformation("Adding game.");

            D_Score entity = Mapper.UnMapScore(inputScore);
            entity.ScoreId = 0;
            _dbContext.Add(entity);
        }
        /// <summary> Deletes one score related to a score id.
        /// <param name="scoreId"> int (score id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteScoreById(int scoreId)
        {
            _logger.LogInformation($"Deleting score with ID {scoreId}");
            D_Score entity = await _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ScoreId == scoreId);
            if (entity == null)
            {
                _logger.LogInformation($"Score ID {scoreId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
        }
        /// <summary> Changes all score related to a particular existing score.
        /// <param name="inputScore"> object L_Score (name of object) - This is a logic object of type Score. </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task UpdateScore(L_Score inputScore)
        {
            _logger.LogInformation($"Updating score with ID {inputScore.ScoreId}");
            D_Score currentEntity = await _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.ScoreId == inputScore.ScoreId);
            D_Score newEntity = Mapper.UnMapScore(inputScore);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all scores related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All scores related to input game </returns>
        /// </summary>
        public async Task<List<L_Score>> GetScoresByGameId(int gameId)
        {
            _logger.LogInformation($"Retrieving scores with game id: {gameId}");
            IQueryable<D_Score> returnScores = _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game);
            List<D_Score> scores = await returnScores
                .ToListAsync();
            scores = scores.FindAll(p => p.GameId == gameId);

            return scores.Select(Mapper.MapScore).ToList();
        }
        /// <summary> Delete all scores related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteScoresByGameId(int gameId)
        {
            _logger.LogInformation($"Deleting scores with game ID {gameId}");
            IQueryable<D_Score> entity = _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game);
            List<D_Score> scores = await entity
                .ToListAsync();
            scores = scores
                .FindAll(p => p.GameId == gameId);
            if (scores.Count == 0)
            {
                _logger.LogInformation($"Scores with Game ID {gameId} not found to delete! : Returning.");
                return;
            }
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
        }
        /// <summary> Fetches all scores related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> All scores related to input user </returns>
        /// </summary>
        public async Task<List<L_Score>> GetScoresByUserId(int userId)
        {
            _logger.LogInformation($"Retrieving scores with user id: {userId}");
            IQueryable<D_Score> returnScores = _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game);
            List<D_Score> scores = await returnScores
                .ToListAsync();
            scores = scores
                .FindAll(p => p.UserId == userId);
            return returnScores.Select(Mapper.MapScore).ToList();
        }
        /// <summary> Delete all scores related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteScoresByUserId(int userId)
        {
            _logger.LogInformation($"Deleting scores with user ID {userId}");
            IQueryable<D_Score> entity = _dbContext.Scores
                .Include(p => p.User)
                .Include(p => p.Game);
            List<D_Score> scores = await entity
                .ToListAsync();
            scores = scores
                .FindAll(p => p.UserId == userId);
            if (scores.Count == 0)
            {
                _logger.LogInformation($"Scores with Game ID {userId} not found to delete! : Returning.");
                return;
            }
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
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