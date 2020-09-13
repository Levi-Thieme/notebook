using Notes.Data;
using Notes.Models;
using System.Collections.ObjectModel;

namespace Notes.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        public ObservableCollection<Todo> Todos { get; set; }
        private ITodoRepository TodoRepository;

        public TodoViewModel(ITodoRepository todoRepository)
        {
            TodoRepository = todoRepository;
            Todos = new ObservableCollection<Todo>(TodoRepository.GetTodos());
        }
    }
}
