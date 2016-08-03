using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DAIS.Models;
using System.Data;

namespace DAIS.DataAccessLayer
{
    public class PresentsDB
    {
        private SqlConnection connection;

        public PresentsDB()
        {
            this.connection=new SqlConnection("Data Source=МАРИО-PC;Initial Catalog=PresentsDB;Integrated Security=True");
        }
        
        public List<User> Users
        {
            get
            {
                return this.GetUsersFromDatabase();
            }
        }
        public List<Present> Presents
        {
            get
            {
                return this.GetPresentsFromDatabase();
            }
        }
        public List<Vote> Votes
        {
            get
            {
                return this.GetVoteFromDatabase();
            }
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            User user = null;
            string query = string.Format("SELECT * FROM Users WHERE Username='{0}' AND Pass='{1}'", username, password);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return user;
                    }
                    if (reader.Read())
                    {
                        int userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string usern = reader.GetString(reader.GetOrdinal("Username"));
                        string pass = reader.GetString(reader.GetOrdinal("Pass"));
                        DateTime birthdate = reader.GetDateTime(reader.GetOrdinal("BirthDate")).Date;
                        user = new User(userID, usern, pass, birthdate);
                    }
                    this.connection.Close();
                    return user;
                }
            }
        }
        public User GetUserByUsername(string username)
        {
            User user=null;
            string query = string.Format("SELECT * FROM Users WHERE Username='{0}'", username);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string usern = reader.GetString(reader.GetOrdinal("Username"));
                        string pass = reader.GetString(reader.GetOrdinal("Pass"));
                        DateTime birthdate = reader.GetDateTime(reader.GetOrdinal("BirthDate")).Date;
                        user = new User(userID, usern, pass, birthdate);
                    }
                    this.connection.Close();
                    return user;
                }
            }
        }
        public User GetUserByID(int id)
        {
            User user = null;
            string query = string.Format("SELECT * FROM Users WHERE UserID='{0}'", id);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string usern = reader.GetString(reader.GetOrdinal("Username"));
                        string pass = reader.GetString(reader.GetOrdinal("Pass"));
                        DateTime birthdate = reader.GetDateTime(reader.GetOrdinal("BirthDate")).Date;
                        user = new User(userID, usern, pass, birthdate);
                    }
                    this.connection.Close();
                    return user;
                }
            }
        }
        public Present GetPresentByID(int id)
        {
            Present present = null;
            string query = string.Format("SELECT * FROM Presents WHERE PresentID='{0}'", id);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int presentID = reader.GetInt32(reader.GetOrdinal("PresentID"));
                        string presentName = reader.GetString(reader.GetOrdinal("PresentName"));

                        present = new Present(presentID, presentName);
                    }
                    this.connection.Close();
                    return present;
                }
            }
        }
        public void InsertDataForVoting(int currentLoggedID, int birthdayUserID)
        {
            List<User> users = this.Users;
            foreach (User user in users)
            {
                if(!user.UserID.Equals(birthdayUserID))
                {
                string query = string.Format("INSERT INTO Vote(UserID,AdminID,BirthdayUserID) VALUES('{0}','{1}','{2}')",
                    user.UserID,currentLoggedID,birthdayUserID);
                using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
                {
                    this.connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    this.connection.Close();
                }
                }
            }
        }
        public void VoteForPresent(int currentLoggedID, int presentID)
        {
            string query = string.Format("UPDATE Vote SET PresentID={0} WHERE UserID={1}",
                presentID, currentLoggedID);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                sqlCmd.ExecuteNonQuery();
                this.connection.Close();
            }
        }
        public int GetVoteRowsCount()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Vote";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                count = (Int32)sqlCmd.ExecuteScalar();
                this.connection.Close();
            }
            return count;
        }
        public void DeleteVoteData()
        {
            string query = "DELETE FROM Vote";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                sqlCmd.ExecuteNonQuery();
                this.connection.Close();
            }
        }
        public int GetVotesFromUser(int currentLoggedID)
        {
            int count = 0;
            string query = 
                string.Format("SELECT COUNT(*) FROM Vote WHERE UserID={0} AND PresentID IS NOT NULL",
                currentLoggedID);
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                count = (Int32)sqlCmd.ExecuteScalar();
                this.connection.Close();
            }
            return count;
        }
        public int GetBirthdayUserID()
        {
            int birthdayUserID = 0;
            string query = String.Format("SELECT TOP 1 BirthdayUserID FROM Vote");
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                birthdayUserID = (Int32)sqlCmd.ExecuteScalar();
                this.connection.Close();
            }
            return birthdayUserID;
        }
        public int GetAdminUserID()
        {
            int adminUserID = 0;
            string query = String.Format("SELECT TOP 1 AdminID FROM Vote");
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                adminUserID = (Int32)sqlCmd.ExecuteScalar();
                this.connection.Close();
            }
            return adminUserID;
        }
        public DataTable GetVoteTable()
        {
            DataTable dt = new DataTable();
            string query = "SELECT Users.Username, Presents.PresentName " +
                            "FROM Vote INNER JOIN Users ON " +
                            "Vote.UserID=Users.UserID INNER JOIN " +
                            "Presents ON Vote.PresentID=Presents.PresentID";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                {
                    sqlAdapter.Fill(dt);
                }
                this.connection.Close();
            }
            return dt;
        }
        private List<User> GetUsersFromDatabase()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM Users";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        string username = reader.GetString(reader.GetOrdinal("Username"));
                        string password = reader.GetString(reader.GetOrdinal("Pass"));
                        DateTime birthdate = reader.GetDateTime(reader.GetOrdinal("BirthDate")).Date;
                        User user = new User(userID, username, password, birthdate);
                        users.Add(user);
                    }
                }
                this.connection.Close();
            }
            return users;
        }
        private List<Present> GetPresentsFromDatabase()
        {
            List<Present> presents = new List<Present>();
            string query = "SELECT * FROM Presents";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int presentID = reader.GetInt32(reader.GetOrdinal("PresentID"));
                        string presentName = reader.GetString(reader.GetOrdinal("PresentName"));
                        Present present = new Present(presentID, presentName);
                        presents.Add(present);
                    }
                }
                this.connection.Close();
            }
            return presents;
        }
        private List<Vote> GetVoteFromDatabase()
        {
            List<Vote> votes = new List<Vote>();
            string query = "SELECT * FROM Vote WHERE PresentID IS NOT NULL";
            using (SqlCommand sqlCmd = new SqlCommand(query, this.connection))
            {
                this.connection.Open();
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        int presentID = reader.GetInt32(reader.GetOrdinal("PresentID"));
                        int adminID = reader.GetInt32(reader.GetOrdinal("AdminID"));
                        int birthdayUserID = reader.GetInt32(reader.GetOrdinal("BirthdayUserID"));

                        Vote vote = new Vote(userID, presentID, adminID, birthdayUserID);
                        votes.Add(vote);
                    }
                }
                this.connection.Close();
            }
            return votes;
        }
        public int GetVoteStoppedColumnsCount()
        {
            int count = 0;
            string query = "SELECT COUNT(IsStoppedVoting) FROM Vote WHERE IsStoppedVoting=1";
            using(SqlCommand sqlCmd=new SqlCommand(query,this.connection))
            {
                this.connection.Open();
                count=(Int32)sqlCmd.ExecuteScalar();
                this.connection.Close();
            }
            return count;
        }
        public void StopVoting()
        {
            string query = "UPDATE Vote SET IsStoppedVoting=1 WHERE IsStoppedVoting=0";
            using(SqlCommand sqlCmd=new SqlCommand(query,this.connection))
            {
                this.connection.Open();
                sqlCmd.ExecuteNonQuery();
                this.connection.Close();
            }
        }
    }
}