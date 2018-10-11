﻿ namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Vitae.Model;

    public class PublicationsXMLService : IPublicationsXMLService
    {
        private readonly string filePath;

        public PublicationsXMLService() 
        {
            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            filePath = prefix + @"\XML\Publications.xml";
        }

        public void Delete(Guid guid) 
        {

            var doc = XDocument.Load(filePath);
            var element = doc.Root.Elements("Publication")
                             .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (element == null) throw new ArgumentException("guid not found.");
            element.Remove();
            doc.Save(filePath);
        }

        public IPublicationEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(filePath);
            var element = getXElement(doc, guid);
            if (element == null) return null;
            return convertToObject(element);
        }

        public IList<IPublicationEntity> GetAll() 
        {
            var list = new List<IPublicationEntity>();
            var doc = XDocument.Load(filePath);
            foreach (var element in doc.Root.Elements("Publication"))
            {
                list.Add(convertToObject(element));
            }

            return list;
        }

        public Guid Insert(IPublicationEntity entity) 
        {
            var doc = XDocument.Load(filePath);
            var g = Guid.NewGuid();
            doc.Root.Add(convertToXml(g, entity));
            doc.Save(filePath);
            return g;
        }

        public void Update(Guid guid, IPublicationEntity entity) 
        {
            var doc = XDocument.Load(filePath);
            var element = getXElement(doc, guid);
            if (element == null) throw new ArgumentException("guid not found.");
            element.ReplaceWith(convertToXml(guid, entity));
            doc.Save(filePath);
        }

        private XElement getXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("Publication")
                    .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private IPublicationEntity convertToObject(XElement element) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var pe = ioc.Get<IPublicationEntity>();

                Guid output = Guid.Empty;
                if (Guid.TryParse(element.Attribute("Guid").Value, out output))
                    pe.ID = output;
                else pe.ID = Guid.NewGuid();

                pe.Publication = element.Value;

                return pe;
            }
        }

        private XElement convertToXml(Guid guid, IPublicationEntity entity) 
        {
            return new XElement("Publication",
                new XAttribute("Guid", guid.ToString()),
                entity.Publication);
        }
    }
}
