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

        public void Delete(Guid g) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);

                if (doc.Root.Attribute("Guid") != null)
                    doc.Root.Attribute("Guid").Value = "";
                doc.Root.Element("FullName").Value = "";
                doc.Root.Element("AddLine1").Value = "";
                doc.Root.Element("AddLine2").Value = "";
                doc.Root.Element("Phone").Value = "";
                doc.Root.Element("Email").Value = "";

                doc.Save(generalInfoFilePath);
            }
        }

        public IGeneralInfoEntity Get(Guid guid) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var gie = ioc.Get<IGeneralInfoEntity>();

                XDocument xDoc = XDocument.Load(generalInfoFilePath);
                gie.FullName = xDoc.Root.Element("FullName").Value;
                gie.Add1 = xDoc.Root.Element("AddLine1").Value;
                gie.Add2 = xDoc.Root.Element("AddLine2").Value;
                gie.Email = xDoc.Root.Element("Email").Value;
                gie.Phone = xDoc.Root.Element("Phone").Value;

                return gie;
            }
        }

        public IList<IGeneralInfoEntity> GetAll() 
        {
            return new List<IGeneralInfoEntity> { Get(Guid.Empty) };
        }

        public Guid Insert(IGeneralInfoEntity entity) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(generalInfoFilePath);
                var guid = Guid.NewGuid();

                var ele = doc.Root;
                if (ele.Attribute("Guid") != null) ele.Attribute("Guid").Value = guid.ToString();
                else ele.Add(new XAttribute("Guid", guid.ToString()));

                ele.Element("FullName").Value = entity.FullName;
                ele.Element("AddLine1").Value = entity.Add1;
                ele.Element("AddLine2").Value = entity.Add2;
                ele.Element("Phone").Value = entity.Phone;
                ele.Element("Email").Value = entity.Email;

                doc.Save(generalInfoFilePath);

                return guid;
            }
        }

        public void Update(Guid guid, IGeneralInfoEntity entity) 
        {
            var doc = XDocument.Load(generalInfoFilePath);

            doc.Root.Element("FullName").Value = entity.FullName;
            doc.Root.Element("AddLine1").Value = entity.Add1;
            doc.Root.Element("AddLine2").Value = entity.Add2;
            doc.Root.Element("Email").Value = entity.Email;
            doc.Root.Element("Phone").Value = entity.Phone;

            doc.Save(generalInfoFilePath);
        }
    }
}
