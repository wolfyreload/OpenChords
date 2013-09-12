using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords
{
    public class Settings
    {
        private static OpenChords.Config.ExtAppsAndDirClass _fileAndFolderSettings;

        static Settings()
        {
            _fileAndFolderSettings = new OpenChords.Config.ExtAppsAndDirClass();
        }

        public static OpenChords.Config.ExtAppsAndDirClass ExtAppsAndDir
        {
            get
            {
                return _fileAndFolderSettings;
            }
        }
    }
}
