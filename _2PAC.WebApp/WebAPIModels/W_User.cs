using System.Collections.Generic;

namespace _2PAC.WebApp.WebAPIModel
{
    public class W_User
    {
        public int UserId {get; set;}
        public int PictureId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Description {get; set;}
        public bool Admin {get; set;}
        public string Token {get; set;}

        public List<W_Review> Reviews {get; set;}
        public List<W_Score> Scores {get; set;}
    }
}