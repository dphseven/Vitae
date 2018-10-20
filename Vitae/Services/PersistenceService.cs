namespace Vitae.Services
{
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Vitae.Model;

    public class PersistenceService : IPersistenceService 
    {
        private Dictionary<Type, string> filePaths =  new Dictionary<Type, string>();

        public PersistenceService() 
        {
            RegisterFilePaths();
        }

        /// <exception cref="ArgumentException" />
        public void Persist<T>(XDocument document) where T : class, IEntity
        {
            try
            {
                if (HasRegistration(typeof(T)))
                {
                    var filePath = filePaths[typeof(T)];
                    document.Save(filePath);
                }
                else throw new ArgumentException("The supplied type argument is not registered in the PersistenceService.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public XDocument Load<T>() where T : class, IEntity
        {
            try
            {
                if (HasRegistration(typeof(T)))
                {
                    var filePath = filePaths[typeof(T)];
                    return XDocument.Load(filePath);
                }
                else throw new ArgumentException("The supplied type argument is not registered in the PersistenceService.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool HasRegistration(Type type) 
        {
            return filePaths.ContainsKey(type);
        }

        private void RegisterFilePaths() 
        {
            string prefix = string.Empty;
            if (ApplicationDeployment.IsNetworkDeployed) prefix = ApplicationDeployment.CurrentDeployment.DataDirectory;
            else prefix = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            filePaths.Add(typeof(IEducationEntity), prefix + @"\XML\Education.xml");
            filePaths.Add(typeof(IExperienceEntity), prefix + @"\XML\Experience.xml");
            filePaths.Add(typeof(IExpertiseEntity), prefix + @"\XML\Expertise.xml");
            filePaths.Add(typeof(IGeneralInfoEntity), prefix + @"\XML\GeneralInfo.xml");
            filePaths.Add(typeof(IPublicationEntity), prefix + @"\XML\Publications.xml");
        }

    }
}