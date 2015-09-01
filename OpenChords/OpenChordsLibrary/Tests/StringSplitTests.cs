using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenChords.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Tests
{
    [TestClass]
    public class StringSplitTests
    {
        [TestMethod]
        public void TestStringSplitOnVerseLine()
        {
            string lyrics =
@"[V]
 Hello World".Replace("\r\n", "\n");
            string expectedOutputLyrics = lyrics;
            int carotPosition = 1;
            int carrotPositionAfter = SongProcessor.BreakSongLine(ref lyrics, carotPosition);
            Assert.AreEqual(lyrics, expectedOutputLyrics);
            Assert.AreEqual(carotPosition, carrotPositionAfter);
        }

        [TestMethod]
        public void TestStringSplitOnLyricLine()
        {
            string lyrics =
@"[V]
 Hello World".Replace("\r\n", "\n");
            string expectedOutputLyrics =
@"[V]
 Hello 
 World".Replace("\r\n", "\n");
            int carotPosition = 11;
            int carrotPositionAfter = SongProcessor.BreakSongLine(ref lyrics, carotPosition);
            StringAssert.StartsWith(lyrics, expectedOutputLyrics);
            Assert.AreEqual(13, carrotPositionAfter);
        }



        [TestMethod]
        public void TestStringSplitOnLyricWithChordsLine()
        {
            string lyrics =
@"[V]
.       A
 Hello World".Replace("\r\n", "\n");
            string expectedOutputLyrics =
@"[V]
.      
 Hello 
. A
 World".Replace("\r\n", "\n");
            int carotPosition = 21;
            int carrotPositionAfter = SongProcessor.BreakSongLine(ref lyrics, carotPosition);
            StringAssert.StartsWith(lyrics, expectedOutputLyrics);
            Assert.AreEqual(25, carrotPositionAfter);
        }

    }
}