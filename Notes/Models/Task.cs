namespace Notes.Models
{
    public class Task
    {
        public string Name { get; set; }
        public string TodoName { get; set; }
        
        public Task(string todoName, string name)
        {
            Name = name;
            TodoName = todoName;
        }
    }
}
