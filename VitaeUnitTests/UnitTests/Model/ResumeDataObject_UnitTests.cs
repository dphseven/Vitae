using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Vitae;
using Vitae.Model;

namespace VitaeUnitTests
{
    [TestClass]
    public class ResumeDataObject_UnitTests
    {
        [TestMethod]
        public void ResumeDataObject_Constructor_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                IResumeDataObject rdo = ioc.Get<IResumeDataObject>();

                Assert.AreEqual(string.Empty, rdo.FullName);
                Assert.AreEqual(string.Empty, rdo.AddressLine1);
                Assert.AreEqual(string.Empty, rdo.AddressLine2);
                Assert.AreEqual(string.Empty, rdo.Email);
                Assert.AreEqual(string.Empty, rdo.PhoneNumber);
                Assert.AreEqual(string.Empty, rdo.TagLine);

                Assert.IsNotNull(rdo.EducationEntities);
                Assert.IsNotNull(rdo.ExperienceEntities);
                Assert.IsNotNull(rdo.ExpertiseEntities);
                Assert.IsNotNull(rdo.PublicationEntities);
            }

        }
    }
}
