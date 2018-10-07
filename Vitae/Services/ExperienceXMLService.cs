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

            var eeXml = doc.Root.Elements("Job")
                .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            if (eeXml == null) return null;
            else return convertToObject(eeXml);
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
                    list.Add(convertToObject(job));
                }

                return list;
            }
        }

        public Guid Insert(IExperienceEntity entity) 
        {
            var doc = XDocument.Load(experienceFilePath);

            var guid = Guid.NewGuid();
            var el = convertToXml(guid, entity);
            doc.Root.Add(el);
            doc.Save(experienceFilePath);

            return guid;
        }

        public void Update(Guid guid, IExperienceEntity entity) 
        {
            var doc = XDocument.Load(experienceFilePath);

            var job = doc.Root.Elements("Job").FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (job == null) throw new ArgumentException("guid not found.");
            job.ReplaceWith(convertToXml(guid, entity));

            doc.Save(experienceFilePath);
        }

        private IExperienceEntity convertToObject(XElement el) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ee = ioc.Get<IExperienceEntity>();
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
        }

        private XElement convertToXml(Guid guid, IExperienceEntity entity) 
        {
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

            return el;
        }
    }
}