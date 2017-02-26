﻿using System;
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
        [Display(Name = "Student Name")]
        public int Student_ID { get; set; }
        [Display(Name = "Staff Name")]
        public int Staff_ID { get; set; }
        [Display(Name = "Number Points")]
        public int Num_Points { get; set; }
        [Display(Name = "Reward Category")]
        public int Reward_Category_ID { get; set; }
        public virtual RewardCategory RewardCategory { get; set; }
        [Display(Name = "Comment")]
        public string Reward_Comment { get; set; }

    }
}