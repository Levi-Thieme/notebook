using SQLite;
using System;

namespace Notes.Models
{
    public class Note
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Content { get; set; }
        public string LastModified { get; set; }
    }
}