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


        public TodoRepository()
        {
            var databaseConnectionService = DependencyService.Get<IDatabaseConnection>();
            database = databaseConnectionService.Create();
            database.CreateTable<Todo>();
            todos = database.Table<Todo>();
        }

        public void DeleteNote(Todo todo)
        {
            database.Delete<Todo>(todo);
        }

        public IEnumerable<Todo> GetTodos()
        {
            return todos.ToList();
        }

        public void SaveNote(Todo todo)
        {
            todo.LastModified = DateTime.Now.ToString();
            if (todos.FirstOrDefault(t => t.Name == todo.Name) != null)
                database.Update(todo);
            else
                database.Insert(todo);
        }
    }
}
