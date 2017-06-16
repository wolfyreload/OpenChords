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
                Title = Title
            };

            var result = Engine.Razor.RunCompile(HtmlResources.BaseSongHtml, "BaseSongHtml", typeof(SetViewModel), model);
         
            return result;
        }
        
    }
}
