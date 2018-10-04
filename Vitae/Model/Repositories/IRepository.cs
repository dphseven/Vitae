using System;
using System.Collections.Generic;

namespace Vitae.Model
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T Get(Guid guid);
        Guid Add(T entity);
        void Remove(Guid g);
        void Update(Guid g, T t);
    }
}