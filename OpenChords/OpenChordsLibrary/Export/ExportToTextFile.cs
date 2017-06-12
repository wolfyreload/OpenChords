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
            sb.AppendFormat("{0}\t{1}\t{2}\r\n", "Song Title", "Key & Capo", "Order");
            foreach (Song song in currentSet.songList)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\r\n", song.title, song.getKeyAndCapo(), song.presentation);
            }
            string filename = String.Format("{0}{1}_{2:yyyy-MM-dd_HHmmss}.csv", Settings.GlobalApplicationSettings.PrintFolder, currentSet.setName, DateTime.Now);
            string setText = sb.ToString();
            File.WriteAllText(filename, setText);
            return filename;

        }
    }
}
