using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Award
    {
        [Key]
        public int Award_ID { get; set; }
        [Display(Name = "Staff Name")]
        public int Staff_ID { get; set; }
        [Display(Name = "Number Points")]
        public int Num_Points { get; set; }
        [Display(Name = "Reward Category")]
        public int RewardCategory_ID { get; set; }
        [Display(Name = "Comment")]
        public string Reward_Comment { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? AwardDate { get; set; }
        [Display(Name = "Subject")]
        public int Subject_ID { get; set; }


        public List<int> Students { get; set; } 
        public int StudentCount { get; set; }
        public List<string> StudentNames { get; set; }

        public virtual ICollection<StudentAward> StudentAwards { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual RewardCategory RewardCategory { get; set; }
        public virtual Subject Subject { get; set; }
        
    }
}