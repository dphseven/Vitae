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
                List<IExpertiseEntity> list = new List<IExpertiseEntity>();

                var doc = XDocument.Load(expertiseFilePath);

                foreach (var item in doc.Root.Elements("ExpertiseItem"))
                {
                    IExpertiseEntity ee = ioc.Get<IExpertiseEntity>();
                    if (item.Element("Category") != null)
                        ee.Category = item.Element("Category").Value;
                    else ee.Category = "";
                    if (item.Element("Expertise") != null)
                        ee.Expertise = item.Element("Expertise").Value;
                    else ee.Expertise = "";
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

        public IExpertiseEntity Get(Guid guid) 
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

        public void Delete(Guid g) 
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

    }
}