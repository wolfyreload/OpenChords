/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 08:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OpenChords.Forms
{
	partial class DisplayForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.listSongs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenChordsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.showHideDualPanelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferSharpsFlatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenChordsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listSongs
            // 
            this.listSongs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSongs.FormattingEnabled = true;
            this.listSongs.Location = new System.Drawing.Point(828, 24);
            this.listSongs.Name = "listSongs";
            this.listSongs.Size = new System.Drawing.Size(200, 563);
            this.listSongs.TabIndex = 0;
            this.listSongs.Visible = false;
            this.listSongs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListSongsKeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(849, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Press Escape to Close Presentation";
            // 
            // OpenChordsMenuStrip
            // 
            this.OpenChordsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.keyToolStripMenuItem,
            this.visibilityToolStripMenuItem,
            this.navigationToolStripMenuItem});
            this.OpenChordsMenuStrip.Name = "OpenChordsMenuStrip";
            this.OpenChordsMenuStrip.ShowImageMargin = false;
            this.OpenChordsMenuStrip.Size = new System.Drawing.Size(128, 136);
            this.OpenChordsMenuStrip.Opened += new System.EventHandler(this.OpenChordsMenuStrip_Opened);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Refresh";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseSizeToolStripMenuItem,
            this.decreaseSizeToolStripMenuItem});
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
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
            this.keyToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
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
            this.showHideSongListToolStripMenuItem,
            this.showHideDualPanelsToolStripMenuItem,
            this.preferSharpsFlatsToolStripMenuItem});
            this.visibilityToolStripMenuItem.Name = "visibilityToolStripMenuItem";
            this.visibilityToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.visibilityToolStripMenuItem.Text = "Visibility";
            // 
            // showHideChordsToolStripMenuItem
            // 
            this.showHideChordsToolStripMenuItem.Name = "showHideChordsToolStripMenuItem";
            this.showHideChordsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.showHideChordsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.showHideChordsToolStripMenuItem.Text = "Show/Hide Chords";
            this.showHideChordsToolStripMenuItem.Click += new System.EventHandler(this.showHideChordsToolStripMenuItem_Click);
            // 
            // showHideLyricsToolStripMenuItem
            // 
            this.showHideLyricsToolStripMenuItem.Name = "showHideLyricsToolStripMenuItem";
            this.showHideLyricsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+W";
            this.showHideLyricsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.showHideLyricsToolStripMenuItem.Text = "Show/Hide Lyrics";
            this.showHideLyricsToolStripMenuItem.Click += new System.EventHandler(this.showHideLyricsToolStripMenuItem_Click);
            // 
            // showHideNotesToolStripMenuItem
            // 
            this.showHideNotesToolStripMenuItem.Name = "showHideNotesToolStripMenuItem";
            this.showHideNotesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.showHideNotesToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.showHideNotesToolStripMenuItem.Text = "Show/Hide Notes";
            this.showHideNotesToolStripMenuItem.Click += new System.EventHandler(this.showHideNotesToolStripMenuItem_Click);
            // 
            // showHideSongListToolStripMenuItem
            // 
            this.showHideSongListToolStripMenuItem.Name = "showHideSongListToolStripMenuItem";
            this.showHideSongListToolStripMenuItem.ShortcutKeyDisplayString = "L";
            this.showHideSongListToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.showHideSongListToolStripMenuItem.Text = "Show/Hide Song List";
            this.showHideSongListToolStripMenuItem.Click += new System.EventHandler(this.showHideSongListToolStripMenuItem_Click);
            // 
            // showHideDualPanelsToolStripMenuItem
            // 
            this.showHideDualPanelsToolStripMenuItem.Name = "showHideDualPanelsToolStripMenuItem";
            this.showHideDualPanelsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+D";
            this.showHideDualPanelsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.showHideDualPanelsToolStripMenuItem.Text = "Show/Hide Dual Panels";
            this.showHideDualPanelsToolStripMenuItem.Click += new System.EventHandler(this.showHideDualPanelsToolStripMenuItem_Click);
            // 
            // preferSharpsFlatsToolStripMenuItem
            // 
            this.preferSharpsFlatsToolStripMenuItem.Name = "preferSharpsFlatsToolStripMenuItem";
            this.preferSharpsFlatsToolStripMenuItem.ShortcutKeyDisplayString = "S";
            this.preferSharpsFlatsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.preferSharpsFlatsToolStripMenuItem.Text = "Prefer Sharps/Flats";
            // 
            // navigationToolStripMenuItem
            // 
            this.navigationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextSongToolStripMenuItem,
            this.previousSongToolStripMenuItem,
            this.nextPageToolStripMenuItem,
            this.previousPageToolStripMenuItem});
            this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
            this.navigationToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
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
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1028, 590);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listSongs);
            this.MaximizeBox = false;
            this.Name = "DisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DisplayForm";
            this.Load += new System.EventHandler(this.DisplayFormLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayFormPaint);
            this.OpenChordsMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ListBox listSongs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip OpenChordsMenuStrip;
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
        private System.Windows.Forms.ToolStripMenuItem showHideDualPanelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferSharpsFlatsToolStripMenuItem;
		
		
	}
}
