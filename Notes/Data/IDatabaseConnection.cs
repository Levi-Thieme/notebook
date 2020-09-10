using SQLite;

namespace Notes.Data
{
    public interface IDatabaseConnection
    {
        SQLiteConnection Create();
    }
}
