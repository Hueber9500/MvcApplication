using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class VoteViewModel
    {
        private int userID;
        private int presentID;

        public VoteViewModel(int userID,int presentID)
        {
            this.UserID = userID;
            this.PresentID = presentID;
        }

        private int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }
        private int PresentID
        {
             get
            {
                return this.presentID;
            }
            set
            {
                this.presentID = value;
            }
        }
        public string Username { get; set; }
        public string PresentName { get; set; }
    }
}