using SQLite;
using System;
using System.Collections.Generic;

namespace Notes.Models
{
    public class Todo
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string LastModified { get; set; }
        public List<string> Tasks { get; set; }

        public Todo()
        {
            Name = string.Empty;
            LastModified = DateTime.Now.ToString();
            Tasks = new List<string>();
        }

        public Todo(string name)
        {
            Name = name;
            LastModified = DateTime.Now.ToString();
            Tasks = new List<string>();
        }
    }
}
