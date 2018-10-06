using System.Collections.Generic;
using Vitae.Services;
using System;

namespace Vitae.Model
{
    public class EducationRepository : IEducationRepository
    {
        private IEducationXMLService xs;

        public EducationRepository(IEducationXMLService xmlService) 
        {
            if (xmlService == null) throw new ArgumentNullException("xmlService");
            else xs = xmlService;
        }

        public Guid Add(IEducationEntity entity) 
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

        public IList<IEducationEntity> GetAll() 
        {
            return xs.GetAll();
        }

        public IEducationEntity Get(Guid guid) 
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

        public void Update(Guid g, IEducationEntity t) 
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