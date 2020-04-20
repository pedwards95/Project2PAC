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
    public class GameRepository : IGameRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository( _2PACdbContext dbContext, ILogger<GameRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        /// <summary> Fetches all games.
        /// <returns> All games. </returns>
        /// </summary>
        public async Task<List<L_Game>> GetAllGames()
        {
            _logger.LogInformation($"Retrieving all games.");
            List<D_Game> returnGames = await _dbContext.Games
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .ThenInclude(p => p.User)
                .Include(p => p.Data)
                .ToListAsync();
            return returnGames.Select(Mapper.MapGame).ToList();
        }
        /// <summary> Fetches one game related to its id.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> A single game related to input id </returns>
        /// </summary>
        public async Task<L_Game> GetGameById(int gameId)
        {
            _logger.LogInformation($"Retrieving game with id: {gameId}");
            D_Game returnGame = await _dbContext.Games
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .Include(p => p.Data)
                .FirstOrDefaultAsync(p => p.GameId == gameId);
            return Mapper.MapGame(returnGame);
        }
        /// <summary> Adds a new game to the database.
        /// <param name="inputGame"> object L_Game (name of object) - This is a logic object of type game. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddGame(L_Game inputGame)
        {
            if (inputGame.GameId != 0)
            {
                _logger.LogWarning($"Game to be added has an ID ({inputGame.GameId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new game!",$"{inputGame.GameId}");
            }

            _logger.LogInformation("Adding game.");

            D_Game entity = Mapper.UnMapGame(inputGame);
            entity.GameId = 0;
            _dbContext.Add(entity);
        }
        /// <summary> Deletes one game related to a game id.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteGameById(int gameId)
        {
            _logger.LogInformation($"Deleting game with ID {gameId}");
            D_Game entity = await _dbContext.Games
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .Include(p => p.Data)
                .FirstOrDefaultAsync(p => p.GameId == gameId);
            if (entity == null)
            {
                _logger.LogInformation($"Game ID {gameId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
        }
        /// <summary> Changes all game related to a particular existing game.
        /// <param name="inputGame"> object L_Game (name of object) - This is a logic object of type game. </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task UpdateGame(L_Game inputGame)
        {
            _logger.LogInformation($"Updating game with ID {inputGame.GameId}");
            D_Game currentEntity = await _dbContext.Games
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .Include(p => p.Data)
                .FirstOrDefaultAsync(p => p.GameId == inputGame.GameId);
            D_Game newEntity = Mapper.UnMapGame(inputGame);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
// ! RELATED TO OTHER CLASSES

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