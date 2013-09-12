using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords
{
    public class Settings
    {
        private static OpenChords.Config.ExtAppsAndDirClass _fileAndFolderSettings;

        public static void setup(Entities.FileAndFolderSettings settings)
        {
            _fileAndFolderSettings = new Config.ExtAppsAndDirClass(settings);
        }

        public static OpenChords.Config.ExtAppsAndDirClass ExtAppsAndDir
        {
            get
            {
                return _fileAndFolderSettings;
            }
            set
            {
                _fileAndFolderSettings = value;
            }
        }
    }
}
