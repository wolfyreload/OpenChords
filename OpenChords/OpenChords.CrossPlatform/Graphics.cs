using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using System.IO;

namespace OpenChords.CrossPlatform
{
    class Graphics
    {
        private static Dictionary<string, Image> _imageCache = new Dictionary<string, Image>();
        private static Icon _iconCache;

        public static Icon Icon
        {
            get
            {
                if (_iconCache == null)
                    _iconCache = new Icon("Resources/guitar.ico");
                return _iconCache;
            }
        }

        public static Image ImageExit { get { return getBitmap("Resources/Exit.png"); } }
        public static Image ImagePresentSet { get { return getBitmap("Resources/PresentSet.png"); } }
        public static Image ImagePresentSong { get { return getBitmap("Resources/PresentSong.png"); } }
        public static Image ImagMoveUp { get { return getBitmap("Resources/Up.png"); } }
        public static Image ImageMoveDown { get { return getBitmap("Resources/Down.png"); } }
        public static Image ImageLeft { get { return getBitmap("Resources/Left.png"); } }
        public static Image ImageRight { get { return getBitmap("Resources/Right.png"); } }
        public static Image ImageDelete { get { return getBitmap("Resources/Delete.png"); } }
        public static Image ImageCancel { get { return getBitmap("Resources/Cancel.png"); } }
        public static Image ImageAbout { get { return getBitmap("Resources/About.png"); } }
        public static Image ImageSearch { get { return getBitmap("Resources/AdvancedSearch.png"); } }
        public static Image ImageExplore { get { return getBitmap("Resources/Explore.png"); } }
        public static Image ImageHelp { get { return getBitmap("Resources/Help.png"); } }
        public static Image ImageNew { get { return getBitmap("Resources/New.png"); } }
        public static Image ImageAddToSet { get { return getBitmap("Resources/OK.png"); } }
        public static Image ImageOpenSong { get { return getBitmap("Resources/OpenSong.png"); } }
        public static Image ImagePdf { get { return getBitmap("Resources/pdf.png"); } }
        public static Image ImagePreferences { get { return getBitmap("Resources/Preferences.png"); } }
        public static Image ImageRefresh { get { return getBitmap("Resources/Reload.png"); } }
        public static Image ImageRepairSong { get { return getBitmap("Resources/RepairSong.png"); } }
        public static Image ImageRevert { get { return getBitmap("Resources/Revert.png"); } }
        public static Image ImageSave { get { return getBitmap("Resources/Save.png"); } }
        public static Image ImageKey { get { return getBitmap("Resources/Key.png"); } }
        public static Image ImageCapo { get { return getBitmap("Resources/Capo.png"); } }
        public static Image ImageList { get { return getBitmap("Resources/List.png"); } }
        public static Image ImagePrint { get { return getBitmap("Resources/Print.png"); } }
        public static Image ImageHtml { get { return getBitmap("Resources/Html.png"); } }
        public static Image ImageSet { get { return getBitmap("Resources/Set.png"); } }
        public static Image ImageSong { get { return getBitmap("Resources/Song.png"); } }
        public static Image ImageAll { get { return getBitmap("Resources/All.png"); } }
        public static Image ImageFileOperations { get { return getBitmap("Resources/FileOperations.png"); } }
        public static Image ImageChords { get { return getBitmap("Resources/Chords.png"); } }
        public static Image ImageLyrics { get { return getBitmap("Resources/Lyrics.png"); } }
        public static Image ImageNotes { get { return getBitmap("Resources/Notes.png"); } }
        public static Image ImageSharps { get { return getBitmap("Resources/Sharps.png"); } }
        public static Image ImageMetronome { get { return getBitmap("Resources/Metronome.png"); } }
        public static Image ImageNavigation { get { return getBitmap("Resources/Navigation.png"); } }
        public static Image ImageOtherOptions { get { return getBitmap("Resources/OtherOptions.png"); } }
        public static Image ImageSize { get { return getBitmap("Resources/Size.png"); } }
        public static Image ImageVisibility { get { return getBitmap("Resources/Visibility.png"); } }



        private static Image getBitmap(string filename)
        {
            if (!_imageCache.ContainsKey(filename))
            {
                byte[] fileInBytes = File.ReadAllBytes(filename);
                Bitmap newBitmap = new Bitmap(fileInBytes); ;
                _imageCache[filename] = newBitmap;
            }
            return _imageCache[filename];
        }
    }
}
