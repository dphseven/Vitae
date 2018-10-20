using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using Vitae;
using Vitae.Model;
using Vitae.Services;

namespace VitaeUnitTests 
{
    [TestClass]
    public class ExperienceRepository_UnitTests 
    {
        [TestMethod]
        public void ExperienceRepository_ImplementsTheRightInterfaces() 
        {
            var mockXml = new Mock<IExperienceXMLService>();
            var mockKernel = new Mock<IKernel>();
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            Assert.IsTrue(repos is IExperienceRepository);
            Assert.IsTrue(repos is IRepository<IExperienceEntity>);
        }

        [TestMethod]
        public void ExperienceRepository_Add_Works() 
        {
            var entity = new ExperienceEntity();
            var guid = Guid.NewGuid();
            var mockXml = new Mock<IExperienceXMLService>();
            var mockKernel = new Mock<IKernel>();
            mockXml.Setup(T => T.Insert(entity)).Returns(guid);
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            Guid newGuid = repos.Add(entity);

            Assert.IsNotNull(newGuid);
            Assert.AreEqual(guid, newGuid);
        }

        [TestMethod]
        public void ExperienceRepository_Get_Works() 
        {
            var original = new ExperienceEntity();
            original.Employer = "test";
            var guid = Guid.NewGuid();
            var mockXml = new Mock<IExperienceXMLService>();
            var mockKernel = new Mock<IKernel>();
            mockXml.Setup(T => T.Get(guid)).Returns(original);
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            var itemReturned = repos.Get(guid);

            Assert.IsNotNull(itemReturned);
            Assert.AreEqual(original.Employer, itemReturned.Employer);
        }

        [TestMethod]
        public void ExperienceRepository_GetAll_Works() 
        {
            var originalList = new List<IExperienceEntity>();
            for (int i = 0; i < 42; i++)
            {
                var item = new ExperienceEntity();
                item.Employer = "test";
                originalList.Add(item);
            }
            var mockXml = new Mock<IExperienceXMLService>();
            var mockKernel = new Mock<IKernel>();
            mockXml.Setup(T => T.GetAll()).Returns(originalList);
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            var list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(42, list.Count);
            Assert.IsTrue(list.All(T => T.Employer == "test"));
        }
        
        [TestMethod]
        public void ExperienceRepository_Remove_Works() 
        {
            Guid guid = Guid.NewGuid();
            var mockXml = new Mock<IExperienceXMLService>();
            var mockKernel = new Mock<IKernel>();
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            repos.Remove(guid);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExperienceRepository_Update_Works() 
        {
            var guid = Guid.NewGuid();
            ExperienceEntity entity = new ExperienceEntity();
            var mockXml = new Mock<IExperienceXMLService>();
            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            repos.Update(guid, entity);

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GetAllExperienceItems_Works() 
        {
            var list = new List<IExperienceEntity>();
            for (int i = 0; i < 34; i++)
            {
                list.Add(new ExperienceEntity { Details = new List<string> { "asdf", "sdf" } });
            }
            var mockXml = new Mock<IExperienceXMLService>();
            mockXml.Setup(T => T.GetAll()).Returns(list);

            var repos = new ExperienceRepository(mockXml.Object, new VitaeNinjectKernel());

            var newList = repos.GetAllExperienceItems();

            Assert.IsNotNull(newList);
            Assert.AreEqual(34 * 2, newList.Count);
        }

        [TestMethod()]
        public void GetExperienceDetailsForEmployer_Works() 
        {
            var employer = Guid.NewGuid().ToString();

            var experienceEntity = new ExperienceEntity
            {
                Employer = employer,
                Details = new List<string> { "1", "2", "3", "4", "5" }
            };

            var mock = new Mock<IExperienceXMLService>();
            mock.Setup(T => T.GetAll()).Returns(new List<IExperienceEntity> { experienceEntity });
        
            var repos = new ExperienceRepository(mock.Object, new VitaeNinjectKernel());

            var list = repos.GetExperienceDetailsForEmployer(employer);

            Assert.IsNotNull(list);
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(employer, list[2].Employer);
            Assert.AreEqual("3", list[2].ExperienceDetail);
        }
    }
}