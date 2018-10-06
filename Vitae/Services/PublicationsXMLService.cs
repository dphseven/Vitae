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
                             .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (element == null) throw new ArgumentException("guid not found.");
            element.Remove();
            doc.Save(filePath);
        }

        public IPublicationEntity Get(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ent = ioc.Get<IPublicationEntity>();
                var doc = XDocument.Load(filePath);
                var element = doc.Root.Elements("Publication")
                    .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
                if (element == null) return null;
                ent.Publication = element.Value;
                return ent;
            }
        }

        public IList<IPublicationEntity> GetAll() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var list = new List<IPublicationEntity>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                foreach (XmlNode item in xmlDoc.SelectNodes("//Publications/Publication"))
                {
                    IPublicationEntity pub = ioc.Get<IPublicationEntity>();
                    pub.Publication = item.InnerText;
                    list.Add(pub);
                }

                return list;
            }
        }

        public Guid Insert(IPublicationEntity entity) 
        {
            var doc = XDocument.Load(filePath);
            var g = Guid.NewGuid();
            var element = new XElement("Publication",
                              new XAttribute("Guid", g.ToString()),
                              entity.Publication);
            doc.Root.Add(element);
            doc.Save(filePath);
            return g;
        }

        public void Update(Guid guid, IPublicationEntity entity) 
        {
            var doc = XDocument.Load(filePath);
            var element = doc.Root.Elements("Publication")
                             .SingleOrDefault(T => T.Attribute("Guid").Value == guid.ToString());
            if (element == null) throw new ArgumentException("guid not found.");
            element.Value = entity.Publication;
            doc.Save(filePath);
        }
    }
}
