using OfficeOpenXml;
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
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("SongList");

                // Name
                if (!string.IsNullOrWhiteSpace(setName))
                {
                    worksheet.Cells["A1"].Value = setName;
                    worksheet.Row(1).Style.Font.Size = 14;
                    worksheet.Row(1).Style.Font.Bold = true;
                }

                // Headings
                worksheet.Cells["A3"].Value = "Song Title";
                worksheet.Cells["B3"].Value = "Alternative Title";
                worksheet.Cells["C3"].Value = "Reference";
                worksheet.Cells["D3"].Value = "Key & Capo";
                worksheet.Cells["E3"].Value = "Order";
                worksheet.Cells["F3"].Value = "Sub Folder";
                worksheet.Cells["G3"].Value = "CCLI";
                worksheet.Row(3).Style.Font.Bold = true;

                // Song content
                int startRow = 4;
                foreach (Song song in listOfSongs)
                {
                    worksheet.Cells[startRow, 1].Value = song.title;
                    worksheet.Cells[startRow, 2].Value = song.aka;
                    worksheet.Cells[startRow, 3].Value = song.hymn_number;
                    worksheet.Cells[startRow, 4].Value = song.getKeyAndCapo();
                    worksheet.Cells[startRow, 5].Value = song.presentation;
                    worksheet.Cells[startRow, 6].Value = song.SongSubFolder;
                    worksheet.Cells[startRow, 7].Value = song.ccli;

                    startRow++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                excelPackage.SaveAs(new FileInfo(nameOfFile));
            }
        }
    }
}
