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

        public void Delete(Guid guid) 
        {
            if (guid == null) throw new ArgumentNullException("guid");

            var doc = XDocument.Load(educationFilePath);
            getXElement(doc, guid).Remove();

            doc.Save(educationFilePath);
        }

        public IEducationEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(educationFilePath);

            var element = getXElement(doc, guid);
            if (element == null) return null;

            return convertToObject(element);
        }

        public IList<IEducationEntity> GetAll() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IEducationEntity>();
                var doc = XDocument.Load(educationFilePath);

                var elements = doc.Root.Elements("EducationItem").ToList();
                foreach (var element in elements)
                {
                    list.Add(convertToObject(element));
                }

                return list;
            }
        }

        public Guid Insert(IEducationEntity entity) 
        {
            var doc = XDocument.Load(educationFilePath);

            var guid = Guid.NewGuid();
            var newElement = convertToXml(guid, entity);
            doc.Root.Add(newElement);

            doc.Save(educationFilePath);

            return guid;
        }

        public void Update(Guid guid, IEducationEntity entity) 
        {
            if (guid == null) throw new ArgumentNullException("guid");
            if (entity == null) throw new ArgumentNullException("entity");

            var doc = XDocument.Load(educationFilePath);
            XElement oldEl = getXElement(doc, guid);
            if (oldEl == null) throw new ArgumentException("guid not found.");
            oldEl.ReplaceWith(convertToXml(guid, entity));

            doc.Save(educationFilePath);
        }

        private XElement getXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("EducationItem")
                .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private XElement convertToXml(Guid id, IEducationEntity ent) 
        {
            return new XElement("EducationItem",
                new XAttribute("Guid", id.ToString()),
                new XElement("Credential", ent.Credential),
                new XElement("Institution", ent.Institution));
        }

        private IEducationEntity convertToObject(XElement el) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ee = ioc.Get<IEducationEntity>();
                ee.Credential = el.Element("Credential").Value;
                ee.Institution = el.Element("Institution").Value;
                return ee;
            }
        }
    }
}