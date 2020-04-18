namespace _2PAC.Domain.LogicModel
{
    public class L_Review
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