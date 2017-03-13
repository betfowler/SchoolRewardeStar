using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStar.Models
{
     public interface IeStarContext : IDisposable
    {
        DbSet<Account> Accounts { get; }

        int SaveChanges();
        void MarkAsModified(Account item);
    }
}