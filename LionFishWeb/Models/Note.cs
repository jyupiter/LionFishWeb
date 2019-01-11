using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LionFishWeb.Models
{
    public class Note
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public List<List<String>> BackUp { get; set; }
        // public string Style { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }

        public Note(User user)
        {
            UserID = user.UserId;
        }
    }
}