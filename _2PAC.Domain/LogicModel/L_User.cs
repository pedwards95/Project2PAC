using System.Collections.Generic;

namespace _2PAC.Domain.LogicModel
{
    public class L_User
    {
        public int UserId {get; set;}
        public int PictureId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Description {get; set;}
        public bool Admin {get; set;}

        public List<L_Review> Reviews {get; set;}
        public List<L_Score> Scores {get; set;}
    }
}