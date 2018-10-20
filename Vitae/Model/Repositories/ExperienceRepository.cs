namespace Vitae.Model
{
    using Ninject;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ExperienceRepository : IExperienceRepository
    {
        private readonly IExperienceXMLService xs;
        private readonly IKernel kernel;

        public ExperienceRepository(IExperienceXMLService xmlService,
                                    IKernel kernel) 

        {
            xs = xmlService;
            this.kernel = kernel;
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

        public IList<IDecoratedExperienceEntity> GetAllDecorators() 
        {
            try
            {
                var temp = xs.GetAll();
                var listToReturn = new List<IDecoratedExperienceEntity>();
                foreach (var item in temp)
                {
                    var decorated = kernel.Get<IDecoratedExperienceEntity>();
                    decorated.ID = item.ID;
                    decorated.Employer = item.Employer;
                    decorated.Titles = item.Titles;
                    decorated.StartDate = item.StartDate;
                    decorated.EndDate = item.EndDate;
                    decorated.Details = new ObservableCollection<string>(item.Details);
                    listToReturn.Add(decorated);
                }
                return listToReturn;
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
                var list = new List<IExperienceItem>();

                foreach (var job in xs.GetAll())
                {
                    foreach (var detail in job.Details)
                    {
                        var item = kernel.Get<IExperienceItem>();
                        item.Employer = job.Employer;
                        item.ExperienceDetail = detail;
                        list.Add(item);
                    }
                }

                return list;
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

        public IList<IJobTitle> GetAllJobTitles() 
        {
            try
            {
                var list = new List<IJobTitle>();

                foreach (var job in xs.GetAll())
                {
                    foreach (var title in job.Titles)
                    {
                        var item = kernel.Get<IJobTitle>();
                        item.Employer = job.Employer;
                        item.Title = title;
                        list.Add(item);
                    }
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IJobTitle> GetJobTitlesForJob(Guid jobID) 
        {
            try
            {
                List<IJobTitle> list = new List<IJobTitle>();

                var job = GetAll().FirstOrDefault(T => T.ID.ToString() == jobID.ToString());

                foreach (var title in job.Titles)
                {
                    var titleEntity = kernel.Get<IJobTitle>();
                    titleEntity.Employer = job.Employer;
                    titleEntity.Title = title;
                    list.Add(titleEntity);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}