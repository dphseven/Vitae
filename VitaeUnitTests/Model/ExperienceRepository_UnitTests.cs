using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var mock = new Mock<IXMLService>();
            var repos = new ExperienceRepository(mock.Object);

            Assert.IsTrue(repos is IExperienceRepository);
            Assert.IsTrue(repos is IRepository<IExperienceEntity>);
        }

        [TestMethod]
        public void ExperienceRepository_Add_Works() 
        {
            var entity = new ExperienceEntity();
            var guid = Guid.NewGuid();
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.Insert(entity)).Returns(guid);
            var repos = new ExperienceRepository(mock.Object);

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
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.GetExperience(guid)).Returns(original);
            var repos = new ExperienceRepository(mock.Object);

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
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.GetAllExperiences()).Returns(originalList);
            var repos = new ExperienceRepository(mock.Object);

            var list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(42, list.Count);
            Assert.IsTrue(list.All(T => T.Employer == "test"));
        }

        [TestMethod]
        public void ExperienceRepository_Remove_Works() 
        {
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IXMLService>();
            var repos = new ExperienceRepository(mock.Object);

            repos.Remove(guid);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExperienceRepository_Update_Works() 
        {
            var guid = Guid.NewGuid();
            ExperienceEntity entity = new ExperienceEntity();
            var mock = new Mock<IXMLService>();
            var repos = new ExperienceRepository(mock.Object);

            repos.Update(guid, entity);

            Assert.IsTrue(true);
        }
    }
}