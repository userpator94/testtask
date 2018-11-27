using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_task
{
    public partial class picform : Form
    {
        private double[] zoomFactor = { .25, .33, .50, .66, .80, 1, 1.25, 1.5, 2.0, 2.5, 3.0 };

        public picform()
        {
            InitializeComponent();
            trackBar1.Maximum = zoomFactor.Length-1;
            trackBar1.LargeChange = 1;
            pictureBox1.MouseWheel += mouseWheel;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panel1.MouseWheel += mouseWheel;

        }

        private void picform_Load(object sender, EventArgs e)
        {            
            try
            {
                if (Form1.pic4picform != null)
                    pictureBox1.Image = Form1.pic4picform;
                else { pictureBox1.Image = Image.FromFile(Form1.f2picpath[1]); }

                label1.Text = Path.GetFileName(Form1.f2picpath[1]).ToString();

            }
            catch(Exception exc) { MessageBox.Show("Упс, что-то пошло не так :("); }
        }

        private void mouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.None;
            setZoom();
        }
        private void setZoom()
        {
            double newZoom = zoomFactor[trackBar1.Value];

            pictureBox1.Width = Convert.ToInt32(pictureBox1.Image.Width * newZoom);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Image.Height * newZoom);
        }

        private void mouseWheel(object sender, MouseEventArgs e)
        {
            pictureBox1.Dock = DockStyle.None;
            if (e.Delta < 0)
            {
                if (trackBar1.Value > 0) { trackBar1.Value--; setZoom(); }
                else return;
            }
            else
            {
                if (trackBar1.Value < trackBar1.Maximum) { trackBar1.Value++; setZoom(); }
                else return;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
        }

        private void picform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
        }
    }
}
