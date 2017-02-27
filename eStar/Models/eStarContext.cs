using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class eStarContext : DbContext, IeStarContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public eStarContext() : base("name=eStarContext")
        {
        }

        public System.Data.Entity.DbSet<eStar.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<eStar.Models.Award> Awards { get; set; }

        public System.Data.Entity.DbSet<eStar.Models.RewardCategory> RewardCategories { get; set; }

        public void MarkAsModified(Account item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public System.Data.Entity.DbSet<eStar.Models.StudentAward> StudentAwards { get; set; }
    }
}
