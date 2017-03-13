using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Security;

namespace eStar.Models
{
    public class AccountModel
    {

        private eStarContext db = new eStarContext();
        //modify the type of the db field
        /*private IeStarContext db = new eStarContext();
        //add constructors
        public AccountModel() { }
        public AccountModel(IeStarContext context)
        {
            db = context;
        }*/
        

        public Account find(string email)
        {
            return db.Accounts.Where(acc => acc.Email.Equals(email)).FirstOrDefault();
        }

        public int findStaffPoints(string email)
        {
            return db.Accounts.OfType<Staff>().Where(acc => acc.Email.Equals(email)).FirstOrDefault().Remaining_Points;
        }

        public Student findStudent(string email)
        {
            return db.Accounts.OfType<Student>().Where(acc => acc.Email.Equals(email)).FirstOrDefault();
        }

        public Account findUsingID(int id)
        {
            return db.Accounts.Where(acc => acc.User_ID.Equals(id)).FirstOrDefault();
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