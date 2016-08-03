using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class VotingPresentViewModel
    {
        public VotingPresentViewModel()
        {
            this.Votes = 0;
        }
        public PresentViewModel PresentVM { get; set; }
        public int Votes { get; set; }
    }
}