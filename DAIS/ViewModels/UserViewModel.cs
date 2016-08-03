using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class UserViewModel
    {
        private int userID;
        private string username;
        private string birthdate;

        
        public UserViewModel(int userID,string username, DateTime birthdate)
        {
            this.UserID = userID;
            this.Username = username;
            this.Birthdate = birthdate.ToString("yyyy-MM-dd") ;
        }
        public UserViewModel()
        {
        }
        public int UserID { get { return this.userID; } private set { this.userID = value; } }
        public string Username { get { return this.username; } private  set { this.username = value; } }
        public string Birthdate { get { return this.birthdate; } private set { this.birthdate = value; } }
    }
}