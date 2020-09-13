using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using Xamarin.Forms;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        INoteRepository NoteRepository;

        public NotesPage(INoteRepository noteRepository)
        {
            InitializeComponent();
            BindingContext = new NoteViewModel(noteRepository);
            NoteRepository = noteRepository;
        }

        private void NoteSelected(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new NoteEntryPage
            {
                NoteRepository = NoteRepository,
                BindingContext = e.Item as Note
            });
        }
    }
}