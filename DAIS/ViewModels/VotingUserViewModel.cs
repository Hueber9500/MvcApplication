using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class VotingUserViewModel
    {
        public UserViewModel User { get; set; }
        public PresentListViewModel Presents { get; set; }
    }
}