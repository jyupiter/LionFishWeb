using LionFishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionFishWeb.Repositories
{
    interface INoteRepo
    {
        bool AddNote(Note note);
        bool DeleteNoteByID(int noteid);

        bool GetNoteByID(int noteid);
        bool GetNotesByUser(int userid);
        bool GetNotesByEvent(int eventid);
    }
}
