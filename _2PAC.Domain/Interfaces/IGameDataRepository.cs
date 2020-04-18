using System.Collections.Generic;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface IGameDataRepository
    {
// ! CLASS SPECIFIC
        /// <summary> Fetches one game data related to its id.
        /// <param name="gameDataId"> int (game data id) </param>
        /// <returns> A single game data related to input id </returns>
        /// </summary>
        L_GameData GetGameDataById(int gameDataId);
        /// <summary> Adds a new game data to the database.
        /// <param name="inputGameData"> object L_GameData (name of object) - This is a logic object of type game data. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddGameData(L_GameData inputGameData);
        /// <summary> Deletes one game data related to a game data id.
        /// <param name="gameDataId"> int (game data id) </param>
        /// <returns> void </returns>
        /// </summary>
        void DeleteGameDataById(int gameDataId);
        /// <summary> Changes all game data related to a particular existing game data.
        /// <param name="inputGameData"> object L_GameData (name of object) - This is a logic object of type game data. </param>
        /// <returns> void </returns>
        /// </summary>
        void UpdateGameData(L_GameData inputGameData);
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all game data related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All game data related to input game </returns>
        /// </summary>
        List<L_GameData> GetGameDataByGameId(int gameId);
        /// <summary> Delete all game data related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        void DeleteGameDataByGameId(int gameId);
// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}