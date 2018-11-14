using LionFishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionFishWeb.Repositories
{
    public interface IUserRepo
    {
        bool AddUser(User user);
        bool DeleteUserByID(int userid);
        bool DeleteUserByEmail(string email);

        User GetUserByID(int userid);
        List<User> GetUsersByGroup(int groupid);
    }
}
