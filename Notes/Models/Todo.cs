using System.Collections.Generic;

namespace Notes.Models
{
    public class Todo : Note
    {
        public List<Task> Tasks { get; set; }

        public Todo()
        {
            Tasks = new List<Task>();
        }
        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }
    }
}
