using System.Collections.Generic;
using _2PAC.Domain.LogicModel;
using System.Threading.Tasks;

namespace _2PAC.Domain.Interfaces
{
    public interface IScoreRepository
    {
// ! CLASS SPECIFIC
        Task<List<L_Score>> GetAllScores();
        /// <summary> Fetches one score related to its id.
        /// <param name="scoreId"> int (score id) </param>
        /// <returns> A single score related to input id </returns>
        /// </summary>
        Task<L_Score> GetScoreById(int scoreId);
        /// <summary> Adds a new game data to the database.
        /// <param name="inputScore"> object L_Score (name of object) - This is a logic object of type score. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddScore(L_Score inputScore);
        /// <summary> Deletes one score related to a score id.
        /// <param name="scoreId"> int (score id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteScoreById(int scoreId);
        /// <summary> Changes all score related to a particular existing score.
        /// <param name="inputScore"> object L_Score (name of object) - This is a logic object of type Score. </param>
        /// <returns> void </returns>
        /// </summary>
        Task UpdateScore(L_Score inputScore);
// ! RELATED TO OTHER CLASSES
        /// <summary> Fetches all scores related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> All scores related to input game </returns>
        /// </summary>
        Task<List<L_Score>> GetScoresByGameId(int gameId);
        /// <summary> Delete all scores related to a particular game.
        /// <param name="gameId"> int (game id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteScoresByGameId(int gameId);
        /// <summary> Fetches all scores related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> All scores related to input user </returns>
        /// </summary>
        Task<List<L_Score>> GetScoresByUserId(int userId);
        /// <summary> Delete all scores related to a particular user.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteScoresByUserId(int userId);
// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}