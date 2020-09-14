﻿using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using Xamarin.Forms;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        IRepository<Note> NoteRepository;

        public NotesPage(IRepository<Note> noteRepository)
        {
            InitializeComponent();
            BindingContext = new NoteViewModel(noteRepository);
            NoteRepository = noteRepository;
        }

        private void NoteSelected(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new NoteEntryPage(NoteRepository, e.Item as Note));
        }
    }
}