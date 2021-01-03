using Notes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Notes.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly SQLiteConnection database;
        private readonly TableQuery<Todo> todos;
        private readonly TableQuery<Task> tasks;


        public TodoRepository()
        {
            var databaseConnectionService = DependencyService.Get<IDatabaseConnection>();
            database = databaseConnectionService.Create();
            database.CreateTable<Task>();
            database.CreateTable<Todo>();
            todos = database.Table<Todo>();
            tasks = database.Table<Task>();
        }

        public IEnumerable<Todo> All()
        {
            return todos.ToList();
        }

        public void Save(Todo todo)
        {
            todo.LastModified = DateTime.Now.ToString();
            if (todos.FirstOrDefault(t => t.Name == todo.Name) != null)
                database.Update(todo);
            else
                database.Insert(todo);
        }

        public void Delete(Todo todo)
        {
            tasks.Delete(task => task.TodoName == todo.Name);
            database.Delete(todo);
        }

        public List<Task> GetTasks(string todoName)
        {
            var t = tasks.Where(task => task.TodoName == todoName).ToList();
            return t;
        }

        public void DeleteTask(Task task)
        {
            database.Delete(task);
        }

        public void CreateTask(string todoName, string name)
        {
            var newTask = new Task(todoName, name);
            database.Insert(newTask, newTask.GetType());
        }
    }
}
