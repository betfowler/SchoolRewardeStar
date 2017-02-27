using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStar.Tests
{
    public class ContextValues
    {
        public void SetContext(TesteStarContext context)
        {
            context.Accounts.Add(new Account { User_ID = 1, Email = "student@test.com", Password = "1000:h55+O/T6OYC5eCc3peeyTWmqjtcu97oD:FFIbfpn2LZdrpy2lShWU3JPMdl0=", Prefix = "Miss", First_Name = "Test", Surname = "Student", User_Type = "Student" });
            context.Accounts.Add(new Account { User_ID = 2, Email = "staff@test.com", Password = "1000:h55+O/T6OYC5eCc3peeyTWmqjtcu97oD:FFIbfpn2LZdrpy2lShWU3JPMdl0=", Prefix = "Mr", First_Name = "Test", Surname = "Staff", User_Type = "Staff" });
            context.Accounts.Add(new Account { User_ID = 3, Email = "guardian@test.com", Password = "1000:h55+O/T6OYC5eCc3peeyTWmqjtcu97oD:FFIbfpn2LZdrpy2lShWU3JPMdl0=", Prefix = "Mrs", First_Name = "Test", Surname = "Guardian", User_Type = "Guardian" });
            context.Accounts.Add(new Account { User_ID = 4, Email = "admin@test.com", Password = "1000:h55+O/T6OYC5eCc3peeyTWmqjtcu97oD:FFIbfpn2LZdrpy2lShWU3JPMdl0=", Prefix = "Miss", First_Name = "Test", Surname = "Admin", User_Type = "Admin" });
        }
    }
}
