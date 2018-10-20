using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System.Linq;
using Vitae.Model;

namespace Vitae.Services_UnitTests
{
    [TestClass()]
    public class EducationXMLService_UnitTests
    {
        EducationXMLService xs = new EducationXMLService(new VitaeNinjectKernel());

        [TestMethod()]
        public void EducationXMLService_EducationXMLService_Works() 
        {
            Assert.IsNotNull(xs);
            Assert.IsTrue(xs is IEducationXMLService);
            Assert.IsTrue(xs is IXMLService<IEducationEntity>);
        }

        [TestMethod()]
        public void EducationXMLService_Delete_Works() 
        {
            var id = xs.Insert(new EducationEntity
            {
                Credential = "cred",
                Institution = "inst"
            });

            xs.Delete(id);

            Assert.IsNull(xs.Get(id));
        }

        [TestMethod()]
        public void EducationXMLService_Get_Works() 
        {
            var id = xs.Insert(new EducationEntity
            {
                Credential = "cred",
                Institution = "inst"
            });

            var ent = xs.Get(id);

            Assert.IsNotNull(ent);
            Assert.AreEqual("cred", ent.Credential);
            Assert.AreEqual("inst", ent.Institution);
        }

        [TestMethod()]
        public void EducationXMLService_GetAll_Works() 
        {
            var list = xs.GetAll();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Credential)));
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Institution)));
        }

        [TestMethod()]
        public void EducationXMLService_Insert_Works() 
        {
            var id = xs.Insert(new EducationEntity
            {
                Credential = "cred",
                Institution = "inst"
            });

            var ent = xs.Get(id);

            Assert.IsNotNull(ent);
            Assert.AreEqual("cred", ent.Credential);
            Assert.AreEqual("inst", ent.Institution);
        }

        [TestMethod()]
        public void EducationXMLService_Update_Works() 
        {
            var id = xs.Insert(new EducationEntity
            {
                Credential = "cred",
                Institution = "inst"
            });

            var ent = new EducationEntity
            {
                Credential = "uCred",
                Institution = "uInst"
            };

            xs.Update(id, ent);

            var final = xs.Get(id);

            Assert.AreEqual("uCred", final.Credential);
            Assert.AreEqual("uInst", final.Institution);
        }
    }
}