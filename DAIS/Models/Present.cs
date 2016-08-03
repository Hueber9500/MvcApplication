using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.Models
{
    public class Present
    {
        private int presentID;
        private string presentName;

        public Present(int presentID, string presentName)
        {
            this.PresentID = presentID;
            this.PresentName = presentName;
        }

        public int PresentID
        {
            get
            {
                return this.presentID;
            }
            private set
            {
                this.presentID = value;
            }
        }
        public string PresentName
        {
            get
            {
                return this.presentName;
            }
            private set
            {
                this.presentName = value;
            }
        }
    }
}