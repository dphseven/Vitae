using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System;
using System.Linq;
using Vitae.Model;
using Vitae;

namespace VitaeUnitTests
{
    [TestClass()]
    public class PublicationsXMLService_UnitTests
    {
        PublicationsXMLService serv = new PublicationsXMLService(new VitaeNinjectKernel(), new PersistenceService());

        [TestMethod()]
        public void PublicationsXMLService_Delete_Works() 
        {
            // Deleting one that exists works

            var ent = new PublicationEntity();
            var id = serv.Insert(ent);
            var entGot = serv.Get(id);
            Assert.IsNotNull(entGot);

            serv.Delete(id);
            var entGot2 = serv.Get(id);
            Assert.IsNull(entGot2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PublicationsXMLService_Delete_Exception1() 
        {
            // Deleting one that doesn't exist throws argumentexception

            serv.Delete(Guid.NewGuid());
        }

        [TestMethod()]
        public void PublicationsXMLService_Get_Works() 
        {
            // Getting one that exists gets it

            var id = serv.Insert(new PublicationEntity { Publication = "test" });
            var ent1 = serv.Get(id);
            Assert.IsNotNull(ent1);
            Assert.AreEqual("test", ent1.Publication);

            // Getting one that doesn't exist returns null

            var ent2 = serv.Get(Guid.NewGuid());
            Assert.IsNull(ent2);
        }

        [TestMethod()]
        public void PublicationsXMLService_GetAll_Works() 
        {
            // Method returns multiple items and loads their values

            var list = serv.GetAll();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Publication)));
        }

        [TestMethod()]
        public void PublicationsXMLService_Insert_Works() 
        {
            // Insert one with a text publication

            var pub1 = new PublicationEntity { Publication = "test" };
            var id1 = serv.Insert(pub1);
            var ent1 = serv.Get(id1);

            Assert.IsNotNull(id1);
            Assert.IsNotNull(ent1);
            Assert.AreEqual("test", ent1.Publication);

            // Insert one with a "" publication

            var pub2 = new PublicationEntity();
            var id2 = serv.Insert(pub2);
            var ent2 = serv.Get(id2);

            Assert.IsNotNull(id2);
            Assert.IsNotNull(ent2);
            Assert.AreEqual(string.Empty, ent2.Publication);
        }

        [TestMethod()]
        public void PublicationsXMLService_Update_Works() 
        {
            // Updating one that exists works

            var id1 = serv.Insert(new PublicationEntity { Publication = "initial" });
            var initialItem1 = serv.Get(id1);

            serv.Update(id1, new PublicationEntity { Publication = "updated" });
            Assert.AreEqual("updated", serv.Get(id1).Publication);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PublicationsXMLService_Update_Exception1() 
        {
            // Updating one that doesn't exist throws argumentexception
            serv.Update(Guid.NewGuid(), new PublicationEntity { Publication = "updated" });
        }
    }
}