using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStar.Security
{
    public static class SessionPersister
    {
        static string emailSessionvar = "username";
        static string usernameSessionvar = "username";
        static string userSessionvar = "usertype";

        public static string Email
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[emailSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[emailSessionvar] = value;
            }
        }

        public static int RemainingPoints { get; internal set; }
        public static int UserID { get; internal set; }

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }

        public static string UserType
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[userSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[userSessionvar] = value;
            }
        }
    }
}