using LionFishWeb.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.IO;

namespace LionFishWeb.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required][EmailAddress][RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }
        public DateTime SignedUp { get; set; }

        public string DisplayName { get; set; }
        public string ProfileImg { get; set; }
        public string ProfileBio { get; set; }
        public bool IsConfirmed { get; set; }

        public List<int> FriendList { get; set; }
        // public List<Group> GroupList { get; set; }
        // public List<Group> EventList { get; set; }

        public User() { }

        public User(string email, string pass)
        {
            Email = email;
            Pass = Constants.SaltPass(pass);
            DisplayName = email.Split('@')[0];
            IsConfirmed = false;
        }

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

        public bool AuthUser(User user, string test)
        {
            try
            {
                byte[] hbyt = Convert.FromBase64String(user.Pass);
                byte[] salt = new byte[16];
                Array.Copy(hbyt, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(test, salt, 250);
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                    if (hbyt[i + 16] != hash[i])
                        return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}