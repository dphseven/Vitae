namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Vitae.Model;

    public class ExperienceXMLService : IExperienceXMLService
    {
        private readonly string experienceFilePath;

        public ExperienceXMLService() 
        {
            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            experienceFilePath = prefix + @"\XML\Experience.xml";
        }

        public void Delete(Guid g) 
        {
            var doc = XDocument.Load(experienceFilePath);

            var element = doc.Root.Elements("Job")
                .SingleOrDefault(T => T.Attribute("Guid").Value == g.ToString());

            element.Remove();

            doc.Save(experienceFilePath);
        }

        public IExperienceEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(experienceFilePath);

            var eeXml = doc.Root.Elements("Job").SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            using (var ioc = new VitaeNinjectKernel())
            {
                var ee = ioc.Get<IExperienceEntity>();
                ee.Employer = eeXml.Attribute("Employer").Value;
                ee.StartDate = eeXml.Attribute("StartDate").Value;
                ee.EndDate = eeXml.Attribute("EndDate").Value;

                foreach (var title in eeXml.Element("JobTitles").Elements("JobTitle"))
                    ee.Titles.Add(title.Value);

                foreach (var detail in eeXml.Element("Details").Elements("Detail"))
                    ee.Details.Add(detail.Value);

                return ee;
            }
        }

        public IList<IExperienceEntity> GetAll() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IExperienceEntity>();

                XDocument xDoc = XDocument.Load(experienceFilePath);
                var jobElements = xDoc.Root.Elements("Job");
                foreach (var job in jobElements)
                {
                    var entity = ioc.Get<IExperienceEntity>();
                    entity.Employer = job.Attribute("Employer").Value;
                    entity.StartDate = job.Attribute("StartDate").Value;
                    entity.EndDate = job.Attribute("EndDate").Value;

                    var titleElements = job.Element("JobTitles").Elements("JobTitle");
                    foreach (var title in titleElements)
                    {
                        entity.Titles.Add(title.Value);
                    }

                    var detailElements = job.Element("Details").Elements("Detail");
                    foreach (var detail in detailElements)
                    {
                        entity.Details.Add(detail.Value);
                    }

                    list.Add(entity);
                }

                return list;
            }
        }

        public Guid Insert(IExperienceEntity entity) 
        {
            var guid = Guid.NewGuid();

            var doc = XDocument.Load(experienceFilePath);

            var el = new XElement("Job");
            el.Add(new XAttribute("Employer", entity.Employer));
            el.Add(new XAttribute("StartDate", entity.StartDate));
            el.Add(new XAttribute("EndDate", entity.EndDate));
            el.Add(new XAttribute("Guid", guid.ToString()));
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

            doc.Root.Add(el);

            doc.Save(experienceFilePath);

            return guid;
        }

        public void Update(Guid guid, IExperienceEntity entity) 
        {
            var doc = XDocument.Load(experienceFilePath);

            int testCount = doc.Root.Elements("Job").Where(T => T.Attribute("Guid").Value.Contains(guid.ToString())).Count();

            if (testCount == 0) throw new ArgumentException("guid not found.");

            var job = doc.Root.Elements("Job").FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            job.SetAttributeValue("Employer", entity.Employer);
            job.SetAttributeValue("StartDate", entity.StartDate);
            job.SetAttributeValue("EndDate", entity.EndDate);

            job.Element("JobTitles").Elements("JobTitle").Remove();
            foreach (var newTitle in entity.Titles)
                job.Element("JobTitles").Add(new XElement("JobTitle", newTitle));

            job.Element("Details").Elements("Detail").Remove();
            foreach (var newDetail in entity.Details)
                job.Element("Details").Add(new XElement("Detail", newDetail));

            doc.Save(experienceFilePath);
        }
    }
}