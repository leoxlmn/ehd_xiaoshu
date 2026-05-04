
using System.Collections.Generic;
using JCManagement.Model.Entity;

namespace JCManagement.Repository {
    public interface IRepository<T> where T: EntityBase {
        T GetById(int id);
        IList<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        bool Contains(T item);
        int Count { get; }
    }
}
