using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class PresentListViewModel
    {
        public PresentListViewModel()
        {
            this.Presents = new List<PresentViewModel>();
        }
        public List<PresentViewModel> Presents { get; set; }
    }
}