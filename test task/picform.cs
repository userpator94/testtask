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
                pictureBox1.Image = Image.FromFile(Form1.f2picpath);
                label1.Text = Path.GetFileName(Form1.f2picpath).ToString();
            }
            catch(Exception exc) { MessageBox.Show("Изображение не найдено"); }
        }
    }
}
