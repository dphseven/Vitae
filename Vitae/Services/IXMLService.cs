namespace Vitae.Services
{
    using System;
    using System.Collections.Generic;

    public interface IXMLService<T> 
    {
        T Get(Guid guid);
        IList<T> GetAll();
        Guid Insert(T entity);
        void Delete(Guid g);
        void Update(Guid guid, T entity);
    }

}