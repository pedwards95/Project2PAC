using System.Collections.Generic;
namespace _2PAC.Domain.LogicModel
{
    public class L_Game
    {
        public int GameId {get; set;}
        public string GameName {get; set;}
        public string GameDescription {get; set;}

        public List<L_Review> Reviews {get; set;}
        public List<L_Score> Scores {get; set;}
        public List<L_GameData> Data {get; set;}
    }
}