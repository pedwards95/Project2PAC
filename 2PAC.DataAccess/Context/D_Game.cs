using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_Game
    {
        [Key]
        public int GameId {get; set;}
        public string GameName {get; set;}
        public string GameDescription {get; set;}

        public virtual ICollection<D_Review> Reviews {get; set;}
        public virtual ICollection<D_Score> Scores {get; set;}
        public virtual ICollection<D_GameData> Data {get; set;}
    }
}
