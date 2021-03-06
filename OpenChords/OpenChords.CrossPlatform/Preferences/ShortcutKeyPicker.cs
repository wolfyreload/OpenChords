﻿using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChords.Entities;
using System.Collections.ObjectModel;

namespace OpenChords.CrossPlatform.Preferences
{
    class ShortcutKeyPicker : Collection<TableCell>
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

            this.Add(new Label() { Text = title });
            this.Add(new Label() { Text = "Control" });
            this.Add(chkUseControl);
            this.Add(new Label() { Text = "Alt" });
            this.Add(chkUseAlt);
            this.Add(new Label() { Text = "Shortcut Key" });
            this.Add(txtShortcutKey);
            this.Add(new Label());
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
