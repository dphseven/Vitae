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

    public class ExpertiseXMLService : IExpertiseXMLService
    {
        private readonly string expertiseFilePath;

        public ExpertiseXMLService() 
        {
            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            expertiseFilePath = prefix + @"\XML\Expertise.xml";
        }

        public IList<IExpertiseEntity> GetAll() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IExpertiseEntity>();
                var doc = XDocument.Load(expertiseFilePath);

                foreach (var el in doc.Root.Elements("ExpertiseItem"))
                {
                    list.Add(convertToObject(el));
                }

                return list;
            }
        }

        public Guid Insert(IExpertiseEntity entity) 
        {
            var g = Guid.NewGuid();
            var doc = XDocument.Load(expertiseFilePath);

            doc.Root.Add(convertToXml(g, entity));

            doc.Save(expertiseFilePath);
            return g;
        }

        public IExpertiseEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(expertiseFilePath);

            var el = getXElement(doc, guid);
            if (el == null) throw new ArgumentException("guid not found.");

            return convertToObject(el);
        }

        public void Delete(Guid guid) 
        {
            var doc = XDocument.Load(expertiseFilePath);

            var element = getXElement(doc, guid);
            if (element != null) element.Remove();

            doc.Save(expertiseFilePath);
        }

        public void Update(Guid guid, IExpertiseEntity entity) 
        {
            var doc = XDocument.Load(expertiseFilePath);

            var element = getXElement(doc, guid);
            element.ReplaceWith(convertToXml(guid, entity));

            doc.Save(expertiseFilePath);
        }

        private XElement getXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("ExpertiseItem")
                .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private IExpertiseEntity convertToObject(XElement element) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ee = ioc.Get<IExpertiseEntity>();

                if (Guid.TryParse(element.Attribute("Guid").Value, out Guid output))
                    ee.ID = output;
                else ee.ID = Guid.NewGuid();

                ee.Category = element.Element("Category").Value;
                ee.Expertise = element.Element("Expertise").Value;

                return ee;
            }
        }

        private XElement convertToXml(Guid guid, IExpertiseEntity entity) 
        {
            return new XElement("ExpertiseItem", 
                new XAttribute("Guid", guid.ToString()),
                new XElement("Category", entity.Category),
                new XElement("Expertise", entity.Expertise));
        }

    }
}