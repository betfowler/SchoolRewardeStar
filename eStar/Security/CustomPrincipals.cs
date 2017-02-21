using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using eStar.Models;

namespace eStar.Security
{
    public class CustomPrincipals : IPrincipal
    {
        private Account Account;
        public CustomPrincipals(Account account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity(account.Email);
        }
        public IIdentity Identity{get; set; }

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Account.User_Type.Contains(r));
        }
    }
}