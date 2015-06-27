using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Export
{
    public class ExportToTextFile
    {
        public static string exportSetSongList(Set currentSet)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(currentSet.setName + "\r\n\r\n");
            foreach (Song song in currentSet.songList)
            {
                sb.Append(string.Format("{0} {1}\r\n", song.generateLongTitle(), song.presentation));

            }
            string filename = String.Format("{0}{1}_{2:yyyy-MM-dd_HHmmss}.txt", Settings.ExtAppsAndDir.printFolder, currentSet.setName, DateTime.Now);
            string setText = sb.ToString();
            File.WriteAllText(filename, setText);
            return filename;

        }
    }
}
