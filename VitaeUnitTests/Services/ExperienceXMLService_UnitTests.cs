using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitae.Model;

namespace Vitae.Services_UnitTests
{
    [TestClass()]
    public class ExperienceXMLService_UnitTests
    {
        ExperienceXMLService xs = new ExperienceXMLService(new VitaeNinjectKernel());

        [TestMethod()]
        public void ExperienceXMLService_ExperienceXMLService_Works() 
        {
            Assert.IsTrue(xs is IExperienceXMLService);
            Assert.IsTrue(xs is IXMLService<IExperienceEntity>);
        }

        [TestMethod()]
        public void ExperienceXMLService_Delete_Works() 
        {
            var id = xs.Insert(new ExperienceEntity());

            xs.Delete(id);

            Assert.IsNull(xs.Get(id));
        }

        [TestMethod()]
        public void ExperienceXMLService_Get_Works() 
        {
            var id = xs.Insert(new ExperienceEntity
            {
                Employer = "someEmp",
                StartDate = "Monday",
                EndDate = "Tuesday",
                Titles = new List<string> { "t0", "t1", "t2" },
                Details = new List<string> { "d0", "d1", "d2" }
            });

            var ent = xs.Get(id);
            Assert.AreEqual("someEmp", ent.Employer);
            Assert.AreEqual("Monday", ent.StartDate);
            Assert.AreEqual("Tuesday", ent.EndDate);
            Assert.AreEqual("t1", ent.Titles[1]);
            Assert.AreEqual("d1", ent.Details[1]);
        }

        [TestMethod()]
        public void ExperienceXMLService_GetAll_Works() 
        {
            var list = xs.GetAll();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Any(T => !string.IsNullOrWhiteSpace(T.Employer)));
        }

        [TestMethod()]
        public void ExperienceXMLService_Insert_Works() 
        {
            var id = xs.Insert(new ExperienceEntity
            {
                Employer = "someEmp",
                StartDate = "Monday",
                EndDate = "Tuesday",
                Titles = new List<string> { "t0", "t1", "t2" },
                Details = new List<string> { "d0", "d1", "d2" }
            });

            var ent = xs.Get(id);
            Assert.AreEqual("someEmp", ent.Employer);
            Assert.AreEqual("Monday", ent.StartDate);
            Assert.AreEqual("Tuesday", ent.EndDate);
            Assert.AreEqual("t1", ent.Titles[1]);
            Assert.AreEqual("d1", ent.Details[1]);
        }

        [TestMethod()]
        public void ExperienceXMLService_Update_Works() 
        {
            var id = xs.Insert(new ExperienceEntity
            {
                Employer = "someEmp",
                StartDate = "Monday",
                EndDate = "Tuesday",
                Titles = new List<string> { "t0", "t1", "t2" },
                Details = new List<string> { "d0", "d1", "d2" }
            });

            var newEnt = new ExperienceEntity
            {
                Employer = "updEmp",
                StartDate = "Wednesday",
                EndDate = "Thursday",
                Titles = new List<string> { "t3", "t4", "t5" },
                Details = new List<string> { "d3", "d4", "d5" }
            };

            xs.Update(id, newEnt);

            var final = xs.Get(id);

            Assert.AreEqual("updEmp", final.Employer);
            Assert.AreEqual("Wednesday", final.StartDate);
            Assert.AreEqual("Thursday", final.EndDate);
            Assert.AreEqual("t4", final.Titles[1]);
            Assert.AreEqual("d5", final.Details[2]);
        }
    }
}