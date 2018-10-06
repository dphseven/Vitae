namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using Vitae.Model;

    public class EducationXMLService : IEducationXMLService
    {
        private readonly string educationFilePath;

        public EducationXMLService() 
        {
            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            educationFilePath = prefix + @"\XML\Education.xml";
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

        public IEducationEntity Get(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(educationFilePath);

                var element = doc.Root.Elements()
                    .Where(T => T.Attribute("Guid").Value == guid.ToString()).FirstOrDefault();
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

        public IList<IEducationEntity> GetAll() 
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

        public Guid Insert(IEducationEntity entity) 
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(educationFilePath);
            var guid = Guid.NewGuid();

            var newElement = new XElement("EducationItem",
                new XAttribute("Guid", guid.ToString()),
                new XElement("Credential", entity.Credential),
                new XElement("Institution", entity.Institution));

            doc.Root.Add(newElement);
            doc.Save(educationFilePath);

            return guid;
        }

        public void Update(Guid guid, IEducationEntity entity) 
        {
            if (guid == null) throw new ArgumentNullException("guid");
            if (entity == null) throw new ArgumentNullException("entity");

            XDocument doc = new XDocument();
            doc = XDocument.Load(educationFilePath);

            XElement oldEl = doc.Root
                .Elements("EducationItem")
                .Where(T => T.Attribute("Guid").Value == guid.ToString())
                .FirstOrDefault();

            oldEl.Element("Credential").Value = entity.Credential;
            oldEl.Element("Institution").Value = entity.Institution;

            doc.Save(educationFilePath);
        }
    }
}
