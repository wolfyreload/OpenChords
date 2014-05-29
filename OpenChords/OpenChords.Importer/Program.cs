using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenChords.Importer
{
    class Program
    {
        static void Main(string[] args)
        {

            var fafs = new FileAndFolderSettings();
            fafs.ApplicationDataFolder = @".\Data";
            Settings.setup(fafs);

            var SongList = OpenChords.IO.FileFolderFunctions.getDirectoryListingAsList(@"input");

            foreach (string songFilename in SongList)
            {
                createBaseSong(songFilename);
            }




        }

        private static Song createBaseSong(string filename)
        {
            var path = "input\\" + filename;
            var fileContent = System.IO.File.ReadAllLines(path);

            var title = fileContent[0];
            var author = fileContent[1];

            StringBuilder lyrics = new StringBuilder();

            var song = new Song();
            

            for (int i=2; i < fileContent.Count(); i++)
            {
                var line = fileContent[i];
                if (line.Contains((char)65533))
                  line = line.Replace((char)65533, "'"[0]);
                if (line.Trim().ToUpper() == title.ToUpper()) continue;
                if (line.Trim().ToUpper() == author.ToUpper()) continue;
                if (Regex.IsMatch(line.Trim().ToUpper(), @"\(?PAGE.*")) continue;

                setCapo(song, ref line);

                lyrics.AppendLine(line);
            };

            song.title = title;
            song.author = author;
            song.lyrics = lyrics.ToString();
            song.notes = "";

            song.fixFormatting();
            song.saveSong();
            
            return song;
        }

        private static void setCapo(Song song, ref string line)
        {
            if (Regex.IsMatch(line.Trim(), @"Capo:?\s*(\d+).*(fret)?", RegexOptions.IgnoreCase))
            {
                var match = Regex.Match(line.Trim(), @"Capo:?\s*(\d+).*(fret)?", RegexOptions.IgnoreCase);
                song.capo = match.Groups[1].Value;
                line = "";
            }
        }
    }
}
