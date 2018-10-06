using System;
using System.Collections.Generic;
using Vitae.Services;

namespace Vitae.Model
{
    public class ExpertiseRepository : IExpertiseRepository
    {
        IExpertiseXMLService xs;

        public ExpertiseRepository(IExpertiseXMLService xmlService) 
        {
            xs = xmlService;
        }

        public Guid Add(IExpertiseEntity entity) 
        {
            try
            {
                return xs.Insert(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IExpertiseEntity Get(Guid guid) 
        {
            try
            {
                return xs.Get(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IExpertiseEntity> GetAll() 
        {
            try
            {
                return xs.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(Guid g) 
        {
            try
            {
                xs.Delete(g);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid g, IExpertiseEntity t) 
        {
            try
            {
                xs.Update(g, t);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
