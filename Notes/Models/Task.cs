using SQLite;

namespace Notes.Models
{
    public class Task
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string TodoName { get; set; }
        
        public Task() { }

        public Task(string todoName, string name)
        {
            Name = name;
            TodoName = todoName;
        }
    }
}
