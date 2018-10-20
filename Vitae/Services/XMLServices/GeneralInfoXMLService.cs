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
        private readonly IKernel _kernel;
        private readonly string generalInfoFilePath;

        public GeneralInfoXMLService(IKernel kernel) 
        {
            _kernel = kernel;

            string prefix = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            generalInfoFilePath = prefix + @"\XML\GeneralInfo.xml";
        }

        public void Delete(Guid guid) 
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(generalInfoFilePath);
            var el = GetXElement(doc, guid);

            el.ReplaceWith(ConvertToXml(guid, _kernel.Get<IGeneralInfoEntity>()));

            doc.Save(generalInfoFilePath);
        }

        public IGeneralInfoEntity Get(Guid guid) 
        {
            var doc = XDocument.Load(generalInfoFilePath);
            return ConvertToObject(GetXElement(doc, guid));
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

            var ele = GetXElement(doc, guid);
            ele.ReplaceWith(ConvertToXml(guid, entity));

            doc.Save(generalInfoFilePath);
        }

        private XElement GetXElement(XDocument doc, Guid guid) 
        {
            return doc.Root;
        }

        private IGeneralInfoEntity ConvertToObject(XElement element) 
        {
            var entity = _kernel.Get<IGeneralInfoEntity>();

            Guid output = Guid.Empty;
            if (Guid.TryParse(element.Attribute("Guid").Value, out output))
                entity.ID = output;
            else entity.ID = Guid.NewGuid();

            entity.FullName = element.Element("FullName").Value;
            entity.Add1 = element.Element("AddLine1").Value;
            entity.Add2 = element.Element("AddLine2").Value;
            entity.Email = element.Element("Email").Value;
            entity.Phone = element.Element("Phone").Value;

            return entity;
        }

        private XElement ConvertToXml(Guid guid, IGeneralInfoEntity entity) 
        {
            return 
                new XElement("GeneralInfo",
                    new XAttribute("Guid", entity.ID),
                    new XElement("FullName", entity.FullName),
                    new XElement("AddLine1", entity.Add1),
                    new XElement("AddLine2", entity.Add2),
                    new XElement("Email", entity.Email),
                    new XElement("Phone", entity.Phone));
        }
    }
}