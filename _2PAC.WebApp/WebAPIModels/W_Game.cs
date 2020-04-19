using System.Collections.Generic;
namespace _2PAC.WebApp.WebAPIModel
{
    public class W_Game
    {
        public int GameId {get; set;}
        public string GameName {get; set;}
        public string GameDescription {get; set;}

        public List<W_Review> Reviews {get; set;}
        public List<W_Score> Scores {get; set;}
        public List<W_GameData> Data {get; set;}
    }
}