using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    class Graphics
    {
        public static Eto.Drawing.Icon Icon
        {
            get
            {
                return new Eto.Drawing.Icon("Resources/guitar.ico");
            }
        }
    }
}
