namespace Vitae.Services
{
    using System;
    using System.Collections.Generic;
    using Vitae.Model;

    public interface IXMLService<T> where T : IEntity
    {
        T Get(Guid guid);
        IList<T> GetAll();
        Guid Insert(T entity);
        void Delete(Guid g);
        void Update(Guid guid, T entity);
    }
    }