using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System;
using System.Linq;
using Vitae.Model;

namespace Vitae.Services_UnitTests
{
    [TestClass()]
    public class ExpertiseXMLService_UnitTests
    {
        ExpertiseXMLService xs = new ExpertiseXMLService(new VitaeNinjectKernel());

        [TestMethod()]
        public void ExpertiseXMLService_Works() 
        {
            Assert.IsTrue(xs is IExpertiseXMLService);
            Assert.IsTrue(xs is IXMLService<IExpertiseEntity>);
        }

        [TestMethod()]
        public void ExpertiseXMLService_GetAll_Works() 
        {
            var list = xs.GetAll();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Category)));
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Expertise)));
        }

        [TestMethod()]
        public void ExpertiseXMLService_Insert_Works() 
        {
            var id = xs.Insert(
                new ExpertiseEntity { Category = "cat", Expertise = "exp" });

            Assert.IsNotNull(id);
            Assert.AreNotEqual(Guid.Empty, id);

            var entity = xs.Get(id);
            Assert.IsNotNull(entity);
            Assert.AreEqual("cat", entity.Category);
            Assert.AreEqual("exp", entity.Expertise);
        }

        [TestMethod()]
        public void ExpertiseXMLService_Get_Works() 
        {
            var id = xs.Insert(
                new ExpertiseEntity { Category = "cat", Expertise = "exp" });

            var entity = xs.Get(id);

            Assert.IsNotNull(entity);
            Assert.AreEqual("cat", entity.Category);
            Assert.AreEqual("exp", entity.Expertise);
        }

        [TestMethod()]
        public void ExpertiseXMLService_Delete_Works() 
        {
            var id = xs.Insert(new ExpertiseEntity());
            var initialCount = xs.GetAll().Count();

            xs.Delete(id);
            var finalCount = xs.GetAll().Count();

            Assert.AreEqual(initialCount - 1, finalCount);
        }

        [TestMethod()]
        public void ExpertiseXMLService_Update_Works() 
        {
            var id = xs.Insert(new ExpertiseEntity());

            var newOne = new ExpertiseEntity { Category = "cat", Expertise = "exp" };
            xs.Update(id, newOne);

            var reload = xs.Get(id);
            Assert.AreEqual("cat", reload.Category);
            Assert.AreEqual("exp", reload.Expertise);
        }
    }
}