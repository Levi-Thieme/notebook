using Notes.Data;
using Notes.Models;
using Notes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodosPage : ContentPage
    {
        public string NewTodoName { get; set; }
        IRepository<Todo> TodoRepository;

        public TodosPage(IRepository<Todo> todoRepository)
        {
            InitializeComponent();
            TodoRepository = todoRepository;
            BindingContext = new TodosViewModel(TodoRepository);
        }

        private void TodoSelected(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new TodoEntryPage());
        }
    }
}