using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_User
    {
        [Key]
        public int UserId {get; set;}
        public int PictureId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Description {get; set;}
        public bool Admin {get; set;}

        public virtual ICollection<D_Score> Scores {get; set;}
        public virtual ICollection<D_Review> Reviews {get; set;}
    }
}
