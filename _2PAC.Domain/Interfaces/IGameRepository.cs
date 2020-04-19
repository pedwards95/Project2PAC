using System.Collections.Generic;
using System.Threading.Tasks;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface IGameRepository
    {
// ! CLASS SPECIFIC
        /// <summary> Fetches all games.
        /// <returns> All games. </returns>
        /// </summary>
        Task<List<L_Game>> GetAllGames();
        /// <summary> Fetches one game related to its id.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> A single game related to input id </returns>
        /// </summary>
        Task<L_Game> GetGameById(int gameId);
        /// <summary> Adds a new game to the database.
        /// <param name="inputGame"> object L_Game (name of object) - This is a logic object of type game. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddGame(L_Game inputGame);
        /// <summary> Deletes one game related to a game id.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteGameById(int gameId);
        /// <summary> Changes all game related to a particular existing game.
        /// <param name="inputGame"> object L_Game (name of object) - This is a logic object of type game. </param>
        /// <returns> void </returns>
        /// </summary>
        Task UpdateGame(L_Game inputGame);
// ! RELATED TO OTHER CLASSES

// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}