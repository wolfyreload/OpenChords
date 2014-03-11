namespace OpenChords.UserControls
{
    partial class comSongDisplay
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowSongSegments = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowSongSegments
            // 
            this.flowSongSegments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowSongSegments.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSongSegments.Location = new System.Drawing.Point(4, 70);
            this.flowSongSegments.Name = "flowSongSegments";
            this.flowSongSegments.Size = new System.Drawing.Size(656, 370);
            this.flowSongSegments.TabIndex = 2;
            // 
            // comSongDisplay
            // 
            this.Controls.Add(this.flowSongSegments);
            this.Name = "comSongDisplay";
            this.Size = new System.Drawing.Size(663, 443);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowSongSegments;
    }
}
