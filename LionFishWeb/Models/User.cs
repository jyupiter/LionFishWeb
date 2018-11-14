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
        public DateTime SignedUp { get; set; }

        public string DisplayName { get; set; }
        public string ProfileImg { get; set; }
        public string ProfileBio { get; set; }
        public bool IsPublic { get; set; }

        public List<int> FriendList { get; set; }
        // public List<Group> GroupList { get; set; }
        // public List<Group> EventList { get; set; }

        public bool AddUserToFriendList(User user, User friend)
        {
            try
            {
                user.FriendList.Add(friend.UserId);
                return true;
            }
            catch(Exception e)
            {
                e.ToString();
                return false;
            }
        }
        public bool DeleteUserFromFriendList(User user, User friend)
        {
            try
            {
                user.FriendList.Remove(friend.UserId);
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
    }
}