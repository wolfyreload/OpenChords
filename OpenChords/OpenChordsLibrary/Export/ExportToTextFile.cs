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
            List<Song> listOfSongs = currentSet.songList;
            string filename = $"{Settings.GlobalApplicationSettings.PrintFolder}{currentSet.setName}_{DateTime.Now:yyyy-MM-dd_HHmmss}.csv";
            exportListToFile(listOfSongs, filename, currentSet.setName);
            return filename;
        }

        public static string exportSongList(List<Song> listOfSongs)
        {
            string filename = $"{Settings.GlobalApplicationSettings.PrintFolder}Song_List_{DateTime.Now:yyyy-MM-dd_HHmmss}.csv";
            exportListToFile(listOfSongs, filename, "All Songs");
            return filename;
        }

        private static void exportListToFile(List<Song> listOfSongs, string nameOfFile, string setName = null)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(setName))
                sb.Append(setName + "\r\n\r\n");
            sb.Append("Song Title\tAlternative Title\tReference\tKey & Capo\tOrder\tSub Folder\r\n");
            foreach (Song song in listOfSongs)
            {
                sb.Append($"{song.title}\t{song.aka}\t{song.hymn_number}\t{song.getKeyAndCapo()}\t{song.presentation}\t{song.SongSubFolder}\r\n");
            }
            string setText = sb.ToString();
            File.WriteAllText(nameOfFile, setText);
        }
    }
}
