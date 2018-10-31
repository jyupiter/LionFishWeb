using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LionFishWeb.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
    }
}