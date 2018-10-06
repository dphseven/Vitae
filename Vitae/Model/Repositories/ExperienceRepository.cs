namespace Vitae.Model
{
    using Ninject;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExperienceRepository : IExperienceRepository
    {
        IExperienceXMLService xs;

        public ExperienceRepository(IExperienceXMLService xmlService) 
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
                return xs.Get(guid);
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
                return xs.GetAll();
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
                using (var ioc = new VitaeNinjectKernel())
                {
                    var list = new List<IExperienceItem>();

                    foreach (var job in xs.GetAll())
                    {
                        foreach (var detail in job.Details)
                        {
                            var item = ioc.Get<IExperienceItem>();
                            item.Employer = job.Employer;
                            item.ExperienceDetail = detail;
                            list.Add(item);
                        }
                    }

                    return list;
                }
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

        public IList<IExperienceItem> GetExperienceDetailsForEmployer(string Employer) 
        {
            return GetAllExperienceItems().Where(T => T.Employer == Employer).ToList();
        }
    }
}