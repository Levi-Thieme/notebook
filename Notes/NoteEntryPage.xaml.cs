using Xamarin.Forms;
using Notes.Models;
using Notes.Data;

namespace Notes
{
    public partial class NoteEntryPage : ContentPage
    {
        public IRepository<Note> NoteRepository { get; internal set; }

        public NoteEntryPage(IRepository<Note> noteRepository, Note note)
        {
            NoteRepository = noteRepository;
            BindingContext = note;
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NoteRepository.Save((Note)BindingContext);
        }
    }
}