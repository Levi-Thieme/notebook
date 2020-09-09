using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Task
    {
        public Task(string taskName)
        {
            this.Name = taskName;
        }
        public string Name { get; set; }
    }
}
