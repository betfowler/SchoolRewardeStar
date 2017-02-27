using eStar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStar.Tests
{
    public class TesteStarContext : IeStarContext
    {
        public TesteStarContext()
        {
            this.Accounts = new TestAccountDbSet();
        }

        public DbSet<Account> Accounts { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Account item) { }
        public void Dispose() { }
    }
}
