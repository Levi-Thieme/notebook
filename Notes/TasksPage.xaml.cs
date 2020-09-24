using Notes.ViewModels;
using Xamarin.Forms;

namespace Notes
{
    public partial class TasksPage : ContentPage
    {
        private readonly TodoViewModel ViewModel;

        public TasksPage(TodoViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.DeleteTaskCommand.Execute(e.Item.ToString());
        }
    }
}