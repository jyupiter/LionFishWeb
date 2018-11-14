using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LionFishWeb.Models;

namespace LionFishWeb.Repositories
{
    public class NoteRepo : INoteRepo
    {
        public bool AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNoteByID(int noteid)
        {
            throw new NotImplementedException();
        }

        public bool GetNoteByID(int noteid)
        {
            throw new NotImplementedException();
        }

        public bool GetNotesByEvent(int eventid)
        {
            throw new NotImplementedException();
        }

        public bool GetNotesByUser(int userid)
        {
            throw new NotImplementedException();
        }
    }
}