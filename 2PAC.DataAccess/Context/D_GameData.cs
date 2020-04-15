using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_GameData
    {
        [Key]
        public int DataId {get; set;}
        public int GameId {get; set;}
        [Range(1,20)]
        public int Difficulty {get; set;}
        public string Question {get; set;}
        public string Answer {get; set;}

        public virtual D_Game Game {get; set;}
    }
}
