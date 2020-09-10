using Notes.Data;
using Notes.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        public string EntryName { get; set; } = string.Empty;
        public ObservableCollection<Note> Notes { get; set; }
        private readonly INoteRepository NoteRepository;

        public NotesPage(INoteRepository noteRepository)
        {
            InitializeComponent();
            NoteRepository = noteRepository;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var notes = NoteRepository.GetNotes();
            Notes = new ObservableCollection<Note>(notes);
            listView.ItemsSource = Notes;
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (e.SelectedItem is Todo)
                {
                    await Navigation.PushAsync(new TodoEntryPage
                    {
                        BindingContext = e.SelectedItem as Todo
                    });
                }
                else if (e.SelectedItem is Note)
                {
                    await Navigation.PushAsync(new NoteEntryPage
                    {
                        NoteRepository = NoteRepository,
                        BindingContext = e.SelectedItem as Note
                    });
                }
            }
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            string name = EntryName.Trim();
            if (NoteExists(name))
                await DisplayAlert("Note Already Exists", $"{name} already exists.", "Cancel");
            else if (!string.IsNullOrEmpty(name))
            {
                await Navigation.PushAsync(new NoteEntryPage
                {
                    NoteRepository = NoteRepository,
                    BindingContext = new Note { Name = name }
                });
                EntryName = string.Empty;
            }
        }

        private bool NoteExists(string name) => Notes.Any(note => note.Name == name);

        async void OnTodoAdded_Clicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text.Trim();
            if (name.Length > 0)
            {
                await Navigation.PushAsync(new TodoEntryPage
                {
                    BindingContext = new Todo { Name = name, Tasks = new ObservableCollection<Task>() }
                });
                NameEntry.Text = "";
            }
        }

        private void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}