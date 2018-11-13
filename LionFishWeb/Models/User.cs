using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LionFishWeb.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required][RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }

        public string DisplayName { get; set; }
        public string ProfileImg { get; set; }
        public string ProfileBio { get; set; }
        public bool IsPublic { get; set; }

        public List<User> FriendList { get; set; }
        // public List<Group> GroupList { get; set; }

        bool AddUserToFriendList(User user, User friend)
        {
            try
            {
                user.FriendList.Add(friend);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        bool DeleteUserFromFriendList(User user, User friend)
        {
            try
            {
                user.FriendList.Remove(friend);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}