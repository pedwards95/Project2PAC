using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_Review
    {
        [Key]
        public int ReviewId {get; set;}
        public int UserId {get; set;}
        public int GameId {get; set;}
        [Range(1,10)]
        public int Rating {get; set;}
        public string ReviewBody {get; set;}

        public virtual D_User User {get; set;}
        public virtual D_Game Game {get; set;}
    }
}
