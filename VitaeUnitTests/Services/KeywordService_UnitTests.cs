using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Services;
using System.Collections.Generic;
using Vitae;
using Ninject;

namespace VitaeUnitTests
{
    [TestClass]
    public class KeywordService_UnitTests
    {
        [TestMethod]
        public void KeywordService_GetKeywords_Works() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ks = ioc.Get<IKeywordService>();
                string bigText = "A be Cat DATE exist Father GUESSER hotelier Ionizerss ";

                var minOneList = ks.GetKeywords(bigText, 1) as List<string>;
                Assert.AreEqual(9, minOneList.Count);

                var minFiveList = ks.GetKeywords(bigText, 5) as List<string>;
                Assert.AreEqual(5, minFiveList.Count);

                var minTenList = ks.GetKeywords(bigText, 10) as List<string>;
                Assert.AreEqual(0, minTenList.Count);
            }

        }
    }
}