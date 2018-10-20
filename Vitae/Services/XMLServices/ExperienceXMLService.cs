namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Vitae.Model;

    public class ExperienceXMLService : IExperienceXMLService
    {
        private readonly IKernel _kernel;
        private readonly IPersistenceService persister;

        public ExperienceXMLService(IKernel kernel, IPersistenceService persistenceService) 
        {
            _kernel = kernel;
            persister = persistenceService;
        }

        public void Delete(Guid g) 
        {
            var doc = persister.Load<IExperienceEntity>();

            var element = doc.Root.Elements("Job")
                .SingleOrDefault(T => T.Attribute("Guid").Value == g.ToString());

            element.Remove();

            persister.Persist<IExperienceEntity>(doc);
        }

        public IExperienceEntity Get(Guid guid) 
        {
            var doc = persister.Load<IExperienceEntity>();

            var eeXml = doc.Root.Elements("Job")
                .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            if (eeXml == null) return null;
            else return ConvertToObject(eeXml);
        }

        public IList<IExperienceEntity> GetAll() 
        {
            var list = new List<IExperienceEntity>();

            var doc = persister.Load<IExperienceEntity>();
            var jobElements = doc.Root.Elements("Job");
            foreach (var job in jobElements)
            {
                list.Add(ConvertToObject(job));
            }

            return list;
        }

        public Guid Insert(IExperienceEntity entity) 
        {
            var doc = persister.Load<IExperienceEntity>();

            var guid = Guid.NewGuid();
            var el = ConvertToXml(guid, entity);
            doc.Root.Add(el);
            persister.Persist<IExperienceEntity>(doc);

            return guid;
        }

        public void Update(Guid guid, IExperienceEntity entity) 
        {
            var doc = persister.Load<IExperienceEntity>();

            var job = doc.Root.Elements("Job").FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (job == null) throw new ArgumentException("guid not found.");
            job.ReplaceWith(ConvertToXml(guid, entity));

            persister.Persist<IExperienceEntity>(doc);
        }

        private IExperienceEntity ConvertToObject(XElement el) 
        {
            var ee = _kernel.Get<IExperienceEntity>();

            if (el.Attribute("Guid") != null)
            {
                if (Guid.TryParse(el.Attribute("Guid").Value, out Guid output))
                    ee.ID = output;
                else ee.ID = Guid.NewGuid();
            }

            if (el.Attribute("Employer") != null)
                ee.Employer = el.Attribute("Employer").Value;
            else ee.Employer = string.Empty;
            if (el.Attribute("StartDate") != null)
                ee.StartDate = el.Attribute("StartDate").Value;
            else ee.StartDate = string.Empty;
            if (el.Attribute("EndDate") != null)
                ee.EndDate = el.Attribute("EndDate").Value;
            else ee.EndDate = string.Empty;

            foreach (var title in el.Element("JobTitles").Elements("JobTitle"))
                ee.Titles.Add(title.Value);

            foreach (var detail in el.Element("Details").Elements("Detail"))
                ee.Details.Add(detail.Value);

            return ee;
        }

        private XElement ConvertToXml(Guid guid, IExperienceEntity entity) 
        {
            var el = new XElement("Job");
            el.Add(new XAttribute("Guid", guid.ToString()));
            el.Add(new XAttribute("Employer", entity.Employer));
            el.Add(new XAttribute("StartDate", entity.StartDate));
            el.Add(new XAttribute("EndDate", entity.EndDate));

            el.Add(new XElement("JobTitles"));
            foreach (var title in entity.Titles)
            {
                el.Element("JobTitles").Add(new XElement("JobTitle", title));
            }

            el.Add(new XElement("Details"));
            foreach (var detail in entity.Details)
            {
                el.Element("Details").Add(new XElement("Detail", detail));
            }

            return el;
        }
    }
}