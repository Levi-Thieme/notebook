using Notes.Data;
using SQLite;
using System.IO;
using Xamarin.Forms;
using static System.Environment;

[assembly: Dependency(typeof(DatabaseConnection))]
namespace Notes.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection Create()
        {
            string documentPath = GetFolderPath(SpecialFolder.LocalApplicationData);
            string path = Path.Combine(documentPath, "notes_db.db");
            return new SQLiteConnection(path);
        }
    }
}
