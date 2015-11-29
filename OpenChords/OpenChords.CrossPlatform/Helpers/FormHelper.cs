using Eto.Forms;

namespace OpenChords.CrossPlatform.Helpers
{
    public class FormHelper
    {
        private static int DisplayHeight = (int)Screen.PrimaryScreen.Bounds.Height;
        private static int DisplayWidth = (int)Screen.PrimaryScreen.Bounds.Width;

        public static int getScreenXPercentageInPixels(int percentage, Control control = null)
        {
            int width = DisplayWidth;
            if (control != null && control.Width != -1)
                width = control.Width;
            return (int)(width * (percentage / 100.0));
        }

        public static int getScreenYPercentageInPixels(int percentage, Control control = null)
        {
            int height = DisplayHeight;
            if (control != null && control.Height != -1)
                height = control.Height;
            return (int)(height * (percentage / 100.0));
        }
    }
}
