using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_Score
    {
        [Key]
        public int ScoreId {get; set;}
        public int UserId {get; set;}
        public int GameId {get; set;}
        public double Score {get; set;}

        public virtual D_User User {get; set;}
        public virtual D_Game Game {get; set;}
    }
}
