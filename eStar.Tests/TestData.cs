using eStar.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStar.Tests
{
    class TestData
    {
        public static IQueryable<Account> Accounts
        {
            get
            {
                var accounts = new List<Account>();
                for(int i = 0; i< 10; i++)
                {
                    var account = new Account();
                    accounts.Add(account);
                   
                }
                return accounts.AsQueryable();
            }
        }
    }
}
