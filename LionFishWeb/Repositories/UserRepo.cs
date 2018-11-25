using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using LionFishWeb.Models;
using LionFishWeb.Utility;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace LionFishWeb.Repositories
{
    public class UserRepo : IUserRepo
    {
        public bool AddUser(User user)
        {
            int conv;
            if (user.IsConfirmed) conv = 1;
            else conv = 0;

            string query = "insert into user(email, pass, displayname, ispublic) values (@email, @pass, @displayname, @ispublic)";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@pass", user.Pass);
                command.Parameters.AddWithValue("@displayname", user.DisplayName);
                command.Parameters.AddWithValue("@ispublic", conv);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public bool DeleteUserByID(int userid)
        {
            string query = "delete from user where userid = @userid";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userid", userid);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public bool DeleteUserByEmail(string email)
        {
            string query = "delete from user where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }
        
        public bool CheckUserByEmail(string email)
        {
            string query = "select * from user where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();

                    if (read.Read())
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public User GetUserByID(int userid)
        {
            string query = "select * from user where userid = @userid";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userid", userid);

                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    User u = new User();

                    read.GetOrdinal("userid");
                    read.GetOrdinal("email");
                    read.GetOrdinal("pass");
                    read.GetOrdinal("signedup");
                    read.GetOrdinal("displayname");
                    read.GetOrdinal("profileimg");
                    read.GetOrdinal("profilebio");
                    read.GetOrdinal("ispublic");
                    // read.GetOrdinal("friendlist");

                    while (read.Read())
                    {
                        u.UserId = read.GetInt32("userid");
                        u.Email = read.GetString("email");
                        u.Pass = read.GetString("pass");
                        u.SignedUp = read.GetDateTime("signedup");
                        u.DisplayName = read.GetString("displayname");
                        u.ProfileImg = read.GetString("profileimg");
                        u.ProfileBio = read.GetString("profilebio");
                        u.IsConfirmed = Convert.ToBoolean(read.GetInt16("isconfirmed"));
                    }
                    return u;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public User GetUserByEmail(string email)
        {
            string query = "select * from user where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    User u = new User();
                    
                    read.GetOrdinal("userid");
                    read.GetOrdinal("email");
                    read.GetOrdinal("pass");
                    read.GetOrdinal("signedup");
                    read.GetOrdinal("displayname");
                    read.GetOrdinal("profileimg");
                    read.GetOrdinal("profilebio");
                    read.GetOrdinal("ispublic");
                    // read.GetOrdinal("friendlist");

                    while (read.Read())
                    {
                        u.UserId = read.GetInt32("userid");
                        u.Email = read.GetString("email");
                        u.Pass = read.GetString("pass");
                        u.SignedUp = read.GetDateTime("signedup");
                        u.DisplayName = read.GetString("displayname");
                        u.ProfileImg = read.GetString("profileimg");
                        u.ProfileBio = read.GetString("profilebio");
                        u.IsConfirmed = Convert.ToBoolean(read.GetInt16("isconfirmed"));
                    }
                    return u;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public List<User> GetUsersByGroup(int groupid)
        {
            throw new NotImplementedException();
        }
    }
}