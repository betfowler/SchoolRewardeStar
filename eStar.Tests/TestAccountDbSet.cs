using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStar.Tests
{
    class TestAccountDbSet : TestDbSet<Account>
    {
        public override Account Find(params object[] keyValues)
        {
            return this.SingleOrDefault(account => account.User_ID == (int)keyValues.Single());
        }
    }
}
