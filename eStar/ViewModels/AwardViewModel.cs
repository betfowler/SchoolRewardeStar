using eStar.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStar.ViewModels
{
    public class AwardViewModel
    {
        public Award Award { get; set; }
        public IEnumerable<SelectListItem> RewardCategories { get; set; }
    }
}