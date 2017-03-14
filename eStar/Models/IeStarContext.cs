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

        List<Account> GetAllAccounts();
        Account GetAccountById(int id);
        void AddAccount(Account account);
        void UpdateAccount(int id, Account account);
        void DeleteAccount(Account account);
        void Save();

        int SaveChanges();
        void MarkAsModified(Account item);
    }
}