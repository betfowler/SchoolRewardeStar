using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class PledgeStatus
    {
        [Key]
        public int PledgeStatusID { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Pledge> Pledges { get; set; }
    }
}