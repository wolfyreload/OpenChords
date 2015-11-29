using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChords.Entities;

namespace OpenChords.CrossPlatform.Preferences
{
    class ShortcutKeyPicker : TableRow
    {
        protected CheckBox chkUseControl = new CheckBox();
        protected CheckBox chkUseAlt = new CheckBox();
        protected TextBox txtShortcutKey = new TextBox();
        private ShortcutKey _oldShortcutKey;
      
        public ShortcutKeyPicker(string title, ShortcutKey shortcutKey)
        {
            this._oldShortcutKey = shortcutKey;

            chkUseAlt.CheckedBinding.Bind(shortcutKey, s => s.UseAlt);
            chkUseControl.CheckedBinding.Bind(shortcutKey, s => s.UseControl);
            txtShortcutKey.TextBinding.Bind(shortcutKey, s => s.Key);

            txtShortcutKey.KeyDown += TxtShortcutKey_KeyDown;

            Cells = new System.Collections.ObjectModel.Collection<TableCell>();
            Cells.Add(new Label() { Text = title });
            Cells.Add(new Label() { Text = "Control" });
            Cells.Add(chkUseControl);
            Cells.Add(new Label() { Text = "Alt" });
            Cells.Add(chkUseAlt);
            Cells.Add(new Label() { Text = "Shortcut Key" });
            Cells.Add(txtShortcutKey);
            Cells.Add(new Label());
        }

        private void TxtShortcutKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Keys.Control && e.Key != Keys.Alt && e.Key != Keys.Shift && e.Key != Keys.None)
                txtShortcutKey.Text = e.Key.ToString();
            else
                txtShortcutKey.Text = "";
            e.Handled = true;
        }
    }
}
