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
        public int RewardCategory_ID { get; set; }

        public string Reward_Category { get; set; }

        public virtual ICollection<Award> Award { get; set; }
    }
}