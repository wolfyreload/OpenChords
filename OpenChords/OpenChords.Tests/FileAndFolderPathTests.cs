using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Tests
{
    [TestClass]
    public class FileAndFolderPathTests
    {
        [TestMethod]
        public void TestPathsWithRelativeandAbsoluteFolders()
        {
            var settings = new FileAndFolderSettings();
            settings.ApplicationDataFolder = @"c:\OpenChords";
            settings.OpenSongSetsAndSongs = "../OpenSong";
            Settings.setup(settings);
            Assert.AreEqual(Settings.GlobalApplicationSettings.SongsFolder, @"c:\OpenChords\Songs\");
            Assert.AreEqual(Settings.GlobalApplicationSettings.SetsFolder, @"c:\OpenChords\Sets\");
            Assert.AreEqual(Settings.GlobalApplicationSettings.MediaFolder, @"c:\OpenChords\Media\");
            Assert.AreEqual(Settings.GlobalApplicationSettings.PrintFolder, @"c:\OpenChords\Export\Print\");
            Assert.AreEqual(Settings.GlobalApplicationSettings.TabletFolder, @"c:\OpenChords\Export\Tablet\");
            var applicationDirectory = settings.OpenChordsApplicationDirectory;
            string expectedOpenSong_SongPath = Path.GetFullPath(Path.Combine(applicationDirectory, settings.OpenSongSetsAndSongs + "/Songs/OpenChords/"));
            string expectedOpenSong_BackgroundsPath = Path.GetFullPath(Path.Combine(applicationDirectory, settings.OpenSongSetsAndSongs + "/Backgrounds/"));
            string expectedOpenSong_Sets = Path.GetFullPath(Path.Combine(applicationDirectory, settings.OpenSongSetsAndSongs + "/Sets/"));

            Assert.AreEqual(Settings.GlobalApplicationSettings.OpensongSongsFolder, expectedOpenSong_SongPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.OpenSongSetFolder, expectedOpenSong_Sets);
            Assert.AreEqual(Settings.GlobalApplicationSettings.OpensongBackgroundsFolder, expectedOpenSong_BackgroundsPath);
        }

        private string getFullPath(FileAndFolderSettings settings, string part1, string part2)
        {
            var applicationDirectory = settings.OpenChordsApplicationDirectory;
            string path = Path.GetFullPath(Path.Combine(applicationDirectory, part1 + part2));
            return path;


        }

        [TestMethod]
        public void TestPathRelativeFolders()
        {
            var settings = new FileAndFolderSettings();
            settings.ApplicationDataFolder = @"../Data";
            settings.OpenSongSetsAndSongs = "c:/OpenSong";
            Settings.setup(settings);
            string expectedSongPath = getFullPath(settings, "", "../Data/Songs/");
            string expectedSetsPath = getFullPath(settings, "", "../Data/Sets/");
            string expectedMediaPath = getFullPath(settings, "", "../Data/Media/");
            string expectedPrintPath = getFullPath(settings, "", "../Data/Export/Print/");
            string expectedTabletPath = getFullPath(settings, "", "../Data/Export/Tablet/");
            string expectedOpenSong_BackgroundsPath = getFullPath(settings, settings.OpenSongSetsAndSongs, "/Backgrounds/");
            string expectedOpenSong_Sets = getFullPath(settings, settings.OpenSongSetsAndSongs, "/Sets/");
            string expectedOpenSong_SongPath = getFullPath(settings, settings.OpenSongSetsAndSongs, "/Songs/OpenChords/");

            Assert.AreEqual(Settings.GlobalApplicationSettings.SongsFolder, expectedSongPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.SetsFolder, expectedSetsPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.MediaFolder, expectedMediaPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.PrintFolder, expectedPrintPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.TabletFolder, expectedTabletPath);

            Assert.AreEqual(Settings.GlobalApplicationSettings.OpensongSongsFolder, expectedOpenSong_SongPath);
            Assert.AreEqual(Settings.GlobalApplicationSettings.OpenSongSetFolder, expectedOpenSong_Sets);
            Assert.AreEqual(Settings.GlobalApplicationSettings.OpensongBackgroundsFolder, expectedOpenSong_BackgroundsPath);
        }
    }
}
