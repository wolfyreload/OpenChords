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
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.comSongDisplay1 = new OpenChords.UserControls.comSongDisplay();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowButtons
            // 
            this.flowButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowButtons.AutoScroll = true;
            this.flowButtons.Controls.Add(this.cmdPrevious);
            this.flowButtons.Controls.Add(this.cmdNext);
            this.flowButtons.Location = new System.Drawing.Point(13, 4);
            this.flowButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Size = new System.Drawing.Size(890, 32);
            this.flowButtons.TabIndex = 1;
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Location = new System.Drawing.Point(3, 3);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(75, 23);
            this.cmdPrevious.TabIndex = 0;
            this.cmdPrevious.Text = "previous";
            this.cmdPrevious.UseVisualStyleBackColor = true;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(84, 3);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 1;
            this.cmdNext.Text = "next";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // comSongDisplay1
            // 
            this.comSongDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comSongDisplay1.Enabled = false;
            this.comSongDisplay1.Location = new System.Drawing.Point(13, 39);
            this.comSongDisplay1.Name = "comSongDisplay1";
            this.comSongDisplay1.Size = new System.Drawing.Size(887, 496);
            this.comSongDisplay1.TabIndex = 0;
            // 
            // DisplayForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 547);
            this.Controls.Add(this.flowButtons);
            this.Controls.Add(this.comSongDisplay1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "DisplayForm2";
            this.Text = "OpenChords Display";
            this.Load += new System.EventHandler(this.DisplayForm2_Load);
            this.flowButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.comSongDisplay comSongDisplay1;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Button cmdNext;



    }
}