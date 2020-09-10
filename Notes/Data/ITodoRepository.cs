using Notes.Models;
using System.Collections.Generic;

namespace Notes.Data
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetTodos();
        void SaveNote(Todo note);
        void DeleteNote(Todo note);
    }
}
