using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform.Helpers
{
    public class PopupMessages
    {
        public static void ShowInformationMessage(Control parent, string message, params string[] args)
        {
            message = setMessageParams(message, args);
            MessageBox.Show(parent, message, "", MessageBoxType.Information);
        }

        public static void ShowErrorMessage(Control parent, string message, params string[] args)
        {
            message = setMessageParams(message, args);
            MessageBox.Show(parent, message, "", MessageBoxType.Error);
        }

        public static void ShowErrorMessage(string message, params string[] args)
        {
            ShowErrorMessage(null, message, args);
        }

        public static bool ShowConfirmationMessage(Control parent, string message, params string[] args)
        {
            message = setMessageParams(message, args);
            return MessageBox.Show(parent, message, "", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.Yes) == DialogResult.Yes;
        }

        private static string setMessageParams(string message, string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                message = message.Replace("{" + i + "}", args[i]);
            }
            return message;
        }
    }
}
