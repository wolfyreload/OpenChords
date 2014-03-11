namespace OpenChords.Forms
{
    partial class DisplayForm2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseCapoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseCapoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideChordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideLyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideSongListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenChordsMenuStrip2 = new System.Windows.Forms.MenuStrip();
            this.songListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.comSongDisplay1 = new OpenChords.UserControls.comSongDisplay();
            this.OpenChordsMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.exitToolStripMenuItem.Text = "Refresh";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseSizeToolStripMenuItem,
            this.decreaseSizeToolStripMenuItem});
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.sizeToolStripMenuItem.Text = "Size";
            // 
            // increaseSizeToolStripMenuItem
            // 
            this.increaseSizeToolStripMenuItem.Name = "increaseSizeToolStripMenuItem";
            this.increaseSizeToolStripMenuItem.ShortcutKeyDisplayString = "+";
            this.increaseSizeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.increaseSizeToolStripMenuItem.Text = "Increase Size";
            this.increaseSizeToolStripMenuItem.ToolTipText = "+";
            this.increaseSizeToolStripMenuItem.Click += new System.EventHandler(this.increaseSizeToolStripMenuItem_Click);
            // 
            // decreaseSizeToolStripMenuItem
            // 
            this.decreaseSizeToolStripMenuItem.Name = "decreaseSizeToolStripMenuItem";
            this.decreaseSizeToolStripMenuItem.ShortcutKeyDisplayString = "-";
            this.decreaseSizeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.decreaseSizeToolStripMenuItem.Text = "Decrease Size";
            this.decreaseSizeToolStripMenuItem.ToolTipText = "-";
            this.decreaseSizeToolStripMenuItem.Click += new System.EventHandler(this.decreaseSizeToolStripMenuItem_Click);
            // 
            // keyToolStripMenuItem
            // 
            this.keyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseKeyToolStripMenuItem,
            this.decreaseKeyToolStripMenuItem,
            this.increaseCapoToolStripMenuItem,
            this.decreaseCapoToolStripMenuItem});
            this.keyToolStripMenuItem.Name = "keyToolStripMenuItem";
            this.keyToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.keyToolStripMenuItem.Text = "Key";
            // 
            // increaseKeyToolStripMenuItem
            // 
            this.increaseKeyToolStripMenuItem.Name = "increaseKeyToolStripMenuItem";
            this.increaseKeyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+{+}";
            this.increaseKeyToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.increaseKeyToolStripMenuItem.Text = "Increase Key";
            this.increaseKeyToolStripMenuItem.Click += new System.EventHandler(this.increaseKeyToolStripMenuItem_Click);
            // 
            // decreaseKeyToolStripMenuItem
            // 
            this.decreaseKeyToolStripMenuItem.Name = "decreaseKeyToolStripMenuItem";
            this.decreaseKeyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+{-}";
            this.decreaseKeyToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.decreaseKeyToolStripMenuItem.Text = "Decrease Key";
            this.decreaseKeyToolStripMenuItem.Click += new System.EventHandler(this.decreaseKeyToolStripMenuItem_Click);
            // 
            // increaseCapoToolStripMenuItem
            // 
            this.increaseCapoToolStripMenuItem.Name = "increaseCapoToolStripMenuItem";
            this.increaseCapoToolStripMenuItem.ShortcutKeyDisplayString = "Shift+{+}";
            this.increaseCapoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.increaseCapoToolStripMenuItem.Text = "Increase Capo";
            this.increaseCapoToolStripMenuItem.Click += new System.EventHandler(this.increaseCapoToolStripMenuItem_Click);
            // 
            // decreaseCapoToolStripMenuItem
            // 
            this.decreaseCapoToolStripMenuItem.Name = "decreaseCapoToolStripMenuItem";
            this.decreaseCapoToolStripMenuItem.ShortcutKeyDisplayString = "Shift+{-}";
            this.decreaseCapoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.decreaseCapoToolStripMenuItem.Text = "Decrease Capo";
            this.decreaseCapoToolStripMenuItem.Click += new System.EventHandler(this.decreaseCapoToolStripMenuItem_Click);
            // 
            // visibilityToolStripMenuItem
            // 
            this.visibilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideChordsToolStripMenuItem,
            this.showHideLyricsToolStripMenuItem,
            this.showHideNotesToolStripMenuItem,
            this.showHideSongListToolStripMenuItem});
            this.visibilityToolStripMenuItem.Name = "visibilityToolStripMenuItem";
            this.visibilityToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.visibilityToolStripMenuItem.Text = "Visibility";
            // 
            // showHideChordsToolStripMenuItem
            // 
            this.showHideChordsToolStripMenuItem.Name = "showHideChordsToolStripMenuItem";
            this.showHideChordsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.showHideChordsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showHideChordsToolStripMenuItem.Text = "Show/Hide Chords";
            this.showHideChordsToolStripMenuItem.Click += new System.EventHandler(this.showHideChordsToolStripMenuItem_Click);
            // 
            // showHideLyricsToolStripMenuItem
            // 
            this.showHideLyricsToolStripMenuItem.Name = "showHideLyricsToolStripMenuItem";
            this.showHideLyricsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+W";
            this.showHideLyricsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showHideLyricsToolStripMenuItem.Text = "Show/Hide Lyrics";
            this.showHideLyricsToolStripMenuItem.Click += new System.EventHandler(this.showHideLyricsToolStripMenuItem_Click);
            // 
            // showHideNotesToolStripMenuItem
            // 
            this.showHideNotesToolStripMenuItem.Name = "showHideNotesToolStripMenuItem";
            this.showHideNotesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.showHideNotesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showHideNotesToolStripMenuItem.Text = "Show/Hide Notes";
            this.showHideNotesToolStripMenuItem.Click += new System.EventHandler(this.showHideNotesToolStripMenuItem_Click);
            // 
            // showHideSongListToolStripMenuItem
            // 
            this.showHideSongListToolStripMenuItem.Name = "showHideSongListToolStripMenuItem";
            this.showHideSongListToolStripMenuItem.ShortcutKeyDisplayString = "L";
            this.showHideSongListToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showHideSongListToolStripMenuItem.Text = "Show/Hide Song List";
            // 
            // navigationToolStripMenuItem
            // 
            this.navigationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextSongToolStripMenuItem,
            this.previousSongToolStripMenuItem,
            this.nextPageToolStripMenuItem,
            this.previousPageToolStripMenuItem});
            this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
            this.navigationToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.navigationToolStripMenuItem.Text = "Navigation";
            // 
            // nextSongToolStripMenuItem
            // 
            this.nextSongToolStripMenuItem.Name = "nextSongToolStripMenuItem";
            this.nextSongToolStripMenuItem.ShortcutKeyDisplayString = "PageDown";
            this.nextSongToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.nextSongToolStripMenuItem.Text = "Next Song";
            this.nextSongToolStripMenuItem.Click += new System.EventHandler(this.nextSongToolStripMenuItem_Click);
            // 
            // previousSongToolStripMenuItem
            // 
            this.previousSongToolStripMenuItem.Name = "previousSongToolStripMenuItem";
            this.previousSongToolStripMenuItem.ShortcutKeyDisplayString = "PageUp";
            this.previousSongToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.previousSongToolStripMenuItem.Text = "Previous Song";
            this.previousSongToolStripMenuItem.Click += new System.EventHandler(this.previousSongToolStripMenuItem_Click);
            // 
            // nextPageToolStripMenuItem
            // 
            this.nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
            this.nextPageToolStripMenuItem.ShortcutKeyDisplayString = "Down/Right/Space";
            this.nextPageToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.nextPageToolStripMenuItem.Text = "Next Page";
            this.nextPageToolStripMenuItem.Click += new System.EventHandler(this.nextPageToolStripMenuItem_Click);
            // 
            // previousPageToolStripMenuItem
            // 
            this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
            this.previousPageToolStripMenuItem.ShortcutKeyDisplayString = "Up/Left";
            this.previousPageToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.previousPageToolStripMenuItem.Text = "Previous Page";
            this.previousPageToolStripMenuItem.Click += new System.EventHandler(this.previousPageToolStripMenuItem_Click);
            // 
            // OpenChordsMenuStrip2
            // 
            this.OpenChordsMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.keyToolStripMenuItem,
            this.visibilityToolStripMenuItem,
            this.navigationToolStripMenuItem,
            this.songListToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.OpenChordsMenuStrip2.Location = new System.Drawing.Point(0, 0);
            this.OpenChordsMenuStrip2.Name = "OpenChordsMenuStrip2";
            this.OpenChordsMenuStrip2.Size = new System.Drawing.Size(912, 24);
            this.OpenChordsMenuStrip2.TabIndex = 3;
            this.OpenChordsMenuStrip2.Text = "menuStrip1";
            // 
            // songListToolStripMenuItem
            // 
            this.songListToolStripMenuItem.Name = "songListToolStripMenuItem";
            this.songListToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.songListToolStripMenuItem.Text = "Song List";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // comSongDisplay1
            // 
            this.comSongDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comSongDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.comSongDisplay1.Enabled = false;
            this.comSongDisplay1.Location = new System.Drawing.Point(13, 27);
            this.comSongDisplay1.Name = "comSongDisplay1";
            this.comSongDisplay1.Size = new System.Drawing.Size(887, 508);
            this.comSongDisplay1.TabIndex = 0;
            // 
            // DisplayForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 547);
            this.Controls.Add(this.OpenChordsMenuStrip2);
            this.Controls.Add(this.comSongDisplay1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.OpenChordsMenuStrip2;
            this.Name = "DisplayForm2";
            this.Text = "OpenChords Display";
            this.Load += new System.EventHandler(this.DisplayForm2_Load);
            this.OpenChordsMenuStrip2.ResumeLayout(false);
            this.OpenChordsMenuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.comSongDisplay comSongDisplay1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseCapoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseCapoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideChordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideLyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideSongListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousPageToolStripMenuItem;
        private System.Windows.Forms.MenuStrip OpenChordsMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem songListToolStripMenuItem;



    }
}