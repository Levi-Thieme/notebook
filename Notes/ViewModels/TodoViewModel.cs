using Notes.Data;
using Notes.Models;

namespace Notes.ViewModels
{
    public class TodoViewModel
    {
        public Todo Todo { get; set; }

        private IRepository<Todo> TodoRepository;

        public TodoViewModel(Todo todo, IRepository<Todo> todoRepository)
        {
            Todo = todo;
            TodoRepository = todoRepository;
        }
    }
}
