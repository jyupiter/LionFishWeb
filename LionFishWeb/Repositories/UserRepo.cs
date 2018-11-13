using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LionFishWeb.Models;

namespace LionFishWeb.Repositories
{
    public class UserRepo : IUserRepo
    {
        public bool AddUser(User user)
        {
            throw new NotImplementedException();
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