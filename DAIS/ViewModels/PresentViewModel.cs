using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class PresentViewModel
    {
        private string presentName;
        private int presentID;

        public PresentViewModel(int presentID,string presentName)
        {
            this.PresentName = presentName;
            this.PresentID = presentID;
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
    }
}