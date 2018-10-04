using System;
using System.Collections.Generic;
using Vitae.Services;

namespace Vitae.Model 
{
    public class PublicationsRepository : IPublicationsRepository 
    {
        IXMLService xs;

        public PublicationsRepository(IXMLService xmlService) 
        {
            xs = xmlService;
        }

        public Guid Add(IPublicationEntity entity) 
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

        public IPublicationEntity Get(Guid guid) 
        {
            try
            {
                return xs.GetPublication(guid); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IPublicationEntity> GetAll() 
        {
            try
            {
                return xs.GetPublications();
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
                xs.DeletePublication(g);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid g, IPublicationEntity t)
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