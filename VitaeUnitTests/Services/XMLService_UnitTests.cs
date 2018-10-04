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
        public void DeleteGeneralInfo_Works() 
        {
            xs.DeleteGeneralInfo(Guid.Empty);
            var newGI = xs.GetGeneralInformation(Guid.Empty);

            Assert.IsNotNull(newGI);
            Assert.AreEqual(string.Empty, newGI.FullName);
            Assert.AreEqual(string.Empty, newGI.Add1);
            Assert.AreEqual(string.Empty, newGI.Add2);
            Assert.AreEqual(string.Empty, newGI.Phone);
            Assert.AreEqual(string.Empty, newGI.Email);
        }

        [TestMethod()]
        public void Update_EducationEntity_Works() 
        {
            IEducationEntity ee = new EducationEntity 
            {
                Credential = "testCred",
                Institution = "testInst"
            };
            var g = xs.Insert(ee);
            IEducationEntity eeNew = new EducationEntity 
            {
                Credential = "updated",
                Institution = "updated"
            };

            xs.Update(g, eeNew);

            var eeNewest = xs.GetEducation(g);

            Assert.AreEqual("updated", eeNewest.Credential);
            Assert.AreEqual("updated", eeNewest.Institution);
        }

        [TestMethod()]
        public void GetAllEducations_Works() 
        {
            var list = xs.GetAllEducations();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.AreNotEqual(string.Empty, list[0].Credential);
            Assert.AreNotEqual(string.Empty, list[0].Institution);
        }

        [TestMethod()]
        public void GetEducation_Works() 
        {
            var tbInserted = new EducationEntity
            {
                Credential = "testCred",
                Institution = "testInst"
            };
            var g = xs.Insert(tbInserted);

            var ed = xs.GetEducation(g);

            Assert.IsNotNull(ed);
            Assert.AreEqual("testCred", ed.Credential);
            Assert.AreEqual("testInst", ed.Institution);
        }

        [TestMethod()]
        public void Insert_GeneralInfoEntity_Works() 
        {
            var initialGI = new GeneralInfoEntity
            {
                FullName = "test1",
                Add1 = "test2",
                Add2 = "test3",
                Email = "test4",
                Phone = "test5"
            };

            var guid = xs.Insert(initialGI);
            var gie = xs.GetGeneralInformation(guid);

            Assert.IsNotNull(guid);
            Assert.AreNotEqual(Guid.Empty, guid);
            Assert.AreEqual("test1", gie.FullName);
            Assert.AreEqual("test2", gie.Add1);
            Assert.AreEqual("test3", gie.Add2);
            Assert.AreEqual("test4", gie.Email);
            Assert.AreEqual("test5", gie.Phone);
        }

        [TestMethod()]
        public void Delete_Education_Works() 
        {
            var edInsert = new EducationEntity();
            var g = xs.Insert(edInsert);
            var result1 = xs.GetEducation(g);
            Assert.IsNotNull(result1);

            xs.Delete(g);

            var result2 = xs.GetEducation(g);
            Assert.IsNull(result2);
        }

        [TestMethod()]
        public void Update_ExperienceEntity_Works() 
        {
            var ee1 = new ExperienceEntity
            {
                Employer = "testEmp",
                EndDate = "end1", 
                StartDate = "start1",
                Titles = new List<string> { "t1", "t2", "t3" }, 
                Details = new List<string> { "d1", "d2", "d3" }
            };
            var g = xs.Insert(ee1);
            var ee2 = new ExperienceEntity
            {
                Employer = "upd",
                EndDate = "upd",
                StartDate = "upd",
                Titles = new List<string> { "updt1", "updt2", "updt3" },
                Details = new List<string> { "updd1", "updd2", "updd3" }
            };
            
            xs.Update(g, ee2);

            var final = xs.GetExperience(g);

            Assert.IsNotNull(final);
            Assert.AreEqual("upd", final.Employer);
            Assert.AreEqual("upd", final.EndDate);
            Assert.AreEqual("upd", final.StartDate);
            Assert.AreEqual("updt1", final.Titles[0]);
            Assert.AreEqual("updt2", final.Titles[1]);
            Assert.AreEqual("updt3", final.Titles[2]);
            Assert.AreEqual("updd1", final.Details[0]);
            Assert.AreEqual("updd2", final.Details[1]);
            Assert.AreEqual("updd3", final.Details[2]);
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