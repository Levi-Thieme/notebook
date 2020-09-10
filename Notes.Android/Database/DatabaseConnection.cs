using System.IO;

using Android.App;
using Notes.Data;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection))]
namespace Notes.Droid.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection Create()
        {
            var databaseFileName = "notes_db.db";
            string documentsDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsDirectoryPath, databaseFileName);

            if (!File.Exists(path))
            {
                CopyDatabaseFromAssetsToPath(databaseFileName, path);
            }
            var conn = new SQLiteConnection(path);
            return conn;
        }

        private void CopyDatabaseFromAssetsToPath(string sqliteFilename, string path)
        {
            using (var binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(sqliteFilename)))
            {
                using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        binaryWriter.Write(buffer, 0, length);
                    }
                }
            }
        }
    }
}