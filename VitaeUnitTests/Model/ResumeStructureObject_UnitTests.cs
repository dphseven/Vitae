using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Model;
using Vitae;
using Ninject;
using Moq;

namespace VitaeUnitTests
{
    [TestClass]
    public class ResumeStructureObject_UnitTests
    {
        [TestMethod]
        public void ResumeStructureObject_Constructor_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var rso = ioc.Get<IResumeStructureObject>();

                Assert.IsNotNull(rso.ResumeSections);
                Assert.AreEqual(0, rso.ResumeSections.Count);
            }
        }

        [TestMethod]
        public void ResumeStructureObject_AddSection_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var rso = ioc.Get<IResumeStructureObject>();

                rso.AddSection(ioc.Get<IFullNameSection>());
                rso.AddSection(ioc.Get<IBasicInfoSection>());
                rso.AddSection(ioc.Get<ITagLineSection>());
                rso.AddSection(ioc.Get<IExpertiseSection>());
                rso.AddSection(ioc.Get<IExperienceSection>());
                rso.AddSection(ioc.Get<IEducationSection>());
                rso.AddSection(ioc.Get<IPublicationsSection>());

                Assert.AreEqual(7, rso.ResumeSections.Count);

                Assert.IsInstanceOfType(rso.ResumeSections[0], typeof(IFullNameSection));
                Assert.IsInstanceOfType(rso.ResumeSections[1], typeof(IBasicInfoSection));
                Assert.IsInstanceOfType(rso.ResumeSections[2], typeof(ITagLineSection));
                Assert.IsInstanceOfType(rso.ResumeSections[3], typeof(IExpertiseSection));
                Assert.IsInstanceOfType(rso.ResumeSections[4], typeof(IExperienceSection));
                Assert.IsInstanceOfType(rso.ResumeSections[5], typeof(IEducationSection));
                Assert.IsInstanceOfType(rso.ResumeSections[6], typeof(IPublicationsSection));

                Assert.IsInstanceOfType(rso.ResumeSections[0], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[1], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[2], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[3], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[4], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[5], typeof(IResumeSection));
                Assert.IsInstanceOfType(rso.ResumeSections[6], typeof(IResumeSection));
            }
        }

        [TestMethod]
        public void ResumeStructureObject_RemoveSection_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var rso = ioc.Get<IResumeStructureObject>();
                rso.AddSection(ioc.Get<IFullNameSection>());
                rso.AddSection(ioc.Get<IBasicInfoSection>());
                rso.AddSection(ioc.Get<ITagLineSection>());
                rso.AddSection(ioc.Get<IExpertiseSection>());
                rso.AddSection(ioc.Get<IExperienceSection>());
                rso.AddSection(ioc.Get<IEducationSection>());
                rso.AddSection(ioc.Get<IPublicationsSection>());

                rso.RemoveSection(0);

                Assert.IsInstanceOfType(rso.ResumeSections[0], typeof(IBasicInfoSection));
                Assert.IsInstanceOfType(rso.ResumeSections[1], typeof(ITagLineSection));
                Assert.IsInstanceOfType(rso.ResumeSections[2], typeof(IExpertiseSection));
                Assert.IsInstanceOfType(rso.ResumeSections[3], typeof(IExperienceSection));
                Assert.IsInstanceOfType(rso.ResumeSections[4], typeof(IEducationSection));
                Assert.IsInstanceOfType(rso.ResumeSections[5], typeof(IPublicationsSection));

                Assert.AreEqual(6, rso.ResumeSections.Count);

                rso.RemoveSection(3);

                Assert.IsInstanceOfType(rso.ResumeSections[0], typeof(IBasicInfoSection));
                Assert.IsInstanceOfType(rso.ResumeSections[1], typeof(ITagLineSection));
                Assert.IsInstanceOfType(rso.ResumeSections[2], typeof(IExpertiseSection));
                Assert.IsInstanceOfType(rso.ResumeSections[3], typeof(IEducationSection));
                Assert.IsInstanceOfType(rso.ResumeSections[4], typeof(IPublicationsSection));

                Assert.AreEqual(5, rso.ResumeSections.Count);
            }


        }

        [TestMethod]
        public void ResumeStructureObject_Move_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                ResumeStructureObject rso = new ResumeStructureObject();
                rso.AddSection(ioc.Get<IFullNameSection>());
                rso.AddSection(ioc.Get<IBasicInfoSection>());
                rso.AddSection(ioc.Get<ITagLineSection>());
                rso.AddSection(ioc.Get<IExpertiseSection>());
                rso.AddSection(ioc.Get<IExperienceSection>());
                rso.AddSection(ioc.Get<IEducationSection>());
                rso.AddSection(ioc.Get<IPublicationsSection>());

                rso.MoveSection(2, 1);

                Assert.IsInstanceOfType(rso.ResumeSections[0], typeof(IFullNameSection));
                Assert.IsInstanceOfType(rso.ResumeSections[2], typeof(IBasicInfoSection));
                Assert.IsInstanceOfType(rso.ResumeSections[1], typeof(ITagLineSection));
                Assert.IsInstanceOfType(rso.ResumeSections[3], typeof(IExpertiseSection));
                Assert.IsInstanceOfType(rso.ResumeSections[4], typeof(IExperienceSection));
                Assert.IsInstanceOfType(rso.ResumeSections[5], typeof(IEducationSection));
                Assert.IsInstanceOfType(rso.ResumeSections[6], typeof(IPublicationsSection));
            }
        }

    }
}
