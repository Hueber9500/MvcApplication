using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            this.Users = new List<UserViewModel>();
        }
        public List<UserViewModel> Users { get; set; }
        public int UserViewModelID { get; set; }
    }
}