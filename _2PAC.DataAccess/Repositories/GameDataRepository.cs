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
    public class GameDataRepository : IGameDataRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<GameDataRepository> _logger;

        public GameDataRepository( _2PACdbContext dbContext, ILogger<GameDataRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

// ! CLASS SPECIFIC
        /// <summary> Fetches all game data.
        /// <returns> All game data. </returns>
        /// </summary>
        public async Task<List<L_GameData>> GetAllGameData()
        {
            _logger.LogInformation($"Retrieving all game data.");
            List<D_GameData> returnGameData = await _dbContext.GameDatas
                .Include(p => p.Game)
                .ToListAsync();
            return returnGameData.Select(Mapper.MapGameData).ToList();
        }
        /// <summary> Fetches one game data related to its id.
        /// <param name="gameDataId"> int (game data id) </param>
        /// <returns> A single game data related to input id </returns>
        /// </summary>
        public async Task<L_GameData> GetGameDataById(int gameDataId)
        {
            _logger.LogInformation($"Retrieving game data with id: {gameDataId}");
            D_GameData returnGameData = await _dbContext.GameDatas
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.DataId == gameDataId);
            return Mapper.MapGameData(returnGameData);
        }
        /// <summary> Adds a new game data to the database.
        /// <param name="inputGameData"> object L_GameData (name of object) - This is a logic object of type game data. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddGameData(L_GameData inputGameData)
        {
            if (inputGameData.DataId != 0)
            {
                _logger.LogWarning($"Game data to be added has an ID ({inputGameData.DataId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new game data!",$"{inputGameData.DataId}");
            }

            _logger.LogInformation("Adding game data.");

            D_GameData entity = Mapper.UnMapGameData(inputGameData);
            entity.DataId = 0;
            _dbContext.Add(entity);
        }
        /// <summary> Deletes one game data related to a game data id.
        /// <param name="gameDataId"> int (game data id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteGameDataById(int gameDataId)
        {
            _logger.LogInformation($"Deleting game data with ID {gameDataId}");
            D_GameData entity = await _dbContext.GameDatas
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.DataId == gameDataId);
            if (entity == null)
            {
                _logger.LogInformation($"Game data ID {gameDataId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
        }
        /// <summary> Changes all game data related to a particular existing game data.
        /// <param name="inputGameData"> object L_GameData (name of object) - This is a logic object of type game data. </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task UpdateGameData(L_GameData inputGameData)
        {
            _logger.LogInformation($"Updating game data with ID {inputGameData.DataId}");
            D_GameData currentEntity = await _dbContext.GameDatas
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.DataId == inputGameData.DataId);
            D_GameData newEntity = Mapper.UnMapGameData(inputGameData);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all game data related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All game data related to input game </returns>
        /// </summary>
        public async Task<List<L_GameData>> GetGameDataByGameId(int gameId)
        {
            _logger.LogInformation($"Retrieving game data with game id: {gameId}");
            List<D_GameData> returnGameData = await _dbContext.GameDatas
                .Include(p => p.Game)
                .ToListAsync();
            returnGameData = returnGameData
                .FindAll(p => p.GameId == gameId);
            return returnGameData.Select(Mapper.MapGameData).ToList();
        }
        /// <summary> Delete all game data related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteGameDataByGameId(int gameId)
        {
            _logger.LogInformation($"Deleting game data with game ID {gameId}");
            List<D_GameData> entity = await _dbContext.GameDatas
                .Include(p => p.Game)
                .ToListAsync();
            entity = entity
                .FindAll(p => p.GameId == gameId);
            if (entity.Count == 0)
            {
                _logger.LogInformation($"Game data with Game ID {gameId} not found to delete! : Returning.");
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