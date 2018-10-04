namespace Vitae.Model
{
    using Services;
    using System;
    using System.Collections.Generic;

    public class ExperienceRepository : IExperienceRepository
    {
        IXMLService xs;

        public ExperienceRepository(IXMLService xmlService) 
        {
            xs = xmlService;
        }

        public Guid Add(IExperienceEntity entity) 
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

        public IExperienceEntity Get(Guid guid) 
        {
            try
            {
                return xs.GetExperience(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IExperienceEntity> GetAll() 
        {
            try
            {
                return xs.GetAllExperiences();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IExperienceItem> GetAllExperienceItems() 
        {
            try
            {
                return xs.GetExperienceDetails();
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
                xs.DeleteExperience(g);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid g, IExperienceEntity t) 
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