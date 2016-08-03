using DAIS.DataAccessLayer;
using DAIS.Models;
using DAIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAIS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            UserListViewModel ulvm = new UserListViewModel();
            ViewData["username"] = User.Identity.Name;
            BusinessLayer ubl = new BusinessLayer();

            User loggedUser = ubl.GetUserByUsername(User.Identity.Name);

            bool hasVote = ubl.HasVote();
            bool isVoteStopped = ubl.IsVotingStopped();
            if(hasVote && !isVoteStopped)
            {
                if (ubl.IsUserBirthdayUser(loggedUser))
                {
                    return RedirectToAction("BirthdayUser", "User");
                }
                if (ubl.IsUserAdminUser(loggedUser))
                {
                    return RedirectToAction("AdminIndex", "User");
                }
                return RedirectToAction("NotAdminIndex", "User");
            }
            
            foreach (User user in ubl.GetUsers())
            {
                if (!user.Username.Equals(User.Identity.Name))
                {
                    UserViewModel userVM = new UserViewModel(user.UserID,user.Username, user.Birthdate);
                    ulvm.Users.Add(userVM);
                }
            }
            if (isVoteStopped && !ubl.IsUserBirthdayUser(loggedUser))
            {
                UserViewModelsAndDataTable uvmadt = new UserViewModelsAndDataTable();
                DataTable dt = ubl.GetVoteTable();
                uvmadt.Datatable = dt;
                uvmadt.Users = ulvm;
                return View("IndexStopVote", uvmadt);
            }
            return View("Index", ulvm);
        }

    }
}
