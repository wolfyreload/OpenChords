using Eto.Forms;

namespace OpenChords.CrossPlatform.Helpers
{
    public class FormHelper
    {
        private static int DisplayHeight = (int)Screen.PrimaryScreen.Bounds.Height;
        private static int DisplayWidth = (int)Screen.PrimaryScreen.Bounds.Width;

        public static int getScreenPercentageInPixels(int percentage, Control control = null)
        {
            int width = DisplayWidth;
            if (control != null && control.Width != -1)
                width = control.Width;
            return (int)(width * (percentage / 100.0));
        }
    }
}
