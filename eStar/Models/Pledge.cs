using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Pledge
    {
        [Key]
        public int PledgeID { get; set; }
        [Display(Name = "Point Target")]
        public int Target { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int PledgeStatusID { get; set; }
        public int Student_User_ID { get; set; }
        public int Guardian_User_ID { get; set; }

        public virtual Guardian Guardians { get; set; }
        public virtual Student Students { get; set; }
        public virtual PledgeStatus PledgeStatus { get; set; }
    }
}