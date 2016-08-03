using DAIS.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAIS.Models
{
    public class BusinessLayer
    {
        private PresentsDB db;

        public BusinessLayer()
        {
            this.db = new PresentsDB();
        }
        public List<User> GetUsers()
        {
            return db.Users;
        }
        public List<Present> GetPresents()
        {
            return db.Presents;
        }
        public List<Vote> GetVotes()
        {
            return db.Votes;
        }
        public UserStatus GetUserValidity(UserDetails user)
        {
            User u = db.GetUserByUsernameAndPassword(user.Username, user.Password);

            if (u == null)
            {
                return UserStatus.NonAuthenticatedUser;
            }
            return UserStatus.AuthenticatedUser;
        }
        public User GetUserByUsername(string name)
        {
            User user = db.GetUserByUsername(name);
            return user;
        }
        public User GetUserById(int id)
        {
            User user=db.GetUserByID(id);
            return user;
        }
        public Present GetPresentById(int presentID)
        {
            Present present = db.GetPresentByID(presentID);
            return present;
        }
        public void InitializeVoting(int currentLoggedID, int birthdayUserID)
        {
            this.db.DeleteVoteData();
            if (!this.HasVote())
            {
                this.db.InsertDataForVoting(currentLoggedID, birthdayUserID);
            }
        }
        public void VoteForPresent(int currentLoggedID, int presentID)
        {
            if (!this.IsUserAlreadyVoted(currentLoggedID))
            {
                db.VoteForPresent(currentLoggedID, presentID);
            }
        }
        public bool IsUserBirthdayUser(User user)
        {
            int birthdayUserID = db.GetBirthdayUserID();
            if (!user.UserID.Equals(birthdayUserID))
            {
                return false;
            }
            return true;
        }
        public bool HasVote()
        {
            if (db.GetVoteRowsCount()==0)
            {
                return false;
            }
            return true;
        }
        public bool IsUserAlreadyVoted(int currentLoggedID)
        {
            if (db.GetVotesFromUser(currentLoggedID) == 0)
            {
                return false;
            }
            return true;
        }
        public bool IsUserAdminUser(User user)
        {
            int adminUserID = db.GetAdminUserID();
            if (!user.UserID.Equals(adminUserID))
            {
                return false;
            }
            return true;
        }
        public int GetBirthdayUserID()
        {
            return db.GetBirthdayUserID();
        }
        public int GetAdminUserID()
        {
            return db.GetAdminUserID();
        }
        public DataTable GetVoteTable()
        {
            return db.GetVoteTable();
        }
        public DataTable StopVoting()
        {
            DataTable dt = this.GetVoteTable();
            db.StopVoting();
            return dt;
        }
        public bool IsVotingStopped()
        {
            if (db.GetVoteStoppedColumnsCount() == 0)
            {
                return false;
            }
            return true;
        }
    }
}