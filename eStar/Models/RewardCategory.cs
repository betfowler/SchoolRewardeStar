using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class RewardCategory
    {
        [Key]
        public int Reward_Category_ID { get; set; }
        [Display(Name = "Reward Category")]
        public string Reward_Category { get; set; }

        public virtual Award Award { get; set; }
    }
}