﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Security;

namespace eStar.Models
{
    public class AccountModel
    {
        private eStarContext db = new eStarContext();

        public AccountModel() { }

        public Account find(string email)
        {
            return db.Accounts.Where(acc => acc.Email.Equals(email)).FirstOrDefault();
        }

        public bool findPassword(string email)
        {
            if (db.Accounts.Where(acc => acc.Email.Equals(email)).FirstOrDefault().Password == null)
                return true;
            return false;
        }

        public bool login(string email, string password)
        {
            var getPassword = db.Accounts.SingleOrDefault(acc => acc.Email.Equals(email)); 
            return Hashing.ValidatePassword(password, getPassword.Password);            
        }

        
    }
}