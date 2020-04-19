using System.Collections.Generic;
using System.Threading.Tasks;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface INoticeRepository
    {
// ! CLASS SPECIFIC
        /// <summary> Fetches all notices.
        /// <returns> All notices. </returns>
        /// </summary>
        Task<List<L_Notice>> GetAllNotices();
        /// <summary> Fetches one notice related to its id.
        /// <param name="noticeId"> int (notice id) </param>
        /// <returns> A single notice related to input id </returns>
        /// </summary>
        Task<L_Notice> GetNoticeById(int noticeId);
        /// <summary> Adds a new notice to the database.
        /// <param name="inputNotice"> object L_Notice (name of object) - This is a logic object of type notice. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddNotice(L_Notice inputNotice);
        /// <summary> Deletes one notice related to a notice id.
        /// <param name="noticeId"> int (notice id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteNoticeById(int noticeId);
        /// <summary> Changes all notice related to a particular existing notice.
        /// <param name="inputNotice"> object L_Notice (name of object) - This is a logic object of type notice. </param>
        /// <returns> void </returns>
        /// </summary>
        Task UpdateNotice(L_Notice inputNotice);
// ! RELATED TO OTHER CLASSES

// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}