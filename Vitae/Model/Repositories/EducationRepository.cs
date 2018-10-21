namespace Vitae.Model
{
    using System.Collections.Generic;
    using Vitae.Services;
    using System;

    public class EducationRepository : IEducationRepository
    {
        private IEducationXMLService xs;

        public EducationRepository(IEducationXMLService xmlService) 
        {
            xs = xmlService;
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

        public void Remove(Guid guid) 
        {
            try
            {
                xs.Delete(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid guid, IEducationEntity entity) 
        {
            try
            {
                xs.Update(guid, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}