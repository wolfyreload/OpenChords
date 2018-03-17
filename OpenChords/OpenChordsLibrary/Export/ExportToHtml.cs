using OpenChords.Entities;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.Export
{
    public class ExportToHtml
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class SetViewModel
        {
            public DisplayAndPrintSettings Settings { get; set; }
            public Set Set { get; set; }
            public bool EnableAutoRefresh { get; internal set; }
            public string Title { get; internal set; }
            public SongMetaDataLayout SongMetaDataLayoutTop { get; set; }
            public SongMetaDataLayout SongMetaDataLayoutMiddle { get; set; }
            public SongMetaDataLayout SongMetaDataLayoutBottom { get; set; }

        }


        private DisplayAndPrintSettings _settings;
        private Set _set;
        public string Filename { get; private set; }
        private string Title { get; set; }
        private bool _isSet;

        public ExportToHtml(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _settings = settings;
            Title = set.setName;
            Filename = set.setName + ".html";
            _isSet = true;
        }

        public ExportToHtml(Song song, DisplayAndPrintSettings settings)
        {
            _set = new Set();
            _set.addSongToSet(song);
            _settings = settings;
            Title = song.generateShortTitle();
            Filename = song.generateShortTitle() + ".html";
        }

        public string GenerateHtml(bool enableAutoRefresh = false)
        {
            SetViewModel model = new SetViewModel()
            {
                Settings = _settings,
                Set = _set,
                EnableAutoRefresh = enableAutoRefresh,
                Title = Title,
                SongMetaDataLayoutTop = new SongMetaDataLayout(),
                SongMetaDataLayoutMiddle = new SongMetaDataLayout(),
                SongMetaDataLayoutBottom = new SongMetaDataLayout()
            };
            fillinSongMetaData(model);
        
            var result = Engine.Razor.RunCompile(HtmlResources.BaseSongHtml, "BaseSongHtml", typeof(SetViewModel), model);
         
            return result;
        }

        private void fillinSongMetaData(SetViewModel model)
        {
            var settings = model.Settings;
            foreach (var song in model.Set.songList)
            {
                model.SongMetaDataLayoutTop.LeftMetadata = completeTemplate(song, settings.SongMetaDataLayoutTop.LeftMetadata);
                model.SongMetaDataLayoutTop.MiddleMetadata = completeTemplate(song, settings.SongMetaDataLayoutTop.MiddleMetadata);
                model.SongMetaDataLayoutTop.RightMetadata = completeTemplate(song, settings.SongMetaDataLayoutTop.RightMetadata);
                model.SongMetaDataLayoutMiddle.LeftMetadata = completeTemplate(song, settings.SongMetaDataLayoutMiddle.LeftMetadata);
                model.SongMetaDataLayoutMiddle.MiddleMetadata = completeTemplate(song, settings.SongMetaDataLayoutMiddle.MiddleMetadata);
                model.SongMetaDataLayoutMiddle.RightMetadata = completeTemplate(song, settings.SongMetaDataLayoutMiddle.RightMetadata);
                model.SongMetaDataLayoutBottom.LeftMetadata = completeTemplate(song, settings.SongMetaDataLayoutBottom.LeftMetadata);
                model.SongMetaDataLayoutBottom.MiddleMetadata = completeTemplate(song, settings.SongMetaDataLayoutBottom.MiddleMetadata);
                model.SongMetaDataLayoutBottom.RightMetadata = completeTemplate(song, settings.SongMetaDataLayoutBottom.RightMetadata);
            }
        }
        
        private static string completeTemplate(Song song, string template)
        {
            template = template.Replace("{{title}}", song.title);
            template = template.Replace("{{alternative-title}}", song.aka);
            template = template.Replace("{{reference}}", song.hymn_number);
            template = template.Replace("{{ccli}}", song.ccli);
            template = template.Replace("{{key}}", song.key);
            template = template.Replace("{{capo}}", song.capo);
            template = template.Replace("{{tempo}}", song.tempo);
            template = template.Replace("{{time-signature}}", song.time_sig);
            template = template.Replace("{{order}}", song.presentation);
            template = template.Replace("{{bpm}}", song.bbm);
            template = template.Replace("{{author}}", song.author);
            template = template.Replace("{{copyright}}", song.copyright);
            template = template.Replace("{{smart-title}}", song.generateLongTitle());
            return template;
        }
    }
}
