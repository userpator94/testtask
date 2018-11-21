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
        public static string f2picpath = "";
        private static Dictionary<String, String> picboxes = new Dictionary<String, String>();
        private static string dragsource = "";
        private static int mouseclickcounter = 0;
             
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

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //toolTip1.Show(Path.GetFileName(treeView.SelectedNode.FullPath.ToString()), this, PointToClient(MousePosition), Int32.MaxValue);
            //toolTip1.ShowAlways = true;  
            //toolTip1.Show(Path.GetFileName(treeView.SelectedNode.FullPath.ToString()), this, new Point(treeView.Left + flowLayoutPanel1.Width + 1, treeView.Top + flowLayoutPanel1.Width + 1), Int32.MaxValue);            

            DoDragDrop(e.Item, DragDropEffects.Copy);
            dragsource = "tree";

            //toolTip1.SetToolTip(treeView, treeView.SelectedNode.Name);
            //Cursor _Cursor = Cursors.Default;
            ////toolTip1.SetToolTip(_Cursor, treeView.SelectedNode.Name.ToString());
            //ToolTip tt = new ToolTip();
            //IWin32Window win = this;
            //tt.Show("String", win, mousePosition);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawPicBoxMatrix();
            //listView1.View = View.Details;
            //listView1.Columns.Add("1", 120);
            ////listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);

            //ImageList imgs = new ImageList();
            //imgs.ImageSize = new Size(100, 100);
            ////Bitmap bmp = new Bitmap(@"C:\Users\Илья\Pictures\f6Wqe4jnb7U.jpg");
            //imgs.Images.Add(Image.FromFile(@"C:\Users\Илья\Pictures\f6Wqe4jnb7U.jpg"));
            //listView1.SmallImageList = imgs;
            //listView1.Items.Add("", 0);            
        }
        private void drawPicBoxMatrix()
        {
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
                int a = flowLayoutPanel1.Size.Width / (comboBox1.SelectedIndex + 2) - 7;
                pb.Location = new Point(i * a + 10, y);
                pb.Size = new Size(a, a);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.AllowDrop = true;
                pb.Name = "pictureBox" + (i + 1);
                pb.MouseDown += pictureMover;
                pb.DragEnter += picDragEnter;
                pb.DragDrop += picDragDrop;
                pb.ContextMenuStrip = contextMenuStrip1;
                //pb.MouseClick += pictureBox_MouseClick;

                if (picboxes.ContainsKey(pb.Name))
                {
                    if (picboxes[pb.Name] != "") pb.Image = Image.FromFile(picboxes[pb.Name]);
                }
                else
                    picboxes.Add(pb.Name, "");

                sortingDictionary(picboxes);
                //var sortedElements = picboxes.OrderBy(kvp => kvp.Value);
                flowLayoutPanel1.Controls.Add(pb);
                GC.Collect();
            }
        }

        private void sortingDictionary(Dictionary<String, String> dic)
        {
            Queue<int> ValueEmpty = new Queue<int>();
            Stack<String> ValueFit = new Stack<string>();
            for (int i = 1; i <= dic.Count; i++)
            {
                if (dic["pictureBox" + i] == "") ValueEmpty.Enqueue(i);
                if (dic["pictureBox" + i] != "") ValueFit.Push(i+"|"+dic["pictureBox" + i]);
            }

            if (ValueFit.Count > 0)
            {
                for (int i = 0; i < ValueFit.Count; i++)
                {
                    if (ValueEmpty.Count == 0) return;
                    string[] link = ValueFit.Pop().Split('|');
                    int pb = ValueEmpty.Dequeue();
                    if (pb > Convert.ToInt16(link[0])) return;
                    dic["pictureBox" + pb] = link[1];
                    dic["pictureBox" + link[0]] = "";
                }
            }

        }
        private void clear_button_Click(object sender, EventArgs e)
        {        
            foreach (PictureBox pbs in flowLayoutPanel1.Controls)
            {
                pbs.Image.Dispose();
                pbs.Image = null;
            }
            for(int i=1; i<=picboxes.Count; i++)
                picboxes["pictureBox" + i] = ""; 
        }

        private void pictureMover(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = (PictureBox)sender;
                var img = pb.Image;
                if (img == null) return;
                dragsource = pb.Name;
                if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
                {
                    if (!picboxes[pb.Name].Equals(""))
                        pb.Image = Image.FromFile(picboxes[pb.Name]);
                    else pb.Image = null;
                    //picboxes[pb.Name] = "";
                }

                //для даблклика
                mouseclickcounter++;
                if (mouseclickcounter==2 && ((PictureBox)sender).Image !=null)
                { preshow(sender); mouseclickcounter = 0; }             
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
            PictureBox pb = (PictureBox)sender;


            if (bmp ==null) //это срабатывает если из treeview в pb
            {
                string imagepath = treeView.SelectedNode.FullPath.ToString();
                string ss = rootpath.Substring(0, rootpath.LastIndexOf('\\') + 1) + imagepath;
                bmp = (Bitmap)Image.FromFile(ss);
                pb.Image = bmp;
                picboxes[pb.Name] = ss;
            }
            else //это срабатывает если между ячейками
            {
                if (dragsource.Equals("tree")) return;
                PictureBox pb_source;
                Image pic = (Image)pb.Image;
                string picS = picboxes[pb.Name];

                if (flowLayoutPanel1.Controls.ContainsKey(dragsource))
                {
                    pb_source = (PictureBox)flowLayoutPanel1.Controls[dragsource];
                    //pb_source = (PictureBox)this.Controls.Find(dragsource, true)[0];
                    //if (pb.Image != null)
                    //{
                        pb.Image = pb_source.Image;
                        picboxes[pb.Name] = picboxes[pb_source.Name];
                        pb_source.Image = bmp;
                        picboxes[pb_source.Name] = picS;
                    //}
                    //else
                    //{
                    //    pb.Image = pb_source.Image;
                    //    picboxes[pb.Name] = picboxes[pb_source.Name];
                    //}
                }                                                                
            }
                     
        }    
        

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            //var pic = (sender as PictureBox).ImageLocation;//pic is the Name of the PictureBox that is clicked
            //switch (e.Button){
            //    //case MouseButtons.Right:
            //    //    {

            //    //    } break;
            //    case MouseButtons.Left:{
            //            MessageBox.Show(pic);//Just for example
            //            //DesktopIconRightclick.Show(this, new Point(e.X, e.Y));
            //        } break;
            //}
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //f2picpath = ((PictureBox)contextMenuStrip1.SourceControl).ImageLocation;
                f2picpath = picboxes[((PictureBox)contextMenuStrip1.SourceControl).Name];
                if (f2picpath.Equals(null))
                {
                    MessageBox.Show("Изображение не найдено");
                    return;
                }
            }
            catch { MessageBox.Show("Изображение не найдено"); }
            Form f2 = new picform();
            f2.MaximizeBox = true;
            f2.Show();
        }
        private void предпросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel panelfloat = new Panel();
            Form1 f1 = new Form1();
            panelfloat.BackColor = Color.Black;
            panelfloat.BackgroundImage = ((PictureBox)contextMenuStrip1.SourceControl).Image;
            panelfloat.BackgroundImageLayout = ImageLayout.Zoom;
            panelfloat.AutoScroll = true;
            panelfloat.Location = flowLayoutPanel1.Location;
            this.Controls.Add(panelfloat);
            panelfloat.Size = new Size(this.ClientRectangle.Width - panelfloat.Location.X, this.ClientRectangle.Height - panelfloat.Location.Y);
            //panelfloat.Size = new Size(f1.Width - panelfloat.Location.X, f1.Height - panelfloat.Location.Y);
            panelfloat.BringToFront();
        }

        private void очиститьToolStripMenuItem1_Click(object sender, EventArgs e)
        {         
            ((PictureBox)contextMenuStrip1.SourceControl).Image.Dispose();
            ((PictureBox)contextMenuStrip1.SourceControl).Image = null;
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //f2picpath = ((PictureBox)contextMenuStrip1.SourceControl).ImageLocation;
            f2picpath = picboxes[((PictureBox)contextMenuStrip1.SourceControl).Name];
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((PictureBox)contextMenuStrip1.SourceControl).Image = Image.FromFile(f2picpath);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                flowLayoutPanel1.Width = flowLayoutPanel1.Height;
                drawPicBoxMatrix();
            }
            else
            {
                flowLayoutPanel1.Width = 370;
                drawPicBoxMatrix();
            }

        }

        private void preshow(object sender)
        {
            Panel panelfloat = new Panel();
            Form1 f1 = new Form1();
            panelfloat.BackColor = Color.Transparent;
            panelfloat.BackgroundImage = ((PictureBox)sender).Image;
            panelfloat.BackgroundImageLayout = ImageLayout.Zoom;
            panelfloat.AutoScroll = true;
            panelfloat.Name = "panelfloat";
            panelfloat.Location = flowLayoutPanel1.Location;
            this.Controls.Add(panelfloat);
            panelfloat.Size = new Size(this.ClientRectangle.Width - panelfloat.Location.X, this.ClientRectangle.Height - panelfloat.Location.Y);
            //panelfloat.Size = new Size(f1.Width - panelfloat.Location.X, f1.Height - panelfloat.Location.Y);
            panelfloat.BringToFront();            
            panelfloat.DoubleClick += preshow_DoubleClick;
        }

        private void preshow_DoubleClick(object sender, EventArgs e)
        {
            //((Panel)Controls["panelfloat"]).Dispose();
            //Controls.Remove((Panel)Controls["panelfloat"]);

            foreach (Control item in Controls)
            {
                if (item.Name == "panelfloat")
                {
                    Controls.Remove(item);
                    return; //important step
                }
            }
        }



    }
}
