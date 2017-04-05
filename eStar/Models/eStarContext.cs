using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class eStarContext : DbContext
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

        public void MarkAsModified(Account item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public DbSet<Award> Awards { get; set; }

        public DbSet<StudentAward> StudentAwards { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Enrolment> Enrolments { get; set; }

        public DbSet<ClassStaff> ClassStaffs { get; set; }

        public DbSet<RewardCategory> RewardCategories { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<StudentGuardian> StudentGuardians { get; set; }
        public DbSet<PledgeStatus> PledgeStatuses { get; set; }
        public DbSet<Pledge> Pledges { get; set; }
    }
}
