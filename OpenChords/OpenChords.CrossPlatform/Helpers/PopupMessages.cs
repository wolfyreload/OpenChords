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
        public static void ShowInformationMessage(string message, params string[] args)
        {
            message = setMessageParams(message, args);
            MessageBox.Show(message, "", MessageBoxType.Information);
        }

        public static void ShowErrorMessage(string message, params string[] args)
        {
            message = setMessageParams(message, args);
            MessageBox.Show(message, "", MessageBoxType.Error);
        }

        public static bool ShowConfirmationMessage(string message, params string[] args)
        {
            message = setMessageParams(message, args);
            return MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.Yes) == DialogResult.Yes;
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
