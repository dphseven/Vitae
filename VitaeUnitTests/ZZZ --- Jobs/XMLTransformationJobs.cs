using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae;
using Vitae.Model;
using Ninject;

namespace VitaeUnitTests
{
    //[TestClass]
    public class XMLTransformationJobs 
    {
        //[TestMethod]
        public void CreateGuidForEachPublication_DONE() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var serv = ioc.Get<IPublicationsRepository>();

                foreach (var item in serv.GetAll())
                {
                    serv.Add(item);
                }
            }
        }

        //[TestMethod]
        public void CreateGuidForGeneralInfo_DONE() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var serv = ioc.Get<IGeneralInfoRepository>();

                foreach (var item in serv.GetAll())
                {
                    serv.Add(item);
                }
            }
        }

        //[TestMethod]
        public void CreateGuidForEachExpertise_DONE() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var serv = ioc.Get<IExpertiseRepository>();

                foreach (var item in serv.GetAll())
                {
                    serv.Add(item);
                }
            }
        }

        //[TestMethod]
        public void CreateGuidForEachEducation_DONE() 
        
{
            using (var ioc = new VitaeNinjectKernel())
            {
                var serv = ioc.Get<IEducationRepository>();

                foreach (var item in serv.GetAll())
                {
                    serv.Add(item);
                }
            }
        }

        //[TestMethod]
        public void CreateGuidForEachExperience_DONE() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var serv = ioc.Get<IExperienceRepository>();

                foreach (var item in serv.GetAll())
                {
                    serv.Add(item);
                }
            }
        }

    }
}