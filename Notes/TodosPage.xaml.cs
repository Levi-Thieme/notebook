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
        ITodoRepository TodoRepository;

        public TodosPage(ITodoRepository todoRepository)
        {
            BindingContext = new TodosViewModel(todoRepository);
            TodoRepository = todoRepository;
            InitializeComponent();
        }

        private void TodoSelected(object sender, ItemTappedEventArgs e)
        {
            var tappedTodo = e.Item as Todo;
            Navigation.PushAsync(new TasksPage(new TodoViewModel(tappedTodo?.Name, TodoRepository)));
        }
    }
}