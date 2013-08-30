﻿/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 05:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenChords.Functions;
using OpenChords.IO;
using OpenChords.Settings;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections.Generic;

using System.Xml.Serialization;
using System.Drawing;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 

namespace OpenChords.Entities
{
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class songStyle
    {

        private songStyleBackground backgroundField;

        [System.Xml.Serialization.XmlElementAttribute("background", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public songStyleBackground background
        {
            get
            {
                return this.backgroundField;
            }
            set
            {
                this.backgroundField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class songStyleBackground
    {

        private string filenameField;


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string filename
        {
            get
            {
                return this.filenameField;
            }
            set
            {
                this.filenameField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot("song")]
    public partial class Song
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string titleField;

        private string authorField;

        private string presentationField;

        private string capoField;

        private string keyField;

        private string lyricsField;

        private string ccliField;

        private string preferFlatsField;

        private string notesField;

        [XmlIgnore]
        public string notes
        {
            get
            {
                if (notesField == null)
                    return "";
                else
                    return notesField;

            }
            set
            {
                notesField = value;
            }

        }

        private songStyle styleField;

        
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string presentation
        {
            get
            {
                return this.presentationField;
            }
            set
            {
                this.presentationField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int capo
        {
            get
            {
                var capoValue = 0;
                int.TryParse(capoField, out capoValue);
                return capoValue;
            }
            set
            {
                this.capoField = value.ToString();
            }
        }

        
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lyrics
        {
            get
            {
                return this.lyricsField;
            }
            set
            {
                this.lyricsField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ccli
        {
            get
            {
                return this.ccliField;
            }
            set
            {
                this.ccliField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool preferFlats
        {
            get
            {
                var value = false;
                if (!string.IsNullOrEmpty(preferFlatsField))
                    bool.TryParse(preferFlatsField, out value);
                return value;
            }
            set
            {
                preferFlatsField = value.ToString();
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("style", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public songStyle style
        {
            get
            {
                return this.styleField;
            }
            set
            {
                this.styleField = value;
            }
        }

        public Song()
        {
            titleField = "";
            authorField = "";
            keyField = "";
            capoField = "0";
            presentationField = "";

            lyricsField = "";
            preferFlatsField = "false";
        }




        public static Song loadSong(string SongName)
        {
            Song song = XmlReaderWriter.readSong(ExtAppsAndDir.songsFolder + SongName);
            song.notes = Note.loadNotes(SongName).notes;
            return song;

        }

        public void saveSong()
        {


            if (this.title != "")
            {
                var noteClass = Note.loadNotes(this.title);
                noteClass.notes = notes;

                //remove the notes when saving the song
                XmlReaderWriter.writeSong(ExtAppsAndDir.songsFolder + this.title, this);
                
                noteClass.saveNotes();
            }
        }

        public void deleteSong()
        {
            if (this.title != "")
            {
                FileFolderFunctions.deleteFile(ExtAppsAndDir.songsFolder + this.title);
                FileFolderFunctions.deleteFile(ExtAppsAndDir.notesFolder + this.title);
            }

        }

        public void revertToSaved()
        {
            Song oldSave = loadSong(title);
            this.title = oldSave.title;
            this.notes = oldSave.notes;
            this.author = oldSave.author;
            this.capo = oldSave.capo;
            this.ccli = oldSave.ccli;
            this.key = oldSave.key;
            this.lyrics = oldSave.lyrics;
            this.presentation = oldSave.presentation;
            this.style = oldSave.style;


            var notesClass = Note.loadNotes(title);
            this.notes = notesClass.notes;
        }

        public void transposeKeyUp()
        {
            SongProcessor.transposeKeyUp(this);
            this.key = SongProcessor.transposeChord(this.key, this.preferFlats).TrimEnd();
        }

        public void transposeKeyDown()
        {
            for (int i = 0; i < 11; i++)
            {
                transposeKeyUp();
            }
        }

        public void capoUp()
        {
            for (int i = 0; i < 11; i++)
            {
                capoDown();
            }

        }

        public void capoDown()
        {
            SongProcessor.transposeKeyUp(this);
            int tempcapo = 12 + this.capo - 1;
            this.capo = tempcapo % 12;
        }

        public void fixFormatting()
        {
            SongProcessor.fixFormatting(this);
        }

        public void fixNoteOrdering()
        {
            SongProcessor.fixNoteOrdering(this);
        }

        public void fixLyricsOrdering()
        {
            SongProcessor.fixLyricsOrdering(this);
            SongProcessor.fixFormatting(this);
        }



        public bool isMp3Available()
        {

            string songName = ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return true;

            return false;

        }

        public string getMp3Filename()
        {
            string songName = ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return songName + ".mp3";
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return songName + ".mp4";
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return songName + ".avi";
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return songName + ".ogg";
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return songName + ".flv";
            else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return songName + ".mkv";

            return null;

        }

        public List<SongVerse> getSongVerses()
        {
            return SongVerse.getSongVerses(this);
        }

        public void displayPdf(DisplayAndPrintSettingsType settingsType)
        {
            //get the filemanager for the filesystem
            string fileManager = ExtAppsAndDir.fileManager;

            var pdfPath = Export.ExportToPdf.exportSong(this, settingsType);

            pdfPath = ExtAppsAndDir.printFolder + pdfPath;

            //try run the file with the default application
            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(pdfPath);
            else
                System.Diagnostics.Process.Start(fileManager, pdfPath);


        }

        /// <summary>
        /// Get the currently selected OpenSong image
        /// </summary>
        /// <returns></returns>
        public Image getSongImage()
        {
            Image outputImage = null;
            if (!String.IsNullOrEmpty(OpenSongImageFileName))
            {
                var path = OpenChords.Settings.ExtAppsAndDir.opensongBackgroundsFolder + OpenSongImageFileName;
                try
                {
                    outputImage = Image.FromFile(path);
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    logger.Error("Error file not found", ex);
                    return null;
                }
            }
            return outputImage;
        }

        /// <summary>
        /// read and write the OpenSongImageFileName for when we export to OpenSong
        /// </summary>
        [XmlIgnore]
        public string OpenSongImageFileName
        {
            get
            {
                if (style != null && style.background != null && !String.IsNullOrEmpty(style.background.filename))
                {
                    var backgroundFilename = style.background.filename;
                    return backgroundFilename;
                }
                else
                    return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    style = null;
                    style = new songStyle();
                    style.background = new songStyleBackground();
                    style.background.filename = value;
                }
            }


        }



    }

   

    

}
