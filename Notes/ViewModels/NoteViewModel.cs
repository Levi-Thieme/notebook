using Notes.Data;
using Notes.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private string name;
        public string NewNoteName {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(NewNoteName)); }
        }
        public ObservableCollection<Note> Notes { get; set; }
        public ICommand CreateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        private IRepository<Note> NoteRepository;

        public NoteViewModel(IRepository<Note> noteRepository)
        {
            name = string.Empty;
            NoteRepository = noteRepository;
            Notes = new ObservableCollection<Note>(NoteRepository.All());
            CreateNoteCommand = new Command<string>(CreateNote, IsValidName);
            DeleteNoteCommand = new Command<Note>(DeleteNote);
        }

        private void CreateNote(string name)
        {
            var note = new Note { Name = name.Trim(), LastModified = DateTime.Now.ToString(), Content = string.Empty };
            NewNoteName = string.Empty;
            NoteRepository.Save(note);
            Notes.Add(note);
        }

        private bool IsValidName(string name)
        {
            return name != null
                && name.Trim() != string.Empty &&
                !NoteWithNameExists(name.Trim());
        }

        private bool NoteWithNameExists(string name) => Notes.FirstOrDefault(note => note.Name == name) != null;

        private void DeleteNote(Note note)
        {
            Notes.Remove(note);
            NoteRepository.Delete(note);
        }
    }
}
