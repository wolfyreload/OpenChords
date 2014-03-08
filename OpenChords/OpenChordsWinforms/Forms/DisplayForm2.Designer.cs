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
            this.flowSong = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowSong
            // 
            this.flowSong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowSong.AutoScroll = true;
            this.flowSong.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSong.Location = new System.Drawing.Point(2, 2);
            this.flowSong.Margin = new System.Windows.Forms.Padding(0);
            this.flowSong.Name = "flowSong";
            this.flowSong.Size = new System.Drawing.Size(907, 542);
            this.flowSong.TabIndex = 0;
            // 
            // DisplayForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 547);
            this.Controls.Add(this.flowSong);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "DisplayForm2";
            this.Text = "OpenChords Display";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayForm2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowSong;


    }
}