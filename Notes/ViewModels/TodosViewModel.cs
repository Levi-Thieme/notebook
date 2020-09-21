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
        public ObservableCollection<Todo> Todos { get; set; }
        public ICommand CreateTodoCommand { get; set; }
        public ICommand DeleteTodoCommand { get; set; }
        private IRepository<Todo> TodoRepository;

        public TodosViewModel(IRepository<Todo> todoRepository)
        {
            TodoRepository = todoRepository;
            CreateTodoCommand = new Command<string>(CreateTodo);
            DeleteTodoCommand = new Command<string>(DeleteTodo);
            Todos = new ObservableCollection<Todo>(TodoRepository.All());
        }

        private void CreateTodo(string name)
        {
            var todo = new Todo(name);
            TodoRepository.Save(todo);
            Todos.Add(todo);
        }

        private void DeleteTodo(string name)
        {
            var todoToDelete = Todos.FirstOrDefault(todo => todo.Name == name);
            if (todoToDelete != null)
            {
                TodoRepository.Delete(todoToDelete);
                Todos.Remove(todoToDelete);
            }
        }
    }
}
