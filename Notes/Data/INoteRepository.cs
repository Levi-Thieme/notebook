using Notes.Models;
using System.Collections.Generic;

namespace Notes.Data
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes();
        void SaveNote(Note note);
        void DeleteNote(Note note);
    }
}
