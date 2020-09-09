using System;
using Xamarin.Forms;
using Notes.Models;
using Notes.Data;

namespace Notes
{
    public partial class NoteEntryPage : ContentPage
    {
        public INoteRepository NoteRepository { get; internal set; }

        public NoteEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            NoteRepository.SaveNote((Note)BindingContext);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            NoteRepository.DeleteNote((Note)BindingContext);
            await Navigation.PopAsync();
        }
    }
}