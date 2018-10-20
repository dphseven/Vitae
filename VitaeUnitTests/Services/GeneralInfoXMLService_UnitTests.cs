using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using Vitae.Model;

namespace Vitae.Services_UnitTests
{
    [TestClass()]
    public class GeneralInfoXMLService_UnitTests
    {
        GeneralInfoXMLService xs = new GeneralInfoXMLService(new VitaeNinjectKernel(), new PersistenceService());

        [TestMethod()]
        public void GeneralInfoXMLService_Works() 
        {
            Assert.IsTrue(xs is IGeneralInfoXMLService);
            Assert.IsTrue(xs is IXMLService<IGeneralInfoEntity>);
        }

        [TestMethod()]
        public void GeneralInfoXMLService_Delete_Works() 
        {
            var id = xs.Insert(new GeneralInfoEntity
            {
                FullName = "fn",
                Add1 = "a1",
                Add2 = "a2",
                Phone = "p",
                Email = "e"
            });
            xs.Delete(id);

            var entity = xs.Get(id);

            Assert.IsNotNull(entity);
            Assert.AreEqual(string.Empty, entity.FullName);
            Assert.AreEqual(string.Empty, entity.Add1);
            Assert.AreEqual(string.Empty, entity.Add2);
            Assert.AreEqual(string.Empty, entity.Email);
            Assert.AreEqual(string.Empty, entity.Phone);
        }

        [TestMethod()]
        public void GeneralInfoXMLService_Get_Works() 
        {
            var id = xs.Insert(new GeneralInfoEntity
            {
                FullName = "fn",
                Add1 = "a1",
                Add2 = "a2",
                Phone = "p",
                Email = "e"
            });

            var gie = xs.Get(id);

            Assert.AreEqual("fn", gie.FullName);
            Assert.AreEqual("a1", gie.Add1);
            Assert.AreEqual("a2", gie.Add2);
            Assert.AreEqual("e", gie.Email);
            Assert.AreEqual("p", gie.Phone);
        }

        [TestMethod()]
        public void GeneralInfoXMLService_GetAll_Works() 
        {
            var list = xs.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod()]
        public void GeneralInfoXMLService_Insert_Works() 
        {
            var id = xs.Insert(new GeneralInfoEntity
            {
                FullName = "fn",
                Add1 = "a1",
                Add2 = "a2",
                Phone = "p",
                Email = "e"
            });
            var gie = xs.Get(id);

            Assert.AreEqual(1, xs.GetAll().Count);
            Assert.AreEqual("fn", gie.FullName);
            Assert.AreEqual("a1", gie.Add1);
            Assert.AreEqual("a2", gie.Add2);
            Assert.AreEqual("e", gie.Email);
            Assert.AreEqual("p", gie.Phone);
        }

        [TestMethod()]
        public void GeneralInfoXMLService_Update_Works() 
        {
            var id = xs.Insert(new GeneralInfoEntity
            {
                FullName = "",
                Add1 = "",
                Add2 = "",
                Phone = "",
                Email = ""
            });

            xs.Update(id, new GeneralInfoEntity
            {
                FullName = "1",
                Add1 = "2",
                Add2 = "3",
                Phone = "4",
                Email = "5"
            });
            var gie = xs.Get(id);

            Assert.AreEqual(gie.FullName, "1");
            Assert.AreEqual(gie.Add1, "2");
            Assert.AreEqual(gie.Add2, "3");
            Assert.AreEqual(gie.Phone, "4");
            Assert.AreEqual(gie.Email, "5");
        }
    }
}