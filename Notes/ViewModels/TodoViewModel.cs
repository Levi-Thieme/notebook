using Notes.Data;
using Notes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        public ObservableCollection<Todo> Todos { get; set; }
        public ICommand CreateTodoCommand { get; set; }
        private IRepository<Todo> TodoRepository;

        public TodoViewModel(IRepository<Todo> todoRepository)
        {
            TodoRepository = todoRepository;
            CreateTodoCommand = new Command<string>(CreateTodo);
            Todos = new ObservableCollection<Todo>(TodoRepository.All());
        }

        private void CreateTodo(string name)
        {
            var todo = new Todo(name);
            TodoRepository.Save(todo);
            Todos.Add(todo);
        }
    }
}
