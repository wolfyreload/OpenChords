using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using OpenChords.Entities;

namespace OpenChords.CrossPlatform.Helpers
{
    public static class MenuHelper
    {
        public static Command GetCommand(string title, Image Icon=null, ShortcutKey Keys=null, object Tag=null)
        {
            var command = new Command();
            command.MenuText = title;
            if (Icon != null)
                command.Image = Icon;
            if (Keys != null && !string.IsNullOrWhiteSpace(Keys.Key))
                command.Shortcut = GetEtoShortcut(Keys);
            if (Tag != null)
                command.Tag = Tag;
            return command;

        }

        private static Keys GetEtoShortcut(ShortcutKey key)
        {
            Keys mainKey = Keys.None;
            Enum.TryParse<Keys>(key.Key, out mainKey);
            Keys alt = key.UseAlt ? Application.Instance.AlternateModifier : Keys.None;
            Keys control = key.UseControl ? Application.Instance.CommonModifier : Keys.None;
      
            return alt | control | mainKey;
        }

        internal static bool CompareShortcuts(KeyEventArgs e, ShortcutKey configuredShortcutKey)
        {
            if (configuredShortcutKey == null || string.IsNullOrWhiteSpace(configuredShortcutKey.Key)) return false;
            bool matching = true;
            matching &= e.Control == configuredShortcutKey.UseControl;
            matching &= e.Alt == configuredShortcutKey.UseAlt;
            matching &= e.Key.ToString() == configuredShortcutKey.Key;
            return matching;
        }
    }
}
