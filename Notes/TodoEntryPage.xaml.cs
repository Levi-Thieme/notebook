using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Models;
using System.IO;

namespace Notes
{
    public partial class TodoEntryPage : ContentPage
    {
        public TodoEntryPage()
        {
            InitializeComponent();
        }

        private void OnAddTaskButton_Clicked(object sender, EventArgs e)
        {
            string name = taskEntry.Text.Trim();
            taskEntry.Text = "";
            if (name.Length > 0)
            {
                Todo todo = BindingContext as Todo;
                todo.AddTask(new Task(name));
            }
        }

        private void RemoveTappedItem(object sender, ItemTappedEventArgs e)
        {
            ((Todo)BindingContext).RemoveTask(e.Item as Task);
        }

        async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var todo = (Todo)BindingContext;
            
            await Navigation.PopAsync();
        }

        async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void DeleteEntryButton_Clicked(object sender, EventArgs e)
        {
        }
    }
}