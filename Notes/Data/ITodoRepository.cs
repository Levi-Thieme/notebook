using Notes.Models;
using System.Collections.Generic;

namespace Notes.Data
{
    public interface ITodoRepository : IRepository<Todo>
    {
        List<Task> GetTasks(string todoName);
        void DeleteTask(Task taskToDelete);
        void CreateTask(string todoName, string name);
    }
}
