using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
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
            string filename = $"{Settings.GlobalApplicationSettings.PrintFolder}{currentSet.setName}_{DateTime.Now:yyyy-MM-dd_HHmmss}.xlsx";
            exportListToFile(listOfSongs, filename, currentSet.setName);
            return filename;
        }

        public static string exportSongList(List<Song> listOfSongs)
        {
            string filename = $"{Settings.GlobalApplicationSettings.PrintFolder}Song_List_{DateTime.Now:yyyy-MM-dd_HHmmss}.xlsx";
            exportListToFile(listOfSongs, filename, "All Songs");
            return filename;
        }

        private static void exportListToFile(List<Song> listOfSongs, string nameOfFile, string setName = null)
        {
            IWorkbook workbook = new XSSFWorkbook();
            var worksheet = workbook.CreateSheet("SongList");

            // Set the width of columns
            worksheet.SetColumnWidth(0, 25 * 256);
            worksheet.SetColumnWidth(1, 40 * 256);
            worksheet.SetColumnWidth(2, 40 * 256);
            worksheet.SetColumnWidth(3, 11 * 256);
            worksheet.SetColumnWidth(4, 11 * 256);
            worksheet.SetColumnWidth(5, 11 * 256);
            worksheet.SetColumnWidth(6, 40 * 256);
            worksheet.SetColumnWidth(7, 11 * 256);

            // Headings
            IRow headingRow = worksheet.CreateRow(0);
            if (!string.IsNullOrWhiteSpace(setName))
            {
                headingRow.CreateCell(0).SetCellValue(setName);
            }
            headingRow.CreateCell(1).SetCellValue("Sub Folder");
            headingRow.CreateCell(2).SetCellValue("Song Title");
            headingRow.CreateCell(3).SetCellValue("Alternative Title");
            headingRow.CreateCell(4).SetCellValue("Reference");
            headingRow.CreateCell(5).SetCellValue("Key & Capo");
            headingRow.CreateCell(6).SetCellValue("Order");
            headingRow.CreateCell(7).SetCellValue("CCLI");
            

            // Song content
            int contentRow = 1;
            foreach (Song song in listOfSongs)
            {
                var songContentRow = worksheet.CreateRow(contentRow);
                songContentRow.CreateCell(1).SetCellValue(song.SongSubFolder);
                songContentRow.CreateCell(2).SetCellValue(song.title);
                songContentRow.CreateCell(3).SetCellValue(song.aka);
                songContentRow.CreateCell(4).SetCellValue(song.hymn_number);
                songContentRow.CreateCell(5).SetCellValue(song.getKeyAndCapo());
                songContentRow.CreateCell(6).SetCellValue(song.presentation);
                songContentRow.CreateCell(7).SetCellValue(song.ccli);
                contentRow++;
            }

            using (FileStream streamWriter = File.Create(nameOfFile))
            {
                workbook.Write(streamWriter);
                streamWriter.Close();
            }
        }
    }
}
