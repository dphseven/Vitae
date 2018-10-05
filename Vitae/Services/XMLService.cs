namespace Vitae.Services
{
    using Model;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public class XMLService : IXMLService
    {
        private readonly string generalInfoFilePath;
        private readonly string experienceFilePath;
        private readonly string expertiseFilePath;
        private readonly string educationFilePath;
        private readonly string publicationsFilePath;

        public XMLService() 
        {
            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            generalInfoFilePath = prefix + @"\XML\GeneralInfo.xml";
            experienceFilePath = prefix + @"\XML\Experience.xml";
            expertiseFilePath = prefix + @"\XML\Expertise.xml";
            educationFilePath = prefix + @"\XML\Education.xml";
            publicationsFilePath = prefix + @"\XML\Publications.xml";
        }

        // GENERAL INFO

        public IGeneralInfoEntity GetGeneralInformation(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var gie = ioc.Get<IGeneralInfoEntity>();

                XDocument xDoc = XDocument.Load(generalInfoFilePath);
                gie.FullName = xDoc.Root.Element("FullName").Value;
                gie.Add1 = xDoc.Root.Element("AddLine1").Value;
                gie.Add2 = xDoc.Root.Element("AddLine2").Value;
                gie.Email = xDoc.Root.Element("Email").Value;
                gie.Phone = xDoc.Root.Element("Phone").Value;

                return gie;
            }
        }

        public Guid Insert(IGeneralInfoEntity entity) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);
                var guid = Guid.NewGuid();

                var ele = doc.Root;
                if (ele.Attribute("Guid") != null) ele.Attribute("Guid").Value = guid.ToString();
                else ele.Add(new XAttribute("Guid", guid.ToString()));

                ele.Element("FullName").Value = entity.FullName;
                ele.Element("AddLine1").Value = entity.Add1;
                ele.Element("AddLine2").Value = entity.Add2;
                ele.Element("Phone").Value = entity.Phone;
                ele.Element("Email").Value = entity.Email;

                doc.Save(generalInfoFilePath);

                return guid;
            }
        }

        public void DeleteGeneralInfo(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);

                if (doc.Root.Attribute("Guid") != null) 
                    doc.Root.Attribute("Guid").Value = "";
                doc.Root.Element("FullName").Value = "";
                doc.Root.Element("AddLine1").Value = "";
                doc.Root.Element("AddLine2").Value = "";
                doc.Root.Element("Phone").Value = "";
                doc.Root.Element("Email").Value = "";

                doc.Save(generalInfoFilePath);
            }
        }

        public void Update(Guid g, IGeneralInfoEntity t) 
        {
            var doc = XDocument.Load(generalInfoFilePath);

            doc.Root.Element("FullName").Value = t.FullName;
            doc.Root.Element("AddLine1").Value = t.Add1;
            doc.Root.Element("AddLine2").Value = t.Add2;
            doc.Root.Element("Email").Value = t.Email;
            doc.Root.Element("Phone").Value = t.Phone;

            doc.Save(generalInfoFilePath);
        }

        // EDUCATION -- DONE

        public IList<IEducationEntity> GetAllEducations() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IEducationEntity>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(educationFilePath);
                XmlNodeList educationNodes = xmlDoc.SelectNodes("//EducationItems/EducationItem");
                foreach (XmlNode item in educationNodes)
                {
                    IEducationEntity edEnt = ioc.Get<IEducationEntity>();
                    edEnt.Credential = item.SelectSingleNode("Credential").InnerText;
                    edEnt.Institution = item.SelectSingleNode("Institution").InnerText;
                    list.Add(edEnt);
                }

                return list;
            }
        }

        public IEducationEntity GetEducation(Guid id) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(educationFilePath);

                var element = doc.Root.Elements().Where(T => T.Attribute("Guid").Value == id.ToString()).FirstOrDefault();
                if (element != null)
                {
                    var ee = ioc.Get<IEducationEntity>();
                    ee.Credential = element.Element("Credential").Value;
                    ee.Institution = element.Element("Institution").Value;
                    return ee;
                }
                else return null;
            }
        }

        public Guid Insert(IEducationEntity ee) 
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(educationFilePath);
            var guid = Guid.NewGuid();

            var newElement = new XElement("EducationItem",
                new XAttribute("Guid", guid.ToString()),
                new XElement("Credential", ee.Credential),
                new XElement("Institution", ee.Institution));

            doc.Root.Add(newElement);
            doc.Save(educationFilePath);

            return guid;
        }

        public void Delete(Guid g) 
        {
            if (g == null) throw new ArgumentNullException("guid");

            XDocument doc = new XDocument();
            doc = XDocument.Load(educationFilePath);

            doc.Root.Elements("EducationItem")
                    .Where(T => T.Attribute("Guid").Value == g.ToString())
                    .FirstOrDefault()
                    .Remove();

            doc.Save(educationFilePath);
        }

        public void Update(Guid g, IEducationEntity newEE) 
        {
            if (g == null) throw new ArgumentNullException("g");
            if (newEE == null) throw new ArgumentNullException("newEE");

            XDocument doc = new XDocument();
            doc = XDocument.Load(educationFilePath);

            XElement oldEl = doc.Root
                .Elements("EducationItem")
                .Where(T => T.Attribute("Guid").Value == g.ToString())
                .FirstOrDefault();

            oldEl.Element("Credential").Value = newEE.Credential;
            oldEl.Element("Institution").Value = newEE.Institution;

            doc.Save(educationFilePath);
        }

        // PUBLICATIONS -- DONE

        public IList<IPublicationEntity> GetPublications() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IPublicationEntity>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(publicationsFilePath);
                foreach (XmlNode item in xmlDoc.SelectNodes("//Publications/Publication"))
                {
                    IPublicationEntity pub = ioc.Get<IPublicationEntity>();
                    pub.Publication = item.InnerText;
                    list.Add(pub);
                }

                return list;
            }
        }

        public Guid Insert(IPublicationEntity pe) 
        {
            var doc = XDocument.Load(publicationsFilePath);
            var g = Guid.NewGuid();
            var element = new XElement("Publication",
                              new XAttribute("Guid", g.ToString()),
                              pe.Publication);
            doc.Root.Add(element);
            doc.Save(publicationsFilePath);
            return g;
        }

        public IPublicationEntity GetPublication(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ent = ioc.Get<IPublicationEntity>();
                var doc = XDocument.Load(publicationsFilePath);
                var element = doc.Root.Elements("Publication")
                    .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
                if (element == null) return null;
                ent.Publication = element.Value;
                return ent;
            }            
        }

        public void DeletePublication(Guid guid) 
        {
            var doc = XDocument.Load(publicationsFilePath);
            var element = doc.Root.Elements("Publication")
                             .Where(T => T.Attribute("Guid").Value == guid.ToString());
            element.Remove();
            doc.Save(publicationsFilePath);
        }

        public void Update(Guid guid, IPublicationEntity pub) 
        {
            var doc = XDocument.Load(publicationsFilePath);
            var element = doc.Root.Elements("Publication")
                             .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (element != null) element.Value = pub.Publication;
            doc.Save(publicationsFilePath);
        }

        // EXPERTISE -- DONE

        public IList<IExpertiseEntity> GetExpertise() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                List<IExpertiseEntity> list = new List<IExpertiseEntity>();

                var doc = XDocument.Load(expertiseFilePath);

                foreach (var item in doc.Root.Elements("ExpertiseItem"))
                {
                    IExpertiseEntity ee = ioc.Get<IExpertiseEntity>();
                    ee.Category = (item.Attribute("Category") == null) ? "" : item.Attribute("Category").Value;
                    ee.Expertise = (item.Attribute("Expertise") == null) ? "" : item.Attribute("Expertise").Value;
                    list.Add(ee);
                }

                return list;
            }
        }

        public Guid Insert(IExpertiseEntity entity) 
        {
            var g = Guid.NewGuid();

            var doc = XDocument.Load(expertiseFilePath);

            doc.Root.Add(
                new XElement("ExpertiseItem", new XAttribute("Guid", g.ToString()),
                                              new XElement("Category", entity.Category),
                                              new XElement("Expertise", entity.Expertise)));

            doc.Save(expertiseFilePath);

            return g;
        }

        public IExpertiseEntity GetExpertiseItem(Guid guid) 
        {
            var doc = XDocument.Load(expertiseFilePath);

            var item = doc.Root.Elements("ExpertiseItem").FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            if (item == null) throw new ArgumentException("guid not found.");

            using (var ioc = new VitaeNinjectKernel())
            {
                var ee = ioc.Get<IExpertiseEntity>();
                ee.Category = item.Element("Category").Value;
                ee.Expertise = item.Element("Expertise").Value;
                return ee;
            }
        }

        public void DeleteExpertise(Guid g)
        {
            var doc = XDocument.Load(expertiseFilePath);

            var element = doc.Root.Elements("ExpertiseItem").SingleOrDefault(T => T.Attribute("Guid").Value == g.ToString());
            if (element != null) element.Remove();

            doc.Save(expertiseFilePath);
        }

        public void Update(Guid guid, IExpertiseEntity entity) 
        {
            var doc = XDocument.Load(expertiseFilePath);

            var element = doc.Root.Elements("ExpertiseItem")
                .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());

            element.Element("Category").Value = entity.Category;
            element.Element("Expertise").Value = entity.Expertise;

            doc.Save(expertiseFilePath);
        }

        // EXPERIENCE

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

        public IExperienceEntity GetExperience(Guid guid) 
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

        public IList<IExperienceEntity> GetAllExperiences() 
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

        public void DeleteExperience(Guid g) 
        {
            var doc = XDocument.Load(experienceFilePath);

            var element = doc.Root.Elements("Job")
                .SingleOrDefault(T => T.Attribute("Guid").Value == g.ToString());

            element.Remove();

            doc.Save(experienceFilePath);
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