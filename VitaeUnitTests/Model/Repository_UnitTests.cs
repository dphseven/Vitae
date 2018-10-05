using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Vitae.Model;
using Vitae.Services;

namespace VitaeUnitTests
{
    [TestClass]
    public class EducationRepository_UnitTests
    {
        Guid guidToDelete = Guid.NewGuid();

        [TestInitialize]
        public void Initialize() 
        {

        }

        [TestMethod]
        public void EducationRepository_GetAll_Works() 
        {
            var listToReturn = new List<IEducationEntity>();
            for (int i = 0; i < 315; i++)
            {
                IEducationEntity ee = new EducationEntity();
                ee.Credential = "test credential";
                ee.Institution = "test institution";
                listToReturn.Add(ee);
            }
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.GetAllEducations()).Returns(listToReturn);
            var repos = new EducationRepository(mockXml.Object);

            var list = repos.GetAll();

            Assert.AreEqual(315, list.Count);
            Assert.AreEqual("test credential", list[0].Credential);
            Assert.AreEqual("test institution", list[0].Institution);
        }

        [TestMethod]
        public void EducationRepository_Add_Works() 
        {
            EducationEntity ee = new EducationEntity();
            ee.Credential = "test credential";
            ee.Institution = "test institution";
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Insert(ee));
            var repos = new EducationRepository(mockXml.Object);
            
            var id = repos.Add(ee);

            Assert.IsInstanceOfType(id, typeof(Guid));
            Assert.IsNotNull(id);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentException))]
        public void EducationRepository_Add_Exception1() 
        {
            EducationEntity ee = new EducationEntity();
            ee.Credential = null; // bad!
            ee.Institution = "test institution";
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Insert(ee)).Throws<ArgumentException>();
            var repos = new EducationRepository(mockXml.Object);

            repos.Add(ee);

            Assert.IsTrue(true);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentException))]
        public void EducationRepository_Add_Exception2() 
        {
            EducationEntity ee = new EducationEntity();
            ee.Credential = "test";
            ee.Institution = null; // bad!
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Insert(ee)).Throws<ArgumentException>();
            var repos = new EducationRepository(mockXml.Object);

            repos.Add(ee);

            Assert.IsTrue(true);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentNullException))]
        public void EducationRepository_Add_Exception3() 
        {
            EducationEntity ee = null; // bad!
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Insert(ee)).Throws<ArgumentNullException>();
            var repos = new EducationRepository(mockXml.Object);

            repos.Add(ee);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EducationRepository_Remove_Works() 
        {
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Delete(guidToDelete));
            var repos = new EducationRepository(mockXml.Object);

            repos.Remove(guidToDelete); // If no exception thrown, it passes

            Assert.IsTrue(true);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentNullException))]
        public void EducationRepository_Remove_Exception1() 
        {
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Delete(guidToDelete)).Throws(new ArgumentNullException());
            var repos = new EducationRepository(mockXml.Object);

            repos.Remove(guidToDelete);

            Assert.IsTrue(true);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentNullException))]
        public void EducationRepository_Remove_Exception2() 
        {
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Delete(guidToDelete)).Throws(new ArgumentNullException());
            var repos = new EducationRepository(mockXml.Object);

            repos.Remove(guidToDelete);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void EducationRepository_Update_Works() 
        {
            Guid g = Guid.NewGuid();
            IEducationEntity newEE = new EducationEntity { Credential = "new", Institution = "new" };
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Update(g, newEE));
            var repos = new EducationRepository(mockXml.Object);

            repos.Update(g, newEE);

            Assert.IsTrue(true);
        }

        [TestMethod] [ExpectedException(typeof(ArgumentNullException))]
        public void EducationRepository_Update_Exception1() 
        {
            Guid g = Guid.NewGuid();
            IEducationEntity newEE = null; // bad!
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.Update(g, newEE)).Throws(new ArgumentNullException());
            var repos = new EducationRepository(mockXml.Object);

            repos.Update(g, newEE);
        }

        [TestMethod]
        public void EducationRepository_Get_Works() 
        {
            Guid g = Guid.NewGuid();
            IEducationEntity ee = new EducationEntity { Credential = "c", Institution = "i" };
            var mockXml = new Mock<IXMLService>();
            mockXml.Setup(T => T.GetEducation(g)).Returns(ee);
            var repos = new EducationRepository(mockXml.Object);

            var edItem = repos.Get(g);

            Assert.IsNotNull(edItem);
            Assert.AreEqual("c", edItem.Credential);
            Assert.AreEqual("i", edItem.Institution);
        }

    }
}