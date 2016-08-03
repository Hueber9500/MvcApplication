using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.Models
{
    public class User
    {
        private int userID;
        private string username;
        private string password;
        private DateTime birthdate;

        public User(int userId,string username, string password, DateTime birthdate)
        {
            this.UserID = userId;
            this.Username = username;
            this.Password = password;
            this.Birthdate = birthdate;
        }
        public int UserID
        {
            get
            {
                return this.userID;
            }
            private set
            {
                this.userID = value;
            }
        }
        public string Username 
        { 
            get
            {
                return this.username;
            }
            private set
            {
                this.username = value;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            private set
            {
                this.password = value;
            }
        }
        public DateTime Birthdate
        {
            get
            {
                return this.birthdate;
            }
           private set
            {
                this.birthdate = value;
            }
        }
    }
}