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

        [TestMethod]
        public void TestEnglishNotesTranspose()
        {
            Settings.initialize();
            Entities.Song song = new Entities.Song()
            {
                lyrics =
                @"[V]
.A
.A#
.B
.C
.C#
.D
.D#
.E
.F
.F#
.G
.G#",
                presentation = "V"
            };
            SongProcessor.transposeKeyUp(song);
            string expectedOutput =
            @"[V]
.A#
.B
.C
.C#
.D
.D#
.E
.F
.F#
.G
.G#
.A";
            expectedOutput = expectedOutput.Replace("\r\n", "\n");
            StringAssert.Equals(song.lyrics, expectedOutput);
        }

        [TestMethod]
        public void TestGermanNotesTranspose()
        {

            Settings.initialize();
            Settings.GlobalApplicationSettings.KeyNotationLanguage = Entities.FileAndFolderSettings.KeyNotationLanguageType.German;
            Entities.Song song = new Entities.Song()
            {
                lyrics =
                @"[V]
.A
.B
.H
.C
.C#
.D
.D#
.E
.F
.F#
.G
.G#",
                presentation = "V"
            };

            SongProcessor.transposeKeyUp(song);
            string expectedOutput =
            @"[V]
.B
.H
.C
.C#
.D
.D#
.E
.F
.F#
.G
.G#
.A";
            expectedOutput = expectedOutput.Replace("\r\n", "\n");
            StringAssert.Equals(song.lyrics, expectedOutput);
        }
    }


}