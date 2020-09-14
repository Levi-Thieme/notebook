using System.Collections.Generic;

namespace Notes.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        void Save(T note);
        void Delete(T note);
    }
}
