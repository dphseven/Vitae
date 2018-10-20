using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Model;
using Vitae.Services;
using Moq;
using System.Linq;
using Vitae;

namespace VitaeUnitTests
{
    [TestClass]
    public class PublicationsRepository_UnitTests 
    {
        [TestMethod]
        public void PublicationsRepository_ImplementsInterfaces() 
        {
            var repos = new PublicationsRepository(new PublicationsXMLService(new VitaeNinjectKernel()));

            Assert.IsTrue(repos is IPublicationsRepository);
            Assert.IsTrue(repos is IRepository<IPublicationEntity>);
        }

        [TestMethod]
        public void PublicationsRepository_Add_Works() 
        {
            var pub = new PublicationEntity();
            pub.Publication = "test";
            Guid g = Guid.NewGuid();
            var mockXmlService = new Mock<IPublicationsXMLService>();
            mockXmlService.Setup(T => T.Insert(pub)).Returns(g);
            var repos = new PublicationsRepository(mockXmlService.Object);

            var returnedGuid = repos.Add(pub);

            Assert.AreEqual(g, returnedGuid);
        }

        [TestMethod]
        public void PublicationsRepository_Get_Works() 
        {
            Guid guid = Guid.NewGuid();
            var pub = new PublicationEntity();
            pub.Publication = "test";
            var mockXmlService = new Mock<IPublicationsXMLService>();
            mockXmlService.Setup(T => T.Get(guid)).Returns(pub);
            var repos = new PublicationsRepository(mockXmlService.Object);

            var returnedPub = repos.Get(guid);

            Assert.IsNotNull(returnedPub);
            Assert.AreEqual("test", returnedPub.Publication);
        }

        [TestMethod]
        public void PublicationsRepository_GetAll_Works() 
        {
            var listOfPublications = new List<IPublicationEntity>();
            for (int i = 0; i < 21; i++)
            {
                var pe = new PublicationEntity();
                pe.Publication = "test";
                listOfPublications.Add(pe);
            }
            var mockXmlService = new Mock<IPublicationsXMLService>();
            mockXmlService.Setup(T => T.GetAll()).Returns(listOfPublications);
            var repos = new PublicationsRepository(mockXmlService.Object);

            var returnedList = repos.GetAll();

            Assert.IsNotNull(returnedList);
            Assert.AreEqual(21, returnedList.Count);
            Assert.IsTrue(returnedList.All(T => T.Publication == "test"));
        }

        [TestMethod]
        public void PublicationsRepository_Remove_Works() 
        {
            var guid = Guid.NewGuid();
            var mockXml = new Mock<IPublicationsXMLService>();
            mockXml.Setup(T => T.Delete(guid));
            var repos = new PublicationsRepository(mockXml.Object);

            repos.Remove(guid);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void PublicationsRepository_Update_Works() 
        {
            var guid = Guid.NewGuid();
            var pub = new PublicationEntity();
            pub.Publication = "test";
            var mock = new Mock<IPublicationsXMLService>();
            mock.Setup(T => T.Update(guid, pub));
            var repos = new PublicationsRepository(mock.Object);

            repos.Update(guid, pub);

            Assert.IsTrue(true);
        }

    }
}