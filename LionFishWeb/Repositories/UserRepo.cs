using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LionFishWeb.Models;
using LionFishWeb.Utility;
using MySql.Data.MySqlClient;

namespace LionFishWeb.Repositories
{
    public class UserRepo : IUserRepo
    {
        public bool AddUser(User user)
        {
            MySqlConnection connection = new MySqlConnection(Constants.conn);
            connection.Open();
            MySqlCommand command = new MySqlCommand("", connection)
            {
                CommandText = "create table tasks (task_id INT AUTO_INCREMENT, description TEXT, PRIMARY KEY(task_id))"
            };
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeleteUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserByID(int userid)
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int userid)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByGroup(int groupid)
        {
            throw new NotImplementedException();
        }
    }
}