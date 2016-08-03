using DAIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DAIS.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                BusinessLayer bal = new BusinessLayer();
                UserStatus status = bal.GetUserValidity(u);
                if (status == UserStatus.NonAuthenticatedUser)
                {
                    ModelState.AddModelError("CredentialError", "Invalid username or password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login"); 
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
