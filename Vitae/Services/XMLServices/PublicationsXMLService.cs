 namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Vitae.Model;

    public class PublicationsXMLService : IPublicationsXMLService
    {
        private readonly IKernel _kernel;
        private readonly IPersistenceService persister;

        public PublicationsXMLService(IKernel kernel, IPersistenceService persistenceService) 
        {
            _kernel = kernel;
            persister = persistenceService;
        }

        public void Delete(Guid guid) 
        {
            var doc = persister.Load<IPublicationEntity>();

            var element = doc.Root.Elements("Publication")
                             .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (element == null) throw new ArgumentException("guid not found.");
            element.Remove();

            persister.Persist<IPublicationEntity>(doc);
        }

        public IPublicationEntity Get(Guid guid) 
        {
            var doc = persister.Load<IPublicationEntity>();
            var element = GetXElement(doc, guid);
            if (element == null) return null;
            return ConvertToObject(element);
        }

        public IList<IPublicationEntity> GetAll() 
        {
            var list = new List<IPublicationEntity>();
            var doc = persister.Load<IPublicationEntity>();
            foreach (var element in doc.Root.Elements("Publication"))
            {
                list.Add(ConvertToObject(element));
            }

            return list;
        }

        public Guid Insert(IPublicationEntity entity) 
        {
            var doc = persister.Load<IPublicationEntity>();
            var g = Guid.NewGuid();
            doc.Root.Add(ConvertToXml(g, entity));
            persister.Persist<IPublicationEntity>(doc);
            return g;
        }

        public void Update(Guid guid, IPublicationEntity entity) 
        {
            var doc = persister.Load<IPublicationEntity>();
            var element = GetXElement(doc, guid);
            if (element == null) throw new ArgumentException("guid not found.");
            element.ReplaceWith(ConvertToXml(guid, entity));
            persister.Persist<IPublicationEntity>(doc);
        }

        private XElement GetXElement(XDocument doc, Guid guid) 
        {
            return doc.Root.Elements("Publication")
                    .FirstOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
        }

        private IPublicationEntity ConvertToObject(XElement element) 
        {
            var pe = _kernel.Get<IPublicationEntity>();

            Guid output = Guid.Empty;
            if (Guid.TryParse(element.Attribute("Guid").Value, out output))
                pe.ID = output;
            else pe.ID = Guid.NewGuid();

            pe.Publication = element.Value;

            return pe;
        }

        private XElement ConvertToXml(Guid guid, IPublicationEntity entity) 
        {
            return 
                new XElement("Publication",
                    new XAttribute("Guid", guid.ToString()),
                    entity.Publication);
        }
    }
}
