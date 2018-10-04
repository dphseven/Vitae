using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitae.Model;

namespace VitaeUnitTests
{
    [TestClass()]
    public class XMLService_UnitTests
    {
        readonly IXMLService xs = new XMLService();

        [TestInitialize]
        public void Initialize() 
        {

        }

        [TestMethod()]
        public void XMLService_Works() 
        {
            Assert.IsTrue(xs is IXMLService);
        }

        [TestMethod()]
        public void GetJobTitles_Works() 
        {
            var titles = xs.GetJobTitles();

            Assert.IsNotNull(titles);
            Assert.IsTrue(titles.Count > 0);
        }

        [TestMethod()]
        public void GetExperienceDetails_Works() 
        {
            var details = xs.GetExperienceDetails();

            Assert.IsNotNull(details);
            Assert.IsTrue(details.Count > 0);
        }

        [TestMethod()]
        public void GetExperienceDetailsForEmployer_Works() 
        {
            var details = xs.GetExperienceDetailsForEmployer("Morgan Stanley");

            Assert.IsNotNull(details);
            Assert.IsTrue(details.Count > 0);
        }

        [TestMethod()]
        public void GetAllJobs_Works() 
        {
            var jobs = xs.GetAllJobs();

            Assert.IsNotNull(jobs);
            Assert.IsTrue(jobs.Count > 0);
            Assert.IsNotNull(jobs[0].Details);
            Assert.IsTrue(jobs[0].Details.Count > 0);
            Assert.IsNotNull(jobs[0].Titles);
            Assert.IsTrue(jobs[0].Titles.Count > 0);
        }

        [TestMethod()]
        public void GetGeneralInformation_Works() 
        {
            var gi = xs.GetGeneralInformation(Guid.Empty);

            Assert.IsNotNull(gi);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.FullName));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Add1));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Add2));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Email));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Phone));
        }

        [TestMethod()]
        public void Insert_EducationEntity_Works() 
        {
            var newEdEnt = new EducationEntity 
            {
                Credential = "testCred",
                Institution = "testInst"
            };
            var initialList = xs.GetAllEducations();

            var newGuid = xs.Insert(newEdEnt);
            var updatedList = xs.GetAllEducations();
            Assert.IsNotNull(newGuid);
            Assert.AreNotEqual(Guid.Empty, newGuid);
            Assert.AreEqual(initialList.Count + 1, updatedList.Count);

            var retrievedEnt = xs.GetEducation(newGuid);
            Assert.AreEqual("testCred", retrievedEnt.Credential);
            Assert.AreEqual("testInst", retrievedEnt.Institution);
        }

        [TestMethod()]
        public void GetAllGeneralInfos_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void DeleteGeneralInfo_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Update_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetAllEducations_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetEducation_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Insert_Works1()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Delete_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Update_Works1()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetPublications_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Insert_Works2()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetPublication_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void DeletePublication_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Update_Works2()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetExpertise_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Insert_Works3()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetExpertiseItem_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void DeleteExpertise_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Update_Works3()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Insert_Works4()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetExperience_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void GetAllExperiences_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void DeleteExperience_Works()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void Update_Works4()
        {
            Assert.Inconclusive();
        }
    }
}