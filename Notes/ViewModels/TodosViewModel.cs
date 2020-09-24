using Notes.Data;
using Notes.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class TodosViewModel : ViewModelBase
    {
        private string name;
        public string NewTodoName
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(NewTodoName)); }
        }
        public ObservableCollection<Todo> Todos { get; set; }
        public ICommand CreateTodoCommand { get; set; }
        public ICommand DeleteTodoCommand { get; set; }
        private IRepository<Todo> TodoRepository;

        public TodosViewModel(IRepository<Todo> todoRepository)
        {
            name = string.Empty;
            TodoRepository = todoRepository;
            CreateTodoCommand = new Command<string>(CreateTodo, IsValidName);
            DeleteTodoCommand = new Command<Todo>(DeleteTodo);
            Todos = new ObservableCollection<Todo>(TodoRepository.All());
        }

        private void CreateTodo(string name)
        {
            var todo = new Todo(name);
            TodoRepository.Save(todo);
            Todos.Add(todo);
            NewTodoName = string.Empty;
        }

        private bool IsValidName(string name)
        {
            return name != null
                && name.Trim() != string.Empty &&
                !TodoWithNameExists(name.Trim());
        }

        private bool TodoWithNameExists(string name) => Todos.FirstOrDefault(todo => todo.Name == name) != null;

        private void DeleteTodo(Todo todo)
        {
            TodoRepository.Delete(todo);
            Todos.Remove(todo);
        }
    }
}
