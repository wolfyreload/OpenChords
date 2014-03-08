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
            this.txtHeading = new System.Windows.Forms.RichTextBox();
            this.txtOrder = new System.Windows.Forms.RichTextBox();
            this.flowSongSegments = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // txtHeading
            // 
            this.txtHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeading.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeading.Location = new System.Drawing.Point(4, 4);
            this.txtHeading.Name = "txtHeading";
            this.txtHeading.Size = new System.Drawing.Size(656, 36);
            this.txtHeading.TabIndex = 0;
            this.txtHeading.Text = "";
            // 
            // txtOrder
            // 
            this.txtOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOrder.Location = new System.Drawing.Point(4, 47);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(656, 32);
            this.txtOrder.TabIndex = 1;
            this.txtOrder.Text = "";
            // 
            // flowSongSegments
            // 
            this.flowSongSegments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowSongSegments.Location = new System.Drawing.Point(4, 85);
            this.flowSongSegments.Name = "flowSongSegments";
            this.flowSongSegments.Size = new System.Drawing.Size(656, 355);
            this.flowSongSegments.TabIndex = 2;
            // 
            // comSongDisplay
            // 
            this.Controls.Add(this.flowSongSegments);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.txtHeading);
            this.Name = "comSongDisplay";
            this.Size = new System.Drawing.Size(663, 443);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtHeading;
        private System.Windows.Forms.RichTextBox txtOrder;
        private System.Windows.Forms.FlowLayoutPanel flowSongSegments;
    }
}
