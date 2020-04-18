using System.Collections.Generic;
using _2PAC.Domain.LogicModel;

namespace _2PAC.Domain.Interfaces
{
    public interface IUserRepository
    {
// ! CLASS SPECIFIC
        /// <summary> Fetches all users.
        /// <returns> All users. </returns>
        /// </summary>
        List<L_User> GetAllUsers();
        /// <summary> Fetches one user related to its id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> A single user related to input id </returns>
        /// </summary>
        L_User GetUserById(int userId);
        /// <summary> Adds a new user to the database.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        void AddUser(L_User inputUser);
        /// <summary> Deletes one user related to a user id.
        /// <param name="userId"> int (user id) </param>
        /// <returns> void </returns>
        /// </summary>
        void DeleteUserById(int userId);
        /// <summary> Changes all user related to a particular existing user.
        /// <param name="inputUser"> object L_User (name of object) - This is a logic object of type user. </param>
        /// <returns> void </returns>
        /// </summary>
        void UpdateUser(L_User inputUser);
// ! RELATED TO OTHER CLASSES

// ! GENERAL COMMANDS
        /// <summary> Commit changes in the selected repository and related database.
        /// <returns> void </returns>
        /// </summary>
        void Save();
    }
}