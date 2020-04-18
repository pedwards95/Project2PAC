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
    public class UserRepository : IUserRepository
    {
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository( _2PACdbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        /// <summary> Fetches all users.
        /// <returns> All users. </returns>
        /// </summary>
        public List<L_User> GetAllUsers()
        {
            _logger.LogInformation($"Retrieving all users.");
            List<D_User> returnUsers = _dbContext.Users.ToList();
            return returnUsers.Select(Mapper.MapUser).ToList();
        }
        /// <summary> Fetches one user related to its id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> A single user related to input id </returns>
        /// </summary>
        public L_User GetUserById(int userId)
        {
            _logger.LogInformation($"Retrieving user with id: {userId}");
            D_User returnUser = _dbContext.Users
                .First(p => p.UserId == userId);
            return Mapper.MapUser(returnUser);
        }
        /// <summary> Adds a new user to the database.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        public void AddUser(L_User inputUser)
        {
            if (inputUser.UserId != 0)
            {
                _logger.LogWarning($"User to be added has an ID ({inputUser.UserId}) already!");
                throw new ArgumentException("Id already exists when trying to add a new user!",$"{inputUser.UserId}");
            }

            _logger.LogInformation("Adding user.");

            D_User entity = Mapper.UnMapUser(inputUser);
            entity.UserId = 0;
            _dbContext.Add(entity);
        }
        /// <summary> Deletes one user related to a user id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        public void DeleteUserById(int userId)
        {
            _logger.LogInformation($"Deleting user with ID {userId}");
            D_User entity = _dbContext.Users.Find(userId);
            if (entity == null)
            {
                _logger.LogInformation($"Game ID {userId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
        }
        /// <summary> Changes all user related to a particular existing user.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        public void UpdateUser(L_User inputUser)
        {
            _logger.LogInformation($"Updating user with ID {inputUser.UserId}");
            D_User currentEntity = _dbContext.Users.Find(inputUser.UserId);
            D_User newEntity = Mapper.UnMapUser(inputUser);

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