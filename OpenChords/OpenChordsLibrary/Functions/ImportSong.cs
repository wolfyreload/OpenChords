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

                sbLyrics.AppendLine(line);
            }

            return sbLyrics.ToString();
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
