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
    public partial class Form1 : Form
    {
        private static string rootpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
             
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            //pictureBox1.MouseDown += pictureMover;
            //pictureBox1.AllowDrop = true;
            //pictureBox1.DragEnter += picDragEnter;
            //pictureBox1.DragDrop += picDragDrop;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListDirectory(treeView, rootpath);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            Application.Exit();
        }

        private void задатьКореньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = this.folderBrowserDialog1.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                rootpath = folderBrowserDialog1.SelectedPath.ToString();
                ListDirectory(treeView, rootpath);
            }
            
        }

        private void фурьеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://google.gik-team.com/?q=%D0%B0%D0%BD%D0%B0%D0%BB%D0%B8%D0%B7+%D1%84%D1%83%D1%80%D1%8C%D0%B5");
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var extension = "";
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                if (imagiesExist(directory))
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }
                
            foreach (var file in directoryInfo.GetFiles())
            {   //.bmp, .jpg, .png, .gif, .tiff
                extension = Path.GetExtension(file.ToString());
                if (extension.Equals(".bmp") || extension.Equals(".jpg") || extension.Equals(".png") 
                    || extension.Equals(".gif") || extension.Equals(".tiff"))
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
            }                
            return directoryNode;
        }
        private static bool imagiesExist(DirectoryInfo directoryInfo)
        {
            var extension = "";
            foreach (var file in directoryInfo.GetFiles())
            {   //.bmp, .jpg, .png, .gif, .tiff
                extension = Path.GetExtension(file.ToString());
                if (extension.Equals(".bmp") || extension.Equals(".jpg") || extension.Equals(".png")
                    || extension.Equals(".gif") || extension.Equals(".tiff"))
                    return true;
            }
            return false;
        }

        private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
            //toolTip1.SetToolTip(treeView, treeView.SelectedNode.Name);
            //Cursor _Cursor = Cursors.Default;
            ////toolTip1.SetToolTip(_Cursor, treeView.SelectedNode.Name.ToString());
            //ToolTip tt = new ToolTip();
            //IWin32Window win = this;
            //tt.Show("String", win, mousePosition);
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            string imagepath = treeView.SelectedNode.FullPath.ToString();
            string ss = rootpath.Substring(0, rootpath.LastIndexOf('\\') + 1);
            textBox1.Text = ss + imagepath;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listView1.View = View.Details;
            //listView1.Columns.Add("1", 120);
            ////listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);

            //ImageList imgs = new ImageList();
            //imgs.ImageSize = new Size(100, 100);
            ////Bitmap bmp = new Bitmap(@"C:\Users\Илья\Pictures\f6Wqe4jnb7U.jpg");
            //imgs.Images.Add(Image.FromFile(@"C:\Users\Илья\Pictures\f6Wqe4jnb7U.jpg"));
            //listView1.SmallImageList = imgs;
            //listView1.Items.Add("", 0);

            flowLayoutPanel1.Margin = new Padding(15, 15, 15, 15);
            flowLayoutPanel1.Controls.Clear();            

            int count = 4;
            if (comboBox1.SelectedIndex == 0) count = 4;
            if (comboBox1.SelectedIndex == 1) count = 9;
            if (comboBox1.SelectedIndex == 2) count = 16;
            if (comboBox1.SelectedIndex == 3) count = 25;

            var y = 10;
            for (int i = 0; i < count; i++)
            {
                var pb = new PictureBox();
                int a = flowLayoutPanel1.Size.Width / (comboBox1.SelectedIndex+2) - 7;
                pb.Location = new Point(i * a + 10, y);
                pb.Size = new Size(a, a);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.MouseDown += pictureMover;
                pb.AllowDrop = true;
                pb.DragEnter += picDragEnter;
                pb.DragDrop += picDragDrop;
                pb.BorderStyle = BorderStyle.FixedSingle;
                flowLayoutPanel1.Controls.Add(pb);
            }
        }
        private void clear_button_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Controls.Clear();
            foreach (PictureBox pbs in flowLayoutPanel1.Controls)
            {
                pbs.Image = null;
                //pbs.Image.Dispose();
            }
        }

        private void pictureMover(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            var img = pb.Image;
            if (img == null) return;
            if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
            {
                pb.Image = null;
            }
        }

        private void picDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.Copy; ////
        }
        private void picDragDrop(object sender, DragEventArgs e)
        {
            var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            ///////
            if (bmp ==null)
            {
                string imagepath = treeView.SelectedNode.FullPath.ToString();
                string ss = rootpath.Substring(0, rootpath.LastIndexOf('\\') + 1) + imagepath;
                bmp = (Bitmap)Image.FromFile(ss);
            }
            ///////
            PictureBox pb = (PictureBox)sender;
            pb.Image = bmp;
        }

        private void contexMenu(object sender, MouseEventArgs e)
        {

        }

        private void полноэкранныйРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f2 = new picform();            
            f2.MaximizeBox = true;
            f2.Show();
        }
    }
}
