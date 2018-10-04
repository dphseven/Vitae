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
        string generalInfoFilePath;
        string experienceFilePath;
        string expertiseFilePath;
        string educationFilePath;
        string publicationsFilePath;

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

        // LEGACY

        public IList<IJobTitle> GetJobTitles() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                List<IJobTitle> list = new List<IJobTitle>();

                XmlDocument doc = new XmlDocument();
                doc.Load(experienceFilePath);
                XmlNodeList allJobs = doc.SelectNodes("//Jobs/Job");
                foreach (XmlNode job in allJobs)
                {
                    string emp = job.Attributes["Employer"].InnerText;

                    foreach (XmlNode title in job.SelectNodes("JobTitles/JobTitle"))
                    {
                        IJobTitle jt = ioc.Get<IJobTitle>();
                        jt.Employer = emp;
                        jt.Title = title.InnerText;
                        list.Add(jt);
                    }
                }

                return list;
            }
        }

        public IList<IExperienceItem> GetExperienceDetails() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                List<IExperienceItem> list = new List<IExperienceItem>();

                XmlDocument doc = new XmlDocument();
                doc.Load(experienceFilePath);
                foreach (XmlNode job in doc.SelectNodes("//Jobs/Job"))
                {
                    string emp = job.Attributes["Employer"].InnerText;

                    foreach (XmlNode jobDetail in job.SelectNodes("Details/Detail"))
                    {
                        IExperienceItem ei = ioc.Get<IExperienceItem>();
                        ei.Employer = emp;
                        ei.ExperienceDetail = jobDetail.InnerText;
                        list.Add(ei);
                    }
                }

                return list;
            }
        }

        public IList<IExperienceItem> GetExperienceDetailsForEmployer(string Employer) 
        {
            return GetExperienceDetails().Where(T => T.Employer == Employer).ToList();
        }

        public IList<IExperienceEntity> GetAllJobs() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IExperienceEntity>();

                XmlDocument doc = new XmlDocument();
                doc.Load(experienceFilePath);
                foreach (XmlNode job in doc.SelectNodes("//Jobs/Job"))
                {
                    IExperienceEntity e = ioc.Get<IExperienceEntity>();
                    e.Employer = job.Attributes["Employer"].InnerText;
                    e.StartDate = job.Attributes["StartDate"].InnerText;
                    e.EndDate = job.Attributes["EndDate"].InnerText;

                    foreach (XmlNode title in job.SelectNodes("JobTitles/JobTitle"))
                    {
                        e.Titles.Add(title.InnerText);
                    }

                    foreach (XmlNode detail in job.SelectNodes("Details/Detail"))
                    {
                        e.Details.Add(detail.InnerText);
                    }

                    list.Add(e);
                }

                return list;
            }
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

        /// <exception cref="InvalidOperationException"/>
        public Guid Insert(IGeneralInfoEntity entity) 
        {
            if (this.GetAllGeneralInfos().Count > 0)
                throw new InvalidOperationException("There is already a GeneralInfo node on file.");

            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);
                var guid = Guid.NewGuid();
                
                var newElement = new XElement("GeneralInfo",
                    new XAttribute("Guid", guid.ToString()),
                    new XElement("FullName", entity.FullName),
                    new XElement("AddLine1", entity.Add1),
                    new XElement("AddLine2", entity.Add2),
                    new XElement("Phone", entity.Phone),
                    new XElement("Email", entity.Email));

                doc.Root.Add(newElement);
                doc.Save(educationFilePath);

                return guid;
            }
        }

        public IList<IGeneralInfoEntity> GetAllGeneralInfos() 
        {
            this.Insert(new GeneralInfoEntity());
            throw new NotImplementedException();
        }

        public void DeleteGeneralInfo(Guid guid) 
        {
            throw new NotImplementedException();
        }

        public void Update(Guid g, IGeneralInfoEntity t) 
        {
            throw new NotImplementedException();
        }

        // EDUCATION

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

        // PUBLICATIONS

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
            throw new NotImplementedException();
        }

        public IPublicationEntity GetPublication(Guid guid) 
        {
            throw new NotImplementedException();
        }

        public void DeletePublication(Guid guid) 
        {
            throw new NotImplementedException();
        }

        public void Update(Guid guid, IPublicationEntity pub) 
        {
            throw new NotImplementedException();
        }

        // EXPERTISE

        public IList<IExpertiseEntity> GetExpertise() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                List<IExpertiseEntity> list = new List<IExpertiseEntity>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(expertiseFilePath);
                foreach (XmlNode item in xmlDoc.SelectNodes("//Expertise/ExpertiseItem"))
                {
                    IExpertiseEntity ee = ioc.Get<IExpertiseEntity>();
                    ee.Category = item.Attributes["Category"].InnerText;
                    ee.Expertise = item.Attributes["Expertise"].InnerText;
                    list.Add(ee);
                }

                return list;
            }
        }

        public Guid Insert(IExpertiseEntity entity) 
        {
            throw new NotImplementedException();
        }

        public IExpertiseEntity GetExpertiseItem(Guid guid) 
        {
            throw new NotImplementedException();
        }

        public void DeleteExpertise(Guid g) 
        {
            throw new NotImplementedException();
        }

        public void Update(Guid guid, IExpertiseEntity entity) 
        {
            throw new NotImplementedException();
        }

        // EXPERIENCE

        public Guid Insert(IExperienceEntity entity) 
        {
            throw new NotImplementedException();
        }

        public IExperienceEntity GetExperience(Guid guid) 
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(Guid guid, IExperienceEntity entity) 
        {
            throw new NotImplementedException();
        }


    }
}