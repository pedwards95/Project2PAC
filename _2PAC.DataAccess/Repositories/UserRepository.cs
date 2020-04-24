using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using _2PAC.DataAccess.Context;
using _2PAC.DataAccess.Logic;
using _2PAC.Domain.Interfaces;
using _2PAC.Domain.LogicModel;

namespace _2PAC.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public string Secret { get; set; }
        private readonly _2PACdbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository( _2PACdbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Secret = "longerinsecurestringlolisthislongenough";
        }
// ! CLASS SPECIFIC
        /// <summary> Fetches all users.
        /// <returns> All users. </returns>
        /// </summary>
        public async Task<List<L_User>> GetAllUsers()
        {
            _logger.LogInformation($"Retrieving all users.");
            IQueryable<D_User> returnUsers = _dbContext.Users
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .ThenInclude(p => p.Game);
            List<D_User> users = await returnUsers.ToListAsync();
            return users.Select(Mapper.MapUser).ToList();
        }
        /// <summary> Fetches one user related to its id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> A single user related to input id </returns>
        /// </summary>
        public async Task<L_User> GetUserById(int userId)
        {
            _logger.LogInformation($"Retrieving user with id: {userId}");
            D_User returnUser = await _dbContext.Users
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .ThenInclude(p => p.Game)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if(returnUser == null)
            {
                _logger.LogInformation($"No user with id: {userId} found!");
                return null;
            }
            return Mapper.MapUser(returnUser);
        }
        /// <summary> Fetches one user related to its strings.
        /// <param name="userString"> string (user string names) </param>
        /// <returns> An array of users related to input string </returns>
        /// </summary>
        public async Task<List<L_User>> GetUserByString(string userString)
        {
            _logger.LogInformation($"Retrieving user with string: {userString}");
            List<D_User> returnUser = await _dbContext.Users
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .ThenInclude(p => p.Game)
                .Where(p => (p.FirstName.ToLower().Contains(userString.ToLower())
                    || p.LastName.ToLower().Contains(userString.ToLower())
                    || p.Username.ToLower().Contains(userString.ToLower())
                    ))
                .ToListAsync();
            if(returnUser == null)
            {
                _logger.LogInformation($"No users with string: {userString} found!");
                return new List<L_User>{};
            }
            return returnUser.Select(Mapper.MapUser).ToList();
        }
        /// <summary> Fetches one user related to its username.
        /// <param name="username"> string (users username) </param>
        /// <returns> A single user related to input username </returns>
        /// </summary>
        public async Task<L_User> GetUserByUsername(string username)
        {
            _logger.LogInformation($"Retrieving user with username: {username}");
            D_User returnUser = await _dbContext.Users
                .Include(p => p.Scores)
                .Include(p => p.Reviews)
                .ThenInclude(p => p.Game)
                .FirstOrDefaultAsync(p => p.Username.ToLower() == username.ToLower());
            if(returnUser == null)
            {
                _logger.LogInformation($"No user with username: {username} found!");
                return null;
            }
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
            Save();
        }
        /// <summary> Deletes one user related to a user id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task DeleteUserById(int userId)
        {
            _logger.LogInformation($"Deleting user with ID {userId}");
            D_User entity = await _dbContext.Users.FindAsync(userId);
            if (entity == null)
            {
                _logger.LogInformation($"Game ID {userId} not found to delete! : Returning.");
                return;
            }
            _dbContext.Remove(entity);
            Save();
        }
        /// <summary> Changes all user related to a particular existing user.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        public async Task UpdateUser(L_User inputUser)
        {
            _logger.LogInformation($"Updating user with ID {inputUser.UserId}");
            D_User currentEntity = await _dbContext.Users.FindAsync(inputUser.UserId);
            D_User newEntity = Mapper.UnMapUser(inputUser);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            Save();
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