using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenChords.Functions
{
    public class ImportSong
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string importLyrics(string lyrics)
        {
            var lyricsContent = lyrics.Split(new string[] { "\r\n" }, StringSplitOptions.None);
          
            StringBuilder sbLyrics = new StringBuilder();

            var song = new Song();


            for (int i = 0; i < lyricsContent.Count(); i++)
            {
                var line = lyricsContent[i];
                if (line.Contains((char)65533))
                    line = line.Replace((char)65533, "'"[0]);
                if (Regex.IsMatch(line.Trim().ToUpper(), @"\(?PAGE.*")) continue;

                line = regexReplaceFix(line, @"CHORUS\s?(\d*)\s*:?", "[C$1]");
                line = regexReplaceFix(line, @"BRIDGE\s?(\d*)\s*:?", "[B$1]");
                line = regexReplaceFix(line, @"INTRO\s?(\d*)\s*:?", "[I$1]");
                line = regexReplaceFix(line, @"Interlude\s?(\d*)\s*:?", "[I$1]");
                line = regexReplaceFix(line, @"Verse\s?(\d*)\s*:?", @"[V$1]");
                line = regexReplaceFix(line, @"Refrain\s?(\d*)\s*:?", @"[R$1]");
                line = regexReplaceFix(line, @"Ending\s?(\d*)\s*:?", @"[E$1]");
                line = regexReplaceFix(line, @"Outro\s?(\d*)\s*:?", @"[E$1]");
                //line = line.TrimEnd();
                sbLyrics.AppendLine(line);
            }

            //if there dont appear to be any verses try and find them
            if (!Regex.IsMatch(sbLyrics.ToString(), @"\[V\d+\*]"))
            {
                findMissingVerses(sbLyrics);
            }

            return sbLyrics.ToString();
        }

        private static void findMissingVerses(StringBuilder sbLyrics)
        {
            var verseNumber = 1;
            //find some new lines
            var songSections = Regex.Split(sbLyrics.ToString(), @"([\r\n]){4,100}");
            sbLyrics.Length = 0;
            for (int i =0; i < songSections.Length; i++)
            {
                string section = songSections[i];
                int numberOfLines = numberOfSongLines(section);
                //if this is an unknown section assume that its a verse 
                //we assume a verse if it has more than 4 lines and does not have a label
                if (!section.Contains("[") && numberOfLines >= 4)
                {
                    sbLyrics.Append("[V" + verseNumber + "]" + section);
                    verseNumber++;
                }
                else
                {
                    sbLyrics.Append(section);
                }
            }
        }

        private static int numberOfSongLines(string section)
        {
            var matches = Regex.Matches(section, @"[\r\n]");
            return matches.Count;
        }
        
        private static string regexReplaceFix(string line, string pattern, string replacementText)
        {
            if (Regex.IsMatch(line.Trim(), pattern, RegexOptions.IgnoreCase))
            {
                line = Regex.Replace(line, pattern, replacementText, RegexOptions.IgnoreCase);
            }
            return line;

        }

        public static string importPresentationList(string lyrics)
        {
            var allMatches = Regex.Matches(lyrics, @"\[(\w+)\]", RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in allMatches)
            {
                sb.Append(match.Groups[1] + " ");
            }
            sb.Replace("[", "");
            sb.Replace("]", "");
            return sb.ToString();
        }


    }
}
