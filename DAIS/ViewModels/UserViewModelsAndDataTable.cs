using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAIS.ViewModels
{
    public class UserViewModelsAndDataTable
    {
        public UserListViewModel Users { get; set; }
        public DataTable Datatable { get; set; }
    }
}