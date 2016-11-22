using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GotFoodConnections.Models
{
    public class RateLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateLogID { get; set; }
        public int VoteForID { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
        public bool Active { get; set; }
    }
}