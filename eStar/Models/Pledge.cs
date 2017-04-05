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
        [Display(Name="Student Name")]
        public int Students_User_ID { get; set; }
        [Display(Name = "Guaridan ID")]
        public int Guardians_User_ID { get; set; }
        [Display(Name = "Point Target")]
        public int Target { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}