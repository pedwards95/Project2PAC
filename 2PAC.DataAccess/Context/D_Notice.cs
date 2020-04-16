using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2PAC.DataAccess.Context
{
    public class D_Notice
    {
        [Key]
        public int NoticeId {get; set;}
        public string Description {get; set;}
        public DateTime Time {get; set;}
    }
}
