using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenChords.Entities;
using OpenChords.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Tests
{
    [TestClass]
    public class ImporterTests
    {
        [TestMethod]
        public void TestImport1()
        {
            string input = TestResources.Import1Input;
            var import1Song = new Song() { lyrics = input };
            import1Song.fixFormatting();
            var imported1Result = import1Song.lyrics;
            string import1ExpectedResult = TestResources.Import1Output;
            int expectedVersesCount = import1ExpectedResult.Count(c => c == '[');
            int actualVersesCount = imported1Result.Count(c => c == '[');
            Assert.AreEqual(expectedVersesCount, actualVersesCount);
        }

        [TestMethod]
        public void TestImport2()
        {
            string input = TestResources.Import2Input;
            var import2Song = new Song() { lyrics = input };
            import2Song.fixFormatting();
            var imported2Result = import2Song.lyrics;
            string import2ExpectedResult = TestResources.Import2Output;
            int expectedVersesCount = import2ExpectedResult.Count(c => c == '[');
            int actualVersesCount = imported2Result.Count(c => c == '[');
            Assert.AreEqual(expectedVersesCount, actualVersesCount);
            Assert.AreEqual("V1 V2 V3 V4 C", import2Song.presentation.Trim());
        }

        [TestMethod]
        public void TestImportSongOrder()
        {
            string input = TestResources.Import3Input;
            var import3Song = new Song() { lyrics = input };
            import3Song.fixFormatting();
            Assert.AreEqual("V1 C V2 C V3 C V4 C", import3Song.presentation.Trim());
        }



    }
}
