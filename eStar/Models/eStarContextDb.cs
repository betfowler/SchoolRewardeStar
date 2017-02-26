using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class IeStarContext : DbContext
    {
        public IeStarContext() : base("name=eStarContext")
        {
        }

        public DbSet<Account> Account { get; set; }
    }
}