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
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            BusinessLayer bal=new BusinessLayer();

            int userID=Convert.ToInt32(Request.Form["submit"]);

            User birthdayUser = bal.GetUserById(userID);
            User currentLogged = bal.GetUserByUsername(User.Identity.Name);
            UserViewModel uvm = new UserViewModel(birthdayUser.UserID, birthdayUser.Username, birthdayUser.Birthdate);
            PresentListViewModel plvm = new PresentListViewModel();
            plvm.Presents = this.GetPresentsViewModel(bal.GetPresents());
            VotingUserViewModel vvm = new VotingUserViewModel();
            vvm.User = uvm;
            vvm.Presents = plvm;

            bal.InitializeVoting(currentLogged.UserID, birthdayUser.UserID);

            return View("Index",vvm);
        }
        public ActionResult NotAdminIndex()
        {
            BusinessLayer bal = new BusinessLayer();

            User currentLogged = bal.GetUserByUsername(User.Identity.Name);
            if (bal.IsUserAlreadyVoted(currentLogged.UserID))
            {
                VoteListViewModel vlvm=new VoteListViewModel();
                vlvm.VoteViewModels=this.GetVoteViewModel(bal.GetVotes());
                return View("VotingResults", vlvm);
            }

            int userID = bal.GetBirthdayUserID();
            User birthdayUser = bal.GetUserById(userID);
            UserViewModel uvm = new UserViewModel(birthdayUser.UserID, birthdayUser.Username, birthdayUser.Birthdate);
            PresentListViewModel plvm = new PresentListViewModel();
            plvm.Presents = this.GetPresentsViewModel(bal.GetPresents());
            VotingUserViewModel vvm = new VotingUserViewModel();
            vvm.User = uvm;
            vvm.Presents = plvm;

            //bal.InitializeVoting(currentLogged.UserID, birthdayUser.UserID);

            return View("Index", vvm);
        }
        public ActionResult AdminIndex()
        {
            BusinessLayer bal = new BusinessLayer();

            User currentLogged = bal.GetUserByUsername(User.Identity.Name);
            if (bal.IsUserAlreadyVoted(currentLogged.UserID))
            {
                VoteListViewModel vlvm = new VoteListViewModel();
                vlvm.VoteViewModels = this.GetVoteViewModel(bal.GetVotes());
                return View("VotingResultsAdmin", vlvm);
            }
            int userID = bal.GetBirthdayUserID();
            User birthdayUser = bal.GetUserById(userID);
            UserViewModel uvm = new UserViewModel(birthdayUser.UserID, birthdayUser.Username, birthdayUser.Birthdate);
            PresentListViewModel plvm = new PresentListViewModel();
            plvm.Presents = this.GetPresentsViewModel(bal.GetPresents());
            VotingUserViewModel vvm = new VotingUserViewModel();
            vvm.User = uvm;
            vvm.Presents = plvm;

            //bal.InitializeVoting(currentLogged.UserID, birthdayUser.UserID);

            return View("IndexAdmin", vvm);

        }
        public ActionResult StopVoting()
        {
            BusinessLayer bal = new BusinessLayer();
            DataTable dt = bal.StopVoting();
            return View("FinalVoteResults", dt);
        }
        public ActionResult VotingResults()
        {
            int presentID = Convert.ToInt32(Request.Form["Present"]);

            BusinessLayer bal = new BusinessLayer();
            User currentLogged = bal.GetUserByUsername(User.Identity.Name);

            bal.VoteForPresent(currentLogged.UserID, presentID);

            VoteListViewModel vlvm = new VoteListViewModel();
            vlvm.VoteViewModels = this.GetVoteViewModel(bal.GetVotes());
            
            return View("VotingResults",vlvm);
        }
        public ActionResult BirthdayUser()
        {
            return View("BirthdayUser");
        }
        private List<VoteViewModel> GetVoteViewModel(List<Vote> votes)
        {
            BusinessLayer bl = new BusinessLayer();
            List<VoteViewModel> lvvm = new List<VoteViewModel>();
            foreach (Vote vote in votes)
            {
                User user = bl.GetUserById(vote.UserID);
                Present present=bl.GetPresentById(vote.PresentID);
                VoteViewModel vvm = new VoteViewModel(user.UserID, present.PresentID);
                vvm.PresentName = present.PresentName;
                vvm.Username = user.Username;
                lvvm.Add(vvm);
            }
            return lvvm;
        }
        private List<PresentViewModel> GetPresentsViewModel(List<Present> presents)
        {
            List<PresentViewModel> listOfpvm = new List<PresentViewModel>();
            foreach (Present present in presents)
            {
                listOfpvm.Add(new PresentViewModel(present.PresentID, present.PresentName));
            }
            return listOfpvm;
        }
    }
}
