using System;
using System.Collections.Generic;
using System.Text;

namespace GoChat.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> getAll();
        T getById(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
