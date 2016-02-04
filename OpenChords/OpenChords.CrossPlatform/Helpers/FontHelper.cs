using Eto.Drawing;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform.Helpers
{
    public class FontHelper
    {
        public static Font GetFont(SongElementFormat elementFormat)
        {
            return new Font(elementFormat.FontName, elementFormat.FontSize);
        }
    }
}
