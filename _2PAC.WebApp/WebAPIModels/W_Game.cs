using System.Collections.Generic;
using _2PAC.Domain.LogicModel;
namespace _2PAC.WebApp.WebAPIModel
{
    public class W_Game
    {
        public int GameId {get; set;}
        public string GameName {get; set;}
        public string GameDescription {get; set;}
        public double HighScore {get; set;}
        public double AverageReview {get; set;}

        public List<L_Review> Reviews {get; set;}
        public List<L_Score> Scores {get; set;}
        public List<L_GameData> Data {get; set;}
    }
}