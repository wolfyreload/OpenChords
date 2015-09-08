using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenChords.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChords.Tests
{
    [TestClass]
    public class ChordDetectionTests
    {
        [TestMethod]
        public void TestChordDetection()
        {
            Assert.IsTrue(SongProcessor.CheckIfChordsLine("D/F"));
            Assert.IsTrue(SongProcessor.CheckIfChordsLine(" A/G  D/F#  Dm7/F"));
            Assert.IsFalse(SongProcessor.CheckIfChordsLine("A New Commandment"));
        }

    }
}
