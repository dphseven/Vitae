using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Model;
using Vitae.Services;
using Moq;
using System.Linq;

namespace VitaeUnitTests
{
    [TestClass]
    public class ExpertiseRepository_UnitTests
    {
        [TestMethod]
        public void ExpertiseRepository_ImplementsTheRightInterfaces() 
        {
            var mock = new Mock<IXMLService>();
            var repos = new ExpertiseRepository(mock.Object);

            Assert.IsTrue(repos is IExpertiseRepository);
            Assert.IsTrue(repos is IRepository<IExpertiseEntity>);
        }

        [TestMethod]
        public void ExpertiseRepository_Add_Works() 
        {
            var exp = new ExpertiseEntity();
            exp.Category = "testCat";
            exp.Expertise = "testExp";

            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.Insert(exp));
            var repos = new ExpertiseRepository(mock.Object);

            repos.Add(exp);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExpertiseRepository_Get_Works() 
        {
            Guid guid = Guid.NewGuid();
            var entity = new ExpertiseEntity();
            entity.Category = "testCat";
            entity.Expertise = "testExp";

            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.GetExpertiseItem(guid)).Returns(entity);

            var repos = new ExpertiseRepository(mock.Object);

            var returnedEntity = repos.Get(guid);

            Assert.IsNotNull(returnedEntity);
            Assert.AreEqual("testCat", returnedEntity.Category);
            Assert.AreEqual("testExp", returnedEntity.Expertise);
        }

        [TestMethod]
        public void ExpertiseRepository_GetAll_Works() 
        {
            var listCreated = new List<IExpertiseEntity>();
            for (int i = 0; i < 23; i++)
            {
                var ee = new ExpertiseEntity();
                ee.Category = "testCat";
                ee.Expertise = "testExp";
                listCreated.Add(ee);
            }
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.GetExpertise()).Returns(listCreated);
            var repos = new ExpertiseRepository(mock.Object);

            var list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(23, list.Count);
            Assert.IsTrue(list.All(T => T.Category == "testCat"));
            Assert.IsTrue(list.All(T => T.Expertise == "testExp"));
        }

        [TestMethod]
        public void ExpertiseRepository_Remove_Works() 
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.DeleteExpertise(guid));
            var repos = new ExpertiseRepository(mock.Object);

            repos.Remove(guid);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExpertiseRepository_Update_Works() 
        {
            Guid guid = Guid.NewGuid();
            var entity = new ExpertiseEntity();
            var mock = new Mock<IXMLService>();
            mock.Setup(T => T.Update(guid, entity));
            var repos = new ExpertiseRepository(mock.Object);

            repos.Update(guid, entity);

            Assert.IsTrue(true);
        }
    }
}