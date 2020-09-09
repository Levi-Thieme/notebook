using System;

namespace Notes.Models
{
    public class Note
    {
        private string text;
        public string Name {get; set;}
        public string Filename { get; set; }
        public string Text
        {
            get
            {
                if (this is Todo)
                {
                    string tasksString = "";
                    foreach(Task task in ((Todo)this).Tasks)
                    {
                        tasksString += task.Name + "\n";
                    }
                    return tasksString;
                }
                else
                {
                    return text;
                }
            }
            set { text = value; }
        }
        public DateTime Date { get; set; }
    }
}