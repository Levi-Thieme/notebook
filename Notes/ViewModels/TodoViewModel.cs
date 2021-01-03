using Notes.Data;
using Notes.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        private string newTaskName;
        public string NewTaskName
        {
            get { return newTaskName; }
            set { newTaskName = value; OnPropertyChanged(nameof(NewTaskName)); }
        }
        public string TodoName { get; set; }
        public List<Task> Tasks { get; set; }
        public ObservableCollection<string> Names { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand CreateTaskCommand { get; set; }

        private ITodoRepository TodoRepository;

        public TodoViewModel(string todoName, ITodoRepository todoRepository)
        {
            TodoName = todoName;
            Tasks = todoRepository.GetTasks(todoName);
            TodoRepository = todoRepository;
            DeleteTaskCommand = new Command<string>(DeleteTask);
            CreateTaskCommand = new Command<string>(CreateTask, IsValidName);
            Names = new ObservableCollection<string>(Tasks.Select(t => t.Name));
        }

        private void DeleteTask(string name)
        {
            Names.Remove(name);
            var task = Tasks.FirstOrDefault(t => t.Name == name);
            if (task != null)
            {
                TodoRepository.DeleteTask(task);
                Tasks.Remove(task);
            }
        }

        private void CreateTask(string name)
        {
            Tasks.Add(new Task(TodoName, name));
            Names.Add(name);
            NewTaskName = string.Empty;
            TodoRepository.CreateTask(TodoName, name);
        }

        private bool IsValidName(string name)
        {
            return name != null
                && name.Trim() != string.Empty &&
                !TaskWithNameExists(name.Trim());
        }

        private bool TaskWithNameExists(string name) => Tasks.FirstOrDefault(task => task.Name == name) != null;
    }
}
