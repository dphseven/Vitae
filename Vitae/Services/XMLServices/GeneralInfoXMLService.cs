namespace Vitae.Services
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Xml.Linq;
    using Vitae.Model;

    public class GeneralInfoXMLService : IGeneralInfoXMLService 
    {
        private readonly string generalInfoFilePath;

        public GeneralInfoXMLService() 
        {
            {
                string prefix = string.Empty;

                if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
                else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

                generalInfoFilePath = prefix + @"\XML\GeneralInfo.xml";
            }
        }

        public void Delete(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);
                var el = getXElement(doc, guid);

                el.ReplaceWith(convertToXml(guid, ioc.Get<IGeneralInfoEntity>()));

                doc.Save(generalInfoFilePath);
            }
        }

        public IGeneralInfoEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(generalInfoFilePath);
            return convertToObject(getXElement(doc, guid));
        }

        public IList<IGeneralInfoEntity> GetAll() 
        {
            return new List<IGeneralInfoEntity> { Get(Guid.NewGuid()) };
        }

        public Guid Insert(IGeneralInfoEntity entity) 
        {
            var guid = Guid.NewGuid();
            Update(guid, entity);
            return guid;
        }

        public void Update(Guid guid, IGeneralInfoEntity entity) 
        {
            var doc = XDocument.Load(generalInfoFilePath);

            var ele = getXElement(doc, guid);
            ele.ReplaceWith(convertToXml(guid, entity));

            doc.Save(generalInfoFilePath);
        }

        private XElement getXElement(XDocument doc, Guid guid) 
        {
            return doc.Root;
        }

        private IGeneralInfoEntity convertToObject(XElement element) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var entity = ioc.Get<IGeneralInfoEntity>();
                entity.FullName = element.Element("FullName").Value;
                entity.Add1 = element.Element("AddLine1").Value;
                entity.Add2 = element.Element("AddLine2").Value;
                entity.Email = element.Element("Email").Value;
                entity.Phone = element.Element("Phone").Value;
                return entity;
            }
        }

        private XElement convertToXml(Guid guid, IGeneralInfoEntity entity) 
        {
            return new XElement("GeneralInfo",
                new XElement("FullName", entity.FullName),
                new XElement("AddLine1", entity.Add1),
                new XElement("AddLine2", entity.Add2),
                new XElement("Email", entity.Email),
                new XElement("Phone", entity.Phone));
        }
    }
}