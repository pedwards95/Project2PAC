using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using _2PAC.DataAccess.Context;
using _2PAC.DataAccess.Logic;
using _2PAC.Domain.Interfaces;
using _2PAC.Domain.LogicModel;

namespace _2PAC.DataAccess.Repositories
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<NoticeRepository> _logger;

        public NoticeRepository( _2PACdbContext dbContext, ILogger<NoticeRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        /// <summary> Fetches all notices.
        /// <returns> All notices. </returns>
        /// </summary>
        public List<L_Notice> GetAllNotices()
        {
            _logger.LogInformation($"Retrieving all notices.");
            List<D_Notice> returnNotices = _dbContext.Notices.ToList();
            return returnNotices.Select(Mapper.MapNotice).ToList();
        }
        /// <summary> Fetches one notice related to its id.
        /// <param name="noticeId"> int (notice id) </param>
        /// <returns> A single notice related to input id </returns>
        /// </summary>
        public L_Notice GetNoticeById(int noticeId)
        {
            _logger.LogInformation($"Retrieving notice with id: {noticeId}");
            D_Notice returnNotice = _dbContext.Notices
                .First(p => p.NoticeId == noticeId);
            return Mapper.MapNotice(returnNotice);
        }
        /// <summary> Adds a new notice to the database.
        /// <param name="inputNotice"> object L_Notice (name of object) - This is a logic object of type notice. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddNotice(L_Notice inputNotice)
        {
            if (inputNotice.NoticeId != 0)
            {
                _logger.LogWarning($"Notice to be added has an ID ({inputNotice.NoticeId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new notice!",$"{inputNotice.NoticeId}");
            }

            _logger.LogInformation("Adding notice.");

            D_Notice entity = Mapper.UnMapNotice(inputNotice);
            entity.NoticeId = 0;
            _dbContext.Add(entity);
        }
        /// <summary> Deletes one notice related to a notice id.
        /// <param name="noticeId"> int (notice id) </param>
        /// <returns> void </returns>
        /// </summary>
        public void DeleteNoticeById(int noticeId)
        {
            _logger.LogInformation($"Deleting notice with ID {noticeId}");
            D_Notice entity = _dbContext.Notices.Find(noticeId);
            if (entity == null)
            {
                _logger.LogInformation($"Notice ID {noticeId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
        }
        /// <summary> Changes all notice related to a particular existing notice.
        /// <param name="inputNotice"> object L_Notice (name of object) - This is a logic object of type notice. </param>
        /// <returns> void </returns>
        /// </summary>
        public void UpdateNotice(L_Notice inputNotice)
        {
            _logger.LogInformation($"Updating notice with ID {inputNotice.NoticeId}");
            D_Notice currentEntity = _dbContext.Notices.Find(inputNotice.NoticeId);
            D_Notice newEntity = Mapper.UnMapNotice(inputNotice);

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