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
        public picform()
        {
            InitializeComponent();
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
            catch(Exception exc) { MessageBox.Show("Изображение не найдено"); }
        }

        private void picform_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
