using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Model;
using Microsoft.Office.Interop.Word;
using Vitae;
using Ninject;

namespace VitaeUnitTests
{
    [TestClass]
    public class ResumeFormatObject_UnitTests
    {
        [TestMethod]
        public void ResumeFormatObject_DefaultValuesAreCorrect() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var rfo = ioc.Get<IResumeFormatObject>();

                Assert.AreEqual((WdColor)14911564, rfo.NameColorWord);
                Assert.AreEqual("Segoe UI Light", rfo.NameFontName);
                Assert.AreEqual(24, rfo.NameFontSize);
                Assert.AreEqual(WdColor.wdColorBlack, rfo.TagLineColorWord);
                Assert.AreEqual("Segoe UI Light", rfo.TagLineFontName);
                Assert.AreEqual(16, rfo.TagLineFontSize);
                Assert.AreEqual((WdColor)14911564, rfo.HeaderColorWord);
                Assert.AreEqual("Segoe UI Light", rfo.HeaderFontName);
                Assert.AreEqual(16, rfo.HeaderFontSize);
                Assert.AreEqual(WdColor.wdColorBlack, rfo.JobInfoColorWord);
                Assert.AreEqual("Segoe UI Semibold", rfo.JobInfoFontName);
                Assert.AreEqual(12, rfo.JobInfoFontSize);
                Assert.AreEqual(WdColor.wdColorBlack, rfo.CategoryColorWord);
                Assert.AreEqual("Segoe UI Semibold", rfo.CategoryFontName);
                Assert.AreEqual(11, rfo.CategoryFontSize);
                Assert.AreEqual("Segoe UI", rfo.BodyFontName);
                Assert.AreEqual(11, rfo.BodyFontSize);
            }

        }
    }
}