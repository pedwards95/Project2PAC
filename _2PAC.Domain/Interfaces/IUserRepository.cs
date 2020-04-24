using System.Collections.Generic;
using System.Threading.Tasks;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface IUserRepository
    {
        public string Secret { get; set; }
// ! CLASS SPECIFIC
        /// <summary> Fetches all users.
        /// <returns> All users. </returns>
        /// </summary>
        Task<List<L_User>> GetAllUsers();
        /// <summary> Fetches one user related to its id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> A single user related to input id </returns>
        /// </summary>
        Task<L_User> GetUserById(int userId);
        /// <summary> Fetches one user related to its strings.
        /// <param name="userString"> string (user string names) </param>
        /// <returns> An array of users related to input string </returns>
        /// </summary>
        Task<List<L_User>> GetUserByString(string userString);
        /// <summary> Fetches one user related to its username.
        /// <param name="username"> string (users username) </param>
        /// <returns> A single user related to input username </returns>
        /// </summary>
        Task<L_User> GetUserByUsername(string username);
        /// <summary> Adds a new user to the database.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddUser(L_User inputUser);
        /// <summary> Deletes one user related to a user id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        Task DeleteUserById(int userId);
        /// <summary> Changes all user related to a particular existing user.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        Task UpdateUser(L_User inputUser);
// ! RELATED TO OTHER CLASSES

// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}