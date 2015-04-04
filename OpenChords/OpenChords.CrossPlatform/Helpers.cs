using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    public class Helpers
    {

        public static int getScreenPercentageInPixels(int percentage)
        {
            int ScreenWidth = (int)Screen.DisplayBounds.Width;
            return (int)(ScreenWidth * (percentage / 100.0));
        }
    }
}
