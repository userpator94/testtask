using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_task
{
    public partial class f2zoom : Form
    {
        private static int f2h = Form1.f2h, f2w = Form1.f2w;
        private double[] zoomFactor = { 1, 1.25, 1.5, 2, 2.5, 3 };
        public f2zoom()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            trackBar1.Maximum = zoomFactor.Length - 1;
            panel1.MouseWheel += pb_MouseWheel;
            pb_init();            
        }

        private void pb_init()
        {
            pb_f2zoom.MouseWheel += pb_MouseWheel;            
            //pb_f2zoom.BackColor = Color.Transparent;
            pb_f2zoom.Dock = DockStyle.Fill;
            try
            {
                if (Form1.pic4picform != null)
                    pb_f2zoom.Image = Form1.pic4picform;
                else { pb_f2zoom.Image = Image.FromFile(Form1.f2picpath[1]); }
            }
            catch(Exception exc) { }
        }

        private void pb_MouseWheel(object sender, MouseEventArgs e)
        {                        
            if (e.Delta < 0)
            {
                if (pb_f2zoom.Height >= Form1.f2h || pb_f2zoom.Width >= Form1.f2w)
                {
                    pb_f2zoom.Dock = DockStyle.None;
                    //pb_f2zoom.Size = panel1.Size;
                    changeZoom(false);
                    if((pb_f2zoom.Width >= (panel1.Width) || pb_f2zoom.Height >= (panel1.Height)) && trackBar1.Value == 0)
                    {
                        pb_f2zoom.Size = new Size(panel1.Width, panel1.Height);
                        pb_f2zoom.Dock = DockStyle.Fill;
                        
                    }
                    return;
                }
                //Form f1 = new Form1();
                PictureBox pb_source = (PictureBox)(new Form1()).Controls.Find(Form1.f2picpath[0], true)[0];
                int a = pb_source.Height;
                if (this.Height <= (a+6) && this.Width <= (a + 6)) { ((PictureBox)sender).Image.Dispose(); this.Close(); return; }
                if ((int)(this.Width * 0.95) < a || (int)(this.Height * 0.95) < a)
                {
                    this.Size = new Size(a, a);
                }
                else
                {
                    this.Size = new Size((int)(this.Width * 0.95), (int)(this.Height * 0.95));
                }

            }
            else if (e.Delta > 0)
            {
                if (this.Height == Form1.f2h || this.Width == Form1.f2w)
                {
                    pb_f2zoom.Dock = DockStyle.None;
                    pb_f2zoom.Size = panel1.Size; //один раз должно сработать
                    changeZoom(true);
                    return;
                }
                if ((int)(Width * 1.05) > Form1.f2w || (int)(Height * 1.05) > Form1.f2h)
                {
                    pb_f2zoom.Dock = DockStyle.Fill;
                    Size = new Size(Form1.f2w, Form1.f2h);
                }
                else
                    Size = new Size((int)(Width * 1.05), (int)(Height * 1.05));
            }

        }

        private void changeZoom(bool b)
        {
            if (b && trackBar1.Value < trackBar1.Maximum) { trackBar1.Value++; setZoom(); }
            if (!b && trackBar1.Value > trackBar1.Minimum) { trackBar1.Value--; setZoom(); }
        }

        private void setZoom()
        {
            double newZoom = zoomFactor[trackBar1.Value];

            pb_f2zoom.Width = Convert.ToInt32(pb_f2zoom.Width * newZoom); //.Image
            pb_f2zoom.Height = Convert.ToInt32(pb_f2zoom.Height * newZoom);
        }

        private void pb_f2zoom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (pb_f2zoom.Image !=null) pb_f2zoom.Image.Dispose();
            this.Close();
        }
    }
}
