using Notes.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Environment;

namespace Notes.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly string AppDataPath = GetFolderPath(SpecialFolder.LocalApplicationData);

        public IEnumerable<Note> GetNotes()
        {
            var files = GetNoteFiles();
            return files.Select(ConvertFileToNote);
        }

        public void SaveNote(Note note)
        {
            if (IsNewNote(note))
                SaveNewNote(note);
            else
                Save(note);
        }

        public void DeleteNote(Note note)
        {
            string notePath = GetNotePath(note.Filename);
            if (File.Exists(notePath))
                File.Delete(notePath);
        }

        private void SaveNewNote(Note note)
        {
            note.Filename = note.Name;
            Save(note);
        }

        private void Save(Note note)
        {
            FileInfo file = new FileInfo(GetNotePath(note.Filename));
            CreateOrOverwriteTextFile(file, note.Text);
        }

        private void CreateOrOverwriteTextFile(FileInfo file, string text)
        {
            File.WriteAllText(file.FullName, text);
        }

        private bool IsNewNote(Note note) => string.IsNullOrEmpty(note.Filename);

        private string GetNotePath(string filename) => Path.Combine(AppDataPath, filename);

        private IEnumerable<FileInfo> GetNoteFiles()
        {
            return Directory.EnumerateFiles(AppDataPath)
                .Select(filepath => new FileInfo(filepath));
        }

        private Note ConvertFileToNote(FileInfo file)
        {
            return new Note
            {
                Name = file.Name,
                Filename = file.Name,
                Date = file.LastWriteTime,
                Text = File.ReadAllText(file.FullName)
            };
        }
    }
}
