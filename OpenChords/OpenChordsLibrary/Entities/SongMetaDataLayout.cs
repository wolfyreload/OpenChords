using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Entities
{
    public class SongMetaDataLayout
    {
        public string LeftMetadata { get; set; }
        public string MiddleMetadata { get; set; }
        public string RightMetadata { get; set; }

        public SongMetaDataLayout()
        {
            this.LeftMetadata = "";
            this.MiddleMetadata = "";
            this.RightMetadata = "";
        }

        public SongMetaDataLayout(string leftMetadata, string middleMetaData, string rightMetaData)
        {
            this.LeftMetadata = leftMetadata ?? "";
            this.MiddleMetadata = middleMetaData ?? "";
            this.RightMetadata = rightMetaData ?? "";
        }
    }
}
