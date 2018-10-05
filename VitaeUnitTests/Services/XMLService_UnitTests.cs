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
        public void XMLService_XMLService_Works() 
        {
            Assert.IsTrue(xs is IXMLService);
        }

        // General Info

        [TestMethod()]
        public void XMLService_DeleteGeneralInfo_Works() 
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
        public void XMLService_GetGeneralInformation_Works() 
        {
            var gie = new GeneralInfoEntity
            {
                Add1 = "a",
                Add2 = "a",
                FullName = "a",
                Phone = "a",
                Email = "a"
            };
            xs.Update(Guid.Empty, gie);

            var gi = xs.GetGeneralInformation(Guid.Empty);

            Assert.IsNotNull(gi);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.FullName));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Add1));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Add2));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Email));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(gi.Phone));
        }

        [TestMethod()]
        public void XMLService_Insert_GeneralInfoEntity_Works() 
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
        public void XMLService_Update_GeneralInfoEntity_Works() 
        {
            var gie1 = new GeneralInfoEntity 
            {
                FullName = "f",
                Add1 = "a1",
                Add2 = "a2",
                Email = "e",
                Phone = "p"
            };
            var guid = xs.Insert(gie1);
            var gie2 = new GeneralInfoEntity
            {
                FullName = "uf",
                Add1 = "ua1",
                Add2 = "ua2",
                Email = "ue",
                Phone = "up"
            };

            xs.Update(guid, gie2);

            var gie3 = xs.GetGeneralInformation(guid);

            Assert.IsNotNull(gie3);
            Assert.AreEqual("uf", gie3.FullName);
            Assert.AreEqual("ua1", gie3.Add1);
            Assert.AreEqual("ua2", gie3.Add2);
            Assert.AreEqual("ue", gie3.Email);
            Assert.AreEqual("up", gie3.Phone);
        }

        // Expertise

        [TestMethod()]
        public void XMLService_DeleteExpertise_Works() 
        {
            var num1EE = xs.GetExpertise().Count;
            var ee = new ExpertiseEntity();
            var guid = xs.Insert(ee);
            var num2EE = xs.GetExpertise().Count;
            Assert.AreEqual(num1EE + 1, num2EE);

            xs.DeleteExpertise(guid);
            var num3EE = xs.GetExpertise().Count;
            Assert.AreEqual(num1EE, num3EE);
        }

        [TestMethod()]
        public void XMLService_GetExpertise_Works() 
        {
            var list = xs.GetExpertise();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void XMLService_GetExpertiseItem_Works() 
        {
            var ee = new ExpertiseEntity
            {
                Category = "cat",
                Expertise = "exp"
            };
            var id = xs.Insert(ee);
            var item = xs.GetExpertiseItem(id);

            Assert.IsNotNull(item);
            Assert.AreEqual("cat", item.Category);
            Assert.AreEqual("exp", item.Expertise);
        }

        [TestMethod()]
        public void XMLService_Insert_ExpertiseEntity_Works() 
        {
            IExpertiseEntity ee = new ExpertiseEntity
            {
                Category = "testCat",
                Expertise = "testExp"
            };
            var g = xs.Insert(ee);

            Assert.IsNotNull(g);
            Assert.AreNotEqual(Guid.Empty, g);

            var newEE = xs.GetExpertiseItem(g);
            Assert.AreEqual("testCat", newEE.Category);
            Assert.AreEqual("testExp", newEE.Expertise);
        }

        [TestMethod()]
        public void XMLService_Update_ExpertiseEntity_Works() 
        {
            var ee = new ExpertiseEntity
            {
                Category = "testCat",
                Expertise = "testExp"
            };
            var g = xs.Insert(ee);

            var updatedEE = new ExpertiseEntity
            {
                Category = "testCat",
                Expertise = "testExp"
            };
            xs.Update(g, updatedEE);

            var final = xs.GetExpertiseItem(g);

            Assert.AreEqual("testCat", final.Category);
            Assert.AreEqual("testExp", final.Expertise);
        }

        // Experience

        [TestMethod()]
        public void XMLService_Update_ExperienceEntity_Works() 
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
        public void XMLService_GetExperience_Works() 
        {
            var ee = new ExperienceEntity
            {
                Employer = "emp",
                StartDate = "sd",
                EndDate = "ed",
                Titles = new List<string> { "t1", "t2", "t3" },
                Details = new List<string> { "d1", "d2", "d3" }
            };
            var g = xs.Insert(ee);

            var newItem = xs.GetExperience(g);

            Assert.IsNotNull(newItem);
            Assert.AreEqual("emp", newItem.Employer);
            Assert.AreEqual("sd", newItem.StartDate);
            Assert.AreEqual("ed", newItem.EndDate);
            Assert.AreEqual("t2", newItem.Titles[1]);
            Assert.AreEqual("d3", newItem.Details[2]);
        }

        [TestMethod()]
        public void XMLService_GetAllExperiences_Works() 
        {
            var list = xs.GetAllExperiences();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void XMLService_DeleteExperience_Works() 
        {
            var ee1 = new ExperienceEntity();
            var id = xs.Insert(ee1);

            var count = xs.GetAllExperiences().Count;

            xs.DeleteExperience(id);

            Assert.AreEqual(count - 1, xs.GetAllExperiences().Count);
        }

        [TestMethod()]
        public void XMLService_Insert_ExperienceEntity_Works() 
        {
            var ee = new ExperienceEntity
            {
                Employer = "e",
                StartDate = "s",
                EndDate = "e",
                Titles = new List<string> { "t1", "t2", "t3" },
                Details = new List<string> { "d1", "d2", "d3" }
            };
            var id = xs.Insert(ee);
            var reloaded = xs.GetExperience(id);

            Assert.IsNotNull(reloaded);
            Assert.AreEqual("e", reloaded.Employer);
            Assert.AreEqual("s", reloaded.StartDate);
            Assert.AreEqual("e", reloaded.EndDate);
            Assert.AreEqual("t1", reloaded.Titles[0]);
            Assert.AreEqual("d2", reloaded.Details[1]);
        }

        // Education

        [TestMethod()]
        public void XMLService_Update_EducationEntity_Works() 
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
        public void XMLService_GetAllEducations_Works() 
        {
            var list = xs.GetAllEducations();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.AreNotEqual(string.Empty, list[0].Credential);
            Assert.AreNotEqual(string.Empty, list[0].Institution);
        }

        [TestMethod()]
        public void XMLService_GetEducation_Works() 
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
        public void XMLService_Insert_EducationEntity_Works() 
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
        public void XMLService_Delete_Education_Works() 
        {
            var edInsert = new EducationEntity();
            var g = xs.Insert(edInsert);
            var result1 = xs.GetEducation(g);
            Assert.IsNotNull(result1);

            xs.Delete(g);

            var result2 = xs.GetEducation(g);
            Assert.IsNull(result2);
        }

        // Publications

        [TestMethod()]
        public void XMLService_GetPublications_Works() 
        {
            var list = xs.GetPublications();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void XMLService_GetPublication_Works() 
        {
            PublicationEntity pe = new PublicationEntity
            {
                Publication = "testPub",
            };
            var g = xs.Insert(pe);
            var pub = xs.GetPublication(g);

            Assert.IsNotNull(pub);
            Assert.AreEqual("testPub", pub.Publication);
        }

        [TestMethod()]
        public void XMLService_DeletePublication_Works() 
        {
            var count1 = xs.GetPublications().Count;
            var pe = new PublicationEntity { Publication = "someTestPub" };
            var id = xs.Insert(pe);
            var count2 = xs.GetPublications().Count;
            Assert.AreEqual(count1 + 1, count2);

            xs.DeletePublication(id);
            var count3 = xs.GetPublications().Count;
            Assert.AreEqual(count1, count3);
        }

        [TestMethod()]
        public void XMLService_Update_PublicationEntity_Works() 
        {
            var pe = new PublicationEntity { Publication = "pub" };
            var id = xs.Insert(pe);
            pe = null;

            pe = new PublicationEntity { Publication = "updatedPub" };
            xs.Update(id, pe);

            var newItem = xs.GetPublication(id);
            Assert.IsNotNull(newItem);
            Assert.AreEqual("updatedPub", newItem.Publication);
        }

        [TestMethod()]
        public void XMLService_Insert_PublicationEntity_Works() 
        {
            var pe = new PublicationEntity { Publication = "anotherTestPub" };

            var id = xs.Insert(pe);

            Assert.IsNotNull(id);
            Assert.AreNotEqual(Guid.Empty, id);
        }

    }
}