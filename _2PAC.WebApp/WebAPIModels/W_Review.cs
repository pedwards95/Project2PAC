namespace _2PAC.WebApp.WebAPIModel
{
    public class W_Review
    {
        public int ReviewId {get; set;}
        public int UserId {get; set;}
        public string Username {get; set;}
        public int GameId {get; set;}
        public string GameName {get; set;}
        public int Rating {get; set;}
        public string ReviewBody {get; set;}
    }
}