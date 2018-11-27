namespace test_task
{
    partial class f2zoom
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pb_f2zoom = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_f2zoom)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.pb_f2zoom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 318);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pb_f2zoom_MouseDoubleClick);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(3, 263);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(58, 25);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Visible = false;
            // 
            // pb_f2zoom
            // 
            this.pb_f2zoom.BackColor = System.Drawing.Color.White;
            this.pb_f2zoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_f2zoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_f2zoom.Location = new System.Drawing.Point(3, 3);
            this.pb_f2zoom.Name = "pb_f2zoom";
            this.pb_f2zoom.Size = new System.Drawing.Size(220, 205);
            this.pb_f2zoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_f2zoom.TabIndex = 1;
            this.pb_f2zoom.TabStop = false;
            this.pb_f2zoom.WaitOnLoad = true;
            this.pb_f2zoom.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pb_f2zoom_MouseDoubleClick);
            // 
            // f2zoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(520, 318);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "f2zoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "f2zoom";
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pb_f2zoom_MouseDoubleClick);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_f2zoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pb_f2zoom;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}