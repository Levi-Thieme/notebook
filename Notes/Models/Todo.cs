using System.Collections.ObjectModel;

namespace Notes.Models
{
    public class Todo : Note
    {
        public ObservableCollection<Task> Tasks { get; set; }

        public Todo()
        {
            Tasks = new ObservableCollection<Task>();
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
