using SQLite;
using System;

namespace Notes.Models
{
    public class Todo
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string LastModified { get; set; }

        public Todo()
        {
            Name = string.Empty;
            LastModified = DateTime.Now.ToString();
        }

        public Todo(string name)
        {
            Name = name;
            LastModified = DateTime.Now.ToString();
        }
    }
}
