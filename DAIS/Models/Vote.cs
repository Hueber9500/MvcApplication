using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.Models
{
    public class Vote
    {
        private int userID;
        private int presentID;
        private int adminID;
        private int birthdayUserID;

        public Vote(int userID, int presentID, int adminID, int birthdayUserID)
        {
            this.UserID = userID;
            this.PresentID = presentID;
            this.AdminID = adminID;
            this.BbirthdayUserID = birthdayUserID;
        }

        public int UserID
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
        public int PresentID
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
        public int AdminID
        {
            get
            {
                return this.adminID;
            }
            set
            {
                this.adminID = value;
            }
        }
        public int BbirthdayUserID
        {
            get
            {
                return this.birthdayUserID;
            }
            set
            {
                this.birthdayUserID = value;
            }
        }
    }
}