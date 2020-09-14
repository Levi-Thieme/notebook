using System;

using Xamarin.Forms;
using Notes.Models;

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
                todo.Tasks.Add(name);
            }
        }

        private void RemoveTappedItem(object sender, ItemTappedEventArgs e)
        {
            ((Todo)BindingContext).Tasks.Remove(e.Item as string);
        }
    }
}