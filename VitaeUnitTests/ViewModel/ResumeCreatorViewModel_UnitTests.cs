using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Vitae.ViewModel;
using System.Linq;
using Vitae;
using Moq;
using Vitae.Services;
using System;
using Vitae.Model;
using System.Collections.Generic;

namespace VitaeUnitTests
{
    [TestClass]
    public class ResumeCreatorViewModel_UnitTests
    {
        Mock<IResumeCreationService> mockRcs = new Mock<IResumeCreationService>();
        Mock<ILoggingService> mockLog = new Mock<ILoggingService>();
        Mock<IGeneralInfoRepository> mockGiRepos = new Mock<IGeneralInfoRepository>();
        Mock<IExpertiseRepository> mockExpertiseRepos = new Mock<IExpertiseRepository>();
        Mock<IExperienceRepository> mockExperienceRepos = new Mock<IExperienceRepository>();
        Mock<IEducationRepository> mockEdRepos = new Mock<IEducationRepository>();
        Mock<IPublicationsRepository> mockPubRepos = new Mock<IPublicationsRepository>();

        [TestMethod]
        public void ResumeCreatorViewModel_Constructor_Works() 
        {
            var gie = new GeneralInfoEntity();
            gie.FullName = "fn";
            gie.Add1 = "add1";
            gie.Add2 = "add2";
            gie.Phone = "phone";
            gie.Email = "email";
            mockGiRepos.Setup(T => T.Get(Guid.Empty)).Returns(gie);

            var expertiseList = new List<IExpertiseEntity>();
            for (int i = 0; i < 73; i++) expertiseList.Add(new ExpertiseEntity { Category = "c", Expertise = "e" });
            mockExpertiseRepos.Setup(T => T.GetAll()).Returns(expertiseList);

            var experienceList = new List<IExperienceEntity>();
            for (int i = 0; i < 45; i++) experienceList.Add(new ExperienceEntity { Employer = "emp" });
            mockExperienceRepos.Setup(T => T.GetAll()).Returns(experienceList);

            var experienceItemList = new List<IExperienceItem>();
            for (int i = 0; i < 108; i++) experienceItemList.Add(new ExperienceItem { ExperienceDetail = "d" });
            mockExperienceRepos.Setup(T => T.GetAllExperienceItems()).Returns(experienceItemList);

            var educationList = new List<IEducationEntity>();
            for (int i = 0; i < 8; i++) educationList.Add(new EducationEntity { Credential = "cred", Institution = "inst" });
            mockEdRepos.Setup(T => T.GetAll()).Returns(educationList);

            var pubList = new List<IPublicationEntity>();
            for (int i = 0; i < 33; i++) pubList.Add(new PublicationEntity { Publication = "pub" });
            mockPubRepos.Setup(T => T.GetAll()).Returns(pubList);

            var vm = new ResumeCreatorViewModel(
                mockRcs.Object, 
                mockLog.Object, 
                Guid.Empty, 
                mockGiRepos.Object, 
                mockExperienceRepos.Object, 
                mockExpertiseRepos.Object, 
                mockEdRepos.Object, 
                mockPubRepos.Object);

            Assert.AreEqual("fn", vm.FullName);
            Assert.AreEqual("add1", vm.AddLine1);
            Assert.AreEqual("add2", vm.AddLine2);
            Assert.AreEqual("phone", vm.Phone);
            Assert.AreEqual("email", vm.Email);
            Assert.AreEqual(string.Empty, vm.TagLine);

            Assert.AreEqual(73, vm.AllExpertises.Count);
            Assert.AreEqual(0, vm.InExpertises.Count);
            Assert.IsTrue(vm.OutExpertises.Count > 0);
            Assert.IsNull(vm.SelectedOutExpertise);
            Assert.IsNull(vm.SelectedInExpertise);

            Assert.AreEqual(1, vm.AllEmployers.Count);
            Assert.IsNull(vm.SelectedEmployer);

            Assert.AreEqual(108, vm.AllExperiences.Count);
            Assert.AreEqual(0, vm.AllInExperiences.Count);
            Assert.IsNull(vm.SelectedInExperience);
            Assert.IsNull(vm.SelectedOutExperience);

            Assert.AreEqual(8, vm.AllEducations.Count);
            Assert.AreEqual(0, vm.InEducations.Count);
            Assert.IsTrue(vm.OutEducations.Count > 0);
            Assert.IsNull(vm.SelectedOutEducation);
            Assert.IsNull(vm.SelectedInEducation);

            Assert.AreEqual(33, vm.AllPublications.Count);
            Assert.AreEqual(0, vm.InPublications.Count);
            Assert.IsTrue(vm.OutPublications.Count > 0);
            Assert.IsNull(vm.SelectedOutPublication);
            Assert.IsNull(vm.SelectedInPublication);

            Assert.IsNotNull(vm.AllJTSOs);
            Assert.AreEqual(45, vm.AllJTSOs.Count);

            Assert.IsNotNull(vm.ResumePreview.Blocks);
        }

        [TestMethod]
        public void ResumeCreatorViewModel_AddExpertiseCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                var expertiseItem1 = vm.OutExpertises.FirstOrDefault();
                Assert.IsFalse(vm.AddExpertiseCommand.CanExecute(null));
                vm.SelectedOutExpertise = expertiseItem1;
                Assert.IsTrue(vm.AddExpertiseCommand.CanExecute(null));
                vm.AddExpertiseCommand.Execute(null);

                Assert.IsFalse(vm.OutExpertises.Contains(expertiseItem1));
                Assert.IsTrue(vm.InExpertises.Contains(expertiseItem1));
                Assert.AreEqual(0, vm.InExpertises.IndexOf(expertiseItem1));
                Assert.IsNull(vm.SelectedOutExpertise);
                Assert.IsNull(vm.SelectedInExpertise);

                var expertiseItem2 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem2;
                vm.AddExpertiseCommand.Execute(null);

                Assert.AreNotEqual(expertiseItem1.ToString(), expertiseItem2.ToString());
                Assert.IsTrue(vm.InExpertises.Contains(expertiseItem2));
                Assert.AreEqual(1, vm.InExpertises.IndexOf(expertiseItem2));
                Assert.IsNull(vm.SelectedOutExpertise);
                Assert.IsNull(vm.SelectedInExpertise);
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_RemoveExpertiseCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                var expertiseItem1 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem1;
                vm.AddExpertiseCommand.Execute(null);

                var expertiseItem2 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem2;
                vm.AddExpertiseCommand.Execute(null);

                Assert.IsFalse(vm.RemoveExpertiseCommand.CanExecute(null));
                vm.SelectedInExpertise = vm.InExpertises.FirstOrDefault();
                Assert.IsTrue(vm.RemoveExpertiseCommand.CanExecute(null));
                vm.RemoveExpertiseCommand.Execute(null);

                Assert.IsTrue(vm.OutExpertises.Contains(expertiseItem1));
                Assert.IsFalse(vm.InExpertises.Contains(expertiseItem1));
                Assert.IsNull(vm.SelectedOutExpertise);
                Assert.IsNull(vm.SelectedInExpertise);
                Assert.AreEqual(0, vm.InExpertises.IndexOf(expertiseItem2));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveExpertiseUpCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                var expertiseItem1 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem1;
                vm.AddExpertiseCommand.Execute(null);

                var expertiseItem2 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem2;
                vm.AddExpertiseCommand.Execute(null);

                var expertiseItem3 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem3;
                vm.AddExpertiseCommand.Execute(null);

                vm.SelectedInExpertise = null;
                Assert.IsFalse(vm.MoveExpertiseUpCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem1;
                Assert.IsFalse(vm.MoveExpertiseUpCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem2;
                Assert.IsTrue(vm.MoveExpertiseUpCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem3;
                Assert.IsTrue(vm.MoveExpertiseUpCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem2;
                vm.MoveExpertiseUpCommand.Execute(null);
                Assert.AreEqual(0, vm.InExpertises.IndexOf(expertiseItem2));
                Assert.AreEqual(1, vm.InExpertises.IndexOf(expertiseItem1));
                Assert.AreEqual(2, vm.InExpertises.IndexOf(expertiseItem3));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveExpertiseDownCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                var expertiseItem1 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem1;
                vm.AddExpertiseCommand.Execute(null);

                var expertiseItem2 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem2;
                vm.AddExpertiseCommand.Execute(null);

                var expertiseItem3 = vm.OutExpertises.FirstOrDefault();
                vm.SelectedOutExpertise = expertiseItem3;
                vm.AddExpertiseCommand.Execute(null);

                vm.SelectedInExpertise = null;
                Assert.IsFalse(vm.MoveExpertiseDownCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem1;
                Assert.IsTrue(vm.MoveExpertiseDownCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem2;
                Assert.IsTrue(vm.MoveExpertiseDownCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem3;
                Assert.IsFalse(vm.MoveExpertiseDownCommand.CanExecute(null));

                vm.SelectedInExpertise = expertiseItem2;
                vm.MoveExpertiseDownCommand.Execute(null);
                Assert.AreEqual(0, vm.InExpertises.IndexOf(expertiseItem1));
                Assert.AreEqual(1, vm.InExpertises.IndexOf(expertiseItem3));
                Assert.AreEqual(2, vm.InExpertises.IndexOf(expertiseItem2));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_UpdateExperienceLists_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                vm.SelectedEmployer = "Morgan Stanley";
                vm.UpdateExperienceLists();

                Assert.IsTrue(vm.OutExperiences.All(T => T.Employer == "Morgan Stanley"));

                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);

                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();

                Assert.IsTrue(vm.OutExperiences.All(T => T.Employer == "Citigroup"));

                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);

                vm.SelectedEmployer = "Morgan Stanley";
                vm.UpdateExperienceLists();

                Assert.IsTrue(vm.InExperiences.All(T => T.Employer == "Morgan Stanley"));

                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();

                Assert.IsTrue(vm.InExperiences.All(T => T.Employer == "Citigroup"));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_AddExperienceCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();

                vm.SelectedOutExperience = null;
                Assert.IsFalse(vm.AddExperienceCommand.CanExecute(null));

                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                Assert.IsTrue(vm.AddExperienceCommand.CanExecute(null));

                vm.AddExperienceCommand.Execute(null);
                Assert.AreEqual(1, vm.InExperiences.Count);
                Assert.AreEqual(1, vm.AllInExperiences.Count);
                Assert.IsTrue(vm.InExperiences.Contains(vm.SelectedOutExperience));
                Assert.IsTrue(vm.AllInExperiences.Contains(vm.SelectedOutExperience));

                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                Assert.AreEqual(2, vm.InExperiences.Count);
                Assert.AreEqual(2, vm.AllInExperiences.Count);
                Assert.IsTrue(vm.InExperiences.Contains(vm.SelectedOutExperience));
                Assert.IsTrue(vm.AllInExperiences.Contains(vm.SelectedOutExperience));
                Assert.AreEqual(1, vm.InExperiences.IndexOf(vm.SelectedOutExperience));
                Assert.AreEqual(1, vm.AllInExperiences.IndexOf(vm.SelectedOutExperience));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_RemoveExperienceCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();
                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);

                vm.SelectedInExperience = null;
                Assert.IsFalse(vm.RemoveExperienceCommand.CanExecute(null));

                vm.SelectedInExperience = vm.InExperiences.FirstOrDefault();
                Assert.IsTrue(vm.RemoveExperienceCommand.CanExecute(null));

                vm.RemoveExperienceCommand.Execute(null);
                Assert.AreEqual(1, vm.InExperiences.Count);
                Assert.AreEqual(1, vm.AllInExperiences.Count);

                vm.SelectedInExperience = vm.InExperiences.FirstOrDefault();
                vm.RemoveExperienceCommand.Execute(null);
                Assert.AreEqual(0, vm.InExperiences.Count);
                Assert.AreEqual(0, vm.AllInExperiences.Count);
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveExperienceUpCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();
                var item1 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                var item2 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                var item3 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);

                vm.SelectedInExperience = null;
                Assert.IsFalse(vm.MoveExperienceUpCommand.CanExecute(null));

                vm.SelectedInExperience = item1;
                Assert.IsFalse(vm.MoveExperienceUpCommand.CanExecute(null));

                vm.SelectedInExperience = item2;
                Assert.IsTrue(vm.MoveExperienceUpCommand.CanExecute(null));

                vm.SelectedInExperience = item3;
                Assert.IsTrue(vm.MoveExperienceUpCommand.CanExecute(null));

                vm.SelectedInExperience = item2;
                vm.MoveExperienceUpCommand.Execute(null);
                Assert.AreEqual(0, vm.InExperiences.IndexOf(item2));
                Assert.AreEqual(1, vm.InExperiences.IndexOf(item1));
                Assert.AreEqual(2, vm.InExperiences.IndexOf(item3));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveExperienceDownCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                vm.SelectedEmployer = "Citigroup";
                vm.UpdateExperienceLists();
                var item1 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                var item2 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);
                var item3 = vm.SelectedOutExperience = vm.OutExperiences.FirstOrDefault();
                vm.AddExperienceCommand.Execute(null);

                vm.SelectedInExperience = null;
                Assert.IsFalse(vm.MoveExperienceDownCommand.CanExecute(null));

                vm.SelectedInExperience = item1;
                Assert.IsTrue(vm.MoveExperienceDownCommand.CanExecute(null));

                vm.SelectedInExperience = item2;
                Assert.IsTrue(vm.MoveExperienceDownCommand.CanExecute(null));

                vm.SelectedInExperience = item3;
                Assert.IsFalse(vm.MoveExperienceDownCommand.CanExecute(null));

                vm.SelectedInExperience = item2;
                vm.MoveExperienceDownCommand.Execute(null);
                Assert.AreEqual(0, vm.InExperiences.IndexOf(item1));
                Assert.AreEqual(1, vm.InExperiences.IndexOf(item3));
                Assert.AreEqual(2, vm.InExperiences.IndexOf(item2));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_AddEducationCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();

                vm.SelectedOutEducation = null;
                Assert.IsFalse(vm.AddEducationCommand.CanExecute(null));

                vm.SelectedOutEducation = item;
                Assert.IsTrue(vm.AddEducationCommand.CanExecute(null));

                vm.AddEducationCommand.Execute(null);
                Assert.IsTrue(vm.InEducations.Contains(vm.SelectedOutEducation));
                Assert.AreEqual(1, vm.InEducations.Count);

                vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);
                Assert.IsTrue(vm.InEducations.Contains(vm.SelectedOutEducation));
                Assert.AreEqual(2, vm.InEducations.Count);
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_RemoveEducationCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);
                var item2 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);

                vm.SelectedOutEducation = null;
                Assert.IsFalse(vm.RemoveEducationCommand.CanExecute(null));

                vm.SelectedInEducation = item1;
                vm.RemoveEducationCommand.Execute(null);
                Assert.IsFalse(vm.InEducations.Contains(item1));
                Assert.IsTrue(vm.InEducations.Contains(item2));
                Assert.AreEqual(1, vm.InEducations.Count);
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveEducationUpCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);
                var item2 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);

                vm.SelectedInEducation = null;
                Assert.IsFalse(vm.MoveEducationUpCommand.CanExecute(null));

                vm.SelectedInEducation = item1;
                Assert.IsFalse(vm.MoveEducationUpCommand.CanExecute(null));

                vm.SelectedInEducation = item2;
                Assert.IsTrue(vm.MoveEducationUpCommand.CanExecute(null));

                vm.SelectedInEducation = item2;
                vm.MoveEducationUpCommand.Execute(null);
                Assert.AreEqual(0, vm.InEducations.IndexOf(item2));
                Assert.AreEqual(1, vm.InEducations.IndexOf(item1));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MoveEducationDownCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);
                var item2 = vm.SelectedOutEducation = vm.OutEducations.FirstOrDefault();
                vm.AddEducationCommand.Execute(null);

                vm.SelectedInEducation = null;
                Assert.IsFalse(vm.MoveEducationDownCommand.CanExecute(null));

                vm.SelectedInEducation = item1;
                Assert.IsTrue(vm.MoveEducationDownCommand.CanExecute(null));

                vm.SelectedInEducation = item2;
                Assert.IsFalse(vm.MoveEducationDownCommand.CanExecute(null));

                vm.SelectedInEducation = item1;
                vm.MoveEducationDownCommand.Execute(null);
                Assert.AreEqual(0, vm.InEducations.IndexOf(item2));
                Assert.AreEqual(1, vm.InEducations.IndexOf(item1));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_AddPublicationCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();

                vm.SelectedOutPublication = null;
                Assert.IsFalse(vm.AddPublicationCommand.CanExecute(null));

                vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                Assert.IsTrue(vm.AddPublicationCommand.CanExecute(null));

                vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                Assert.IsTrue(vm.InPublications.Contains(vm.SelectedOutPublication));
                Assert.IsFalse(vm.OutPublications.Contains(vm.SelectedOutPublication));

                vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                Assert.AreEqual(2, vm.InPublications.Count);
                Assert.AreEqual(1, vm.InPublications.IndexOf(vm.SelectedOutPublication));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_RemovePublicationCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item2 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item3 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);

                vm.SelectedInPublication = null;
                Assert.IsFalse(vm.RemovePublicationCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                Assert.IsTrue(vm.RemovePublicationCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                vm.RemovePublicationCommand.Execute(null);
                Assert.IsFalse(vm.InPublications.Contains(item2));
                Assert.AreEqual(2, vm.InPublications.Count);
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MovePublicationUpCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item2 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item3 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);

                vm.SelectedInPublication = null;
                Assert.IsFalse(vm.MovePublicationUpCommand.CanExecute(null));

                vm.SelectedInPublication = item1;
                Assert.IsFalse(vm.MovePublicationUpCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                Assert.IsTrue(vm.MovePublicationUpCommand.CanExecute(null));

                vm.SelectedInPublication = item3;
                Assert.IsTrue(vm.MovePublicationUpCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                vm.MovePublicationUpCommand.Execute(null);
                Assert.AreEqual(0, vm.InPublications.IndexOf(item2));
                Assert.AreEqual(1, vm.InPublications.IndexOf(item1));
                Assert.AreEqual(2, vm.InPublications.IndexOf(item3));
            }
        }

        [TestMethod]
        public void ResumeCreatorViewModel_MovePublicationDownCommand_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var vm = ioc.Get<IResumeCreatorViewModel>();
                var item1 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item2 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);
                var item3 = vm.SelectedOutPublication = vm.OutPublications.FirstOrDefault();
                vm.AddPublicationCommand.Execute(null);

                vm.SelectedInPublication = null;
                Assert.IsFalse(vm.MovePublicationDownCommand.CanExecute(null));

                vm.SelectedInPublication = item1;
                Assert.IsTrue(vm.MovePublicationDownCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                Assert.IsTrue(vm.MovePublicationDownCommand.CanExecute(null));

                vm.SelectedInPublication = item3;
                Assert.IsFalse(vm.MovePublicationDownCommand.CanExecute(null));

                vm.SelectedInPublication = item2;
                vm.MovePublicationDownCommand.Execute(null);

                Assert.AreEqual(0, vm.InPublications.IndexOf(item1));
                Assert.AreEqual(1, vm.InPublications.IndexOf(item3));
                Assert.AreEqual(2, vm.InPublications.IndexOf(item2));
            }
        }
    }
}