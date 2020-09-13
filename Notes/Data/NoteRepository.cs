using Notes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Notes.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly SQLiteConnection database;
        private readonly TableQuery<Note> notes;

        public NoteRepository()
        {
            var databaseConnectionService = DependencyService.Get<IDatabaseConnection>();
            database = databaseConnectionService.Create();
            database.CreateTable<Note>();
            notes = database.Table<Note>();
        }

        public IEnumerable<Note> GetNotes()
        {
            return notes.ToList();
        }

        public void SaveNote(Note note)
        {
            note.LastModified = DateTime.Now.ToString();
            if (notes.FirstOrDefault(n => n.Name == note.Name) != null)
                database.Update(note);
            else
                database.Insert(note);
        }

        public void DeleteNote(Note note)
        {
            database.Delete(note);
        }
    }
}
