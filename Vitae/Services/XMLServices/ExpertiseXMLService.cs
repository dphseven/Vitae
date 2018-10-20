namespace Vitae.Services
{
    using Model;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class ExpertiseXMLService : IExpertiseXMLService
    {
        private readonly IKernel _kernel;
        private readonly IPersistenceService persister;

        public ExpertiseXMLService(IKernel kernel, IPersistenceService persistenceService) 
        {
            _kernel = kernel;
            persister = persistenceService;
        }

        public IList<IExpertiseEntity> GetAll() 
        {
            var list = new List<IExpertiseEntity>();
            var doc = persister.Load<IExpertiseEntity>();

            foreach (var el in doc.Root.Elements("ExpertiseItem"))
                list.Add(ConvertToObject(el));

            return list;
        }

        public Guid Insert(IExpertiseEntity entity) 
        {
            var g = Guid.NewGuid();
            var doc = persister.Load<IExpertiseEntity>();

            doc.Root.Add(ConvertToXml(g, entity));

            persister.Persist<IExpertiseEntity>(doc);
            return g;
        }

        public IExpertiseEntity Get(Guid guid) 
        {
            var doc = persister.Load<IExpertiseEntity>();

            var el = GetXElement(doc, guid);
            if (el == null) throw new ArgumentException("guid not found.");

            return ConvertToObject(el);
        }

        public void Delete(Guid guid) 
        {
            var doc = persister.Load<IExpertiseEntity>();

            var element = GetXElement(doc, guid);
            if (element != null) element.Remove();

            persister.Persist<IExpertiseEntity>(doc);
        }

        public void Update(Guid guid, IExpertiseEntity entity) 
        {
            var doc = persister.Load<IExpertiseEntity>();

            var element = GetXElement(doc, guid);
            element.ReplaceWith(ConvertToXml(guid, entity));

            persister.Persist<IExpertiseEntity>(doc);
        }

        private XElement GetXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("ExpertiseItem")
                .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private IExpertiseEntity ConvertToObject(XElement element) 
        {
            var ee = _kernel.Get<IExpertiseEntity>();

            if (Guid.TryParse(element.Attribute("Guid").Value, out Guid output))
                ee.ID = output;
            else ee.ID = Guid.NewGuid();

            ee.Category = element.Element("Category").Value;
            ee.Expertise = element.Element("Expertise").Value;

            return ee;
        }

        private XElement ConvertToXml(Guid guid, IExpertiseEntity entity) 
        {
            return new XElement("ExpertiseItem", 
                new XAttribute("Guid", guid.ToString()),
                new XElement("Category", entity.Category),
                new XElement("Expertise", entity.Expertise));
        }

    }
}