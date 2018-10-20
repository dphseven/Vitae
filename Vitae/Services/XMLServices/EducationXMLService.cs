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
        private readonly IKernel kernel;
        private readonly IPersistenceService persister;

        public EducationXMLService(IKernel kernel, IPersistenceService persistenceService) 
        {
            this.kernel = kernel;
            persister = persistenceService;
        }

        public void Delete(Guid guid) 
        {
            if (guid == null) throw new ArgumentNullException("guid");

            var doc = persister.Load<IEducationEntity>();
            GetXElement(doc, guid).Remove();

            persister.Persist<IEducationEntity>(doc);
        }

        public IEducationEntity Get(Guid guid) 
        {
            var doc = persister.Load<IEducationEntity>();

            var element = GetXElement(doc, guid);
            if (element == null) return null;

            return ConvertToObject(element);
        }

        public IList<IEducationEntity> GetAll() 
        {
            var list = new List<IEducationEntity>();
            var doc = persister.Load<IEducationEntity>();

            var elements = doc.Root.Elements("EducationItem").ToList();
            foreach (var element in elements)
            {
                list.Add(ConvertToObject(element));
            }

            return list;
        }

        public Guid Insert(IEducationEntity entity) 
        {
            var doc = persister.Load<IEducationEntity>();

            var guid = Guid.NewGuid();
            var newElement = ConvertToXml(guid, entity);
            doc.Root.Add(newElement);

            persister.Persist<IEducationEntity>(doc);

            return guid;
        }

        public void Update(Guid guid, IEducationEntity entity) 
        {
            if (guid == null) throw new ArgumentNullException("guid");
            if (entity == null) throw new ArgumentNullException("entity");

            var doc = persister.Load<IEducationEntity>();
            XElement oldEl = GetXElement(doc, guid);
            if (oldEl == null) throw new ArgumentException("guid not found.");
            oldEl.ReplaceWith(ConvertToXml(guid, entity));

            persister.Persist<IEducationEntity>(doc);
        }

        private XElement GetXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("EducationItem")
                .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private XElement ConvertToXml(Guid id, IEducationEntity ent) 
        {
            return new XElement("EducationItem",
                new XAttribute("Guid", id.ToString()),
                new XElement("Credential", ent.Credential),
                new XElement("Institution", ent.Institution));
        }

        private IEducationEntity ConvertToObject(XElement el) 
        {
            var ee = kernel.Get<IEducationEntity>();

            if (el.Attribute("Guid") != null)
            {
                if (Guid.TryParse(el.Attribute("Guid").Value, out Guid output))
                    ee.ID = output;
                else ee.ID = Guid.NewGuid();
            }

            ee.Credential = el.Element("Credential").Value;
            ee.Institution = el.Element("Institution").Value;

            return ee;
        }
    }
}