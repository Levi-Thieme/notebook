﻿using Notes.Data;
using Notes.Models;
using System;
using System.Collections.ObjectModel;
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
        private INoteRepository NoteRepository;

        public NoteViewModel(INoteRepository noteRepository)
        {
            name = string.Empty;
            NoteRepository = noteRepository;
            Notes = new ObservableCollection<Note>(NoteRepository.GetNotes());
            CreateNoteCommand = new Command<string>(CreateNote, IsValidName);
            DeleteNoteCommand = new Command<Note>(DeleteNote);
        }

        private void CreateNote(string name)
        {
            var note = new Note { Name = name, LastModified = DateTime.Now.ToString(), Content = string.Empty };
            NewNoteName = string.Empty;
            NoteRepository.SaveNote(note);
            Notes.Add(note);
        }

        private bool IsValidName(string name) => name != null && name.Trim() != string.Empty;

        private void DeleteNote(Note note)
        {
            Notes.Remove(note);
            NoteRepository.DeleteNote(note);
        }
    }
}