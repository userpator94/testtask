using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_task
{
    public partial class Form1 : Form
    {
        private static string rootpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public static string[] f2picpath = { "", "" };
        private static Dictionary<String, String> picboxes = new Dictionary<String, String>();
        private static string dragsource = "";
        private static int mouseclickcounter = 0;
        public static int f2w = 0, f2h = 0;
             
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListDirectory(treeView, rootpath);
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (PictureBox pbs in flowLayoutPanel1.Controls)
                {
                    if (pbs.Image != null)
                    {
                        pbs.Image.Dispose();
                        pbs.Image = null;
                        picboxes[pbs.Name] = "";
                    }
                    else continue;
                }
                //for (int i = 1; i <= picboxes.Count; i++)
                //    picboxes["pictureBox" + i] = "";
            }
            catch (Exception exc) { }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            Application.Exit();
        }

        private void изменитьПервыйКореньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = this.folderBrowserDialog1.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                rootpath = folderBrowserDialog1.SelectedPath.ToString();
                ListDirectory(treeView, rootpath);
            }
        }

        private void добавитьКореньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = this.folderBrowserDialog1.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                rootpath = folderBrowserDialog1.SelectedPath.ToString();
                treeView.Nodes.Add(CreateDirectoryNode(new DirectoryInfo(rootpath)));
                treeView.Nodes[treeView.Nodes.Count - 1].Expand();
            }            
        }

        private void сброситьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rootpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ListDirectory(treeView, rootpath);
        }

        private void анализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Bitmap image1;
            //int count = 0;
            //int red, green, blue;
            //int redt, greent, bluet;
            //double reshenie;

            //try
            //{

            //    // Retrieve the image.
            //    image1 = new Bitmap(@"C:\bg-img.jpg", true);
            //    double widht, height, pixel;
            //    int x, y;               

            //    // Loop through the images pixels            
            //    for (x = 0; x < image1.Width; x++)
            //    {
            //        for (y = 0; y < image1.Height; y++)
            //        {
            //            Color pixelColor = image1.GetPixel(x, y);
            //            redt = pixelColor.R;
            //            greent = pixelColor.G;
            //            bluet = pixelColor.B;


            //            if ((red + 10 >= redt) && (red - 10 >= redt))//i used +-10 in attempt to resolve the problem that i have writed about the close colours
            //            {

            //                if ((green - 10 <= greent) && (greent <= green + 10))
            //                {
            //                    if ((blue + 10 >= bluet) && (blue - 10 >= bluet))
            //                    {
            //                        count += 1;

            //                    }
            //                }
            //            }
            //        }
            //    }

            //    pictureBox1.Image = image1;

            //    MessageBox.Show("Imashe " + count.ToString());
            //    count = 0;

            //}
            //catch (ArgumentException)
            //{
            //    MessageBox.Show("There was an error." +
            //        "Check the path to the image file.");

            //}
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //        _thePanel.Location = new Point(
            //this.ClientSize.Width / 2 - _thePanel.Size.Width / 2,
            //this.ClientSize.Height / 2 - _thePanel.Size.Height / 2);
            //        _thePanel.Anchor = AnchorStyles.None;

            progressBar1.Visible = true;
            progressBar1.Maximum = picboxes.Count;
            progressBar1.Step = 1;

            this.Enabled = false;            
            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                picInversion(pb);
                progressBar1.PerformStep();
            }
            progressBar1.Visible = false;            
            this.Enabled = true;
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////https://docs.microsoft.com/ru-ru/azure/cognitive-services/computer-vision/quickstarts-sdk/csharp-analyze-sdk
        }

        private void фурьеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/userpator94/testtask");
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.Nodes[0].Expand();            
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
            try
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
            catch(Exception exc) { MessageBox.Show(exc.Message, "Что-то пошло не так"); return false; }
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
            drawPicBoxMatrix(true);           
        }
        private void drawPicBoxMatrix(bool b)
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

                if (picboxes.ContainsKey(pb.Name))
                {
                    if (picboxes[pb.Name] != "") pb.Image = Image.FromFile(picboxes[pb.Name]);
                }
                else
                    picboxes.Add(pb.Name, "");

                if (b) sortingDictionary(picboxes);
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
                if (pbs.Image != null)
                {
                    pbs.Image.Dispose();
                    pbs.Image = null;
                    picboxes[pbs.Name] = "";
                }
                else continue;
            }
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
                { preshow(sender); mouseclickcounter = 0; f2picpath[0] = ((PictureBox)sender).Name;
                    pic4picform = (Bitmap)((PictureBox)sender).Image; }             
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


        public static Bitmap pic4picform;        
        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                f2picpath[1] = picboxes[((PictureBox)contextMenuStrip1.SourceControl).Name];
                if (f2picpath.Equals(null))
                {
                    MessageBox.Show("Изображение не найдено");
                    return;
                }

                if (((PictureBox)contextMenuStrip1.SourceControl).Image != null)
                    pic4picform = (Bitmap)((PictureBox)contextMenuStrip1.SourceControl).Image;
            }
            catch { MessageBox.Show("Изображение не найдено"); }
            Form f2 = new picform();
            f2.MaximizeBox = true;
            f2.Show();            
        }
        private void предпросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2picpath[0] = ((PictureBox)contextMenuStrip1.SourceControl).Name;
            preshow(contextMenuStrip1.SourceControl);
        }

        private void очиститьToolStripMenuItem1_Click(object sender, EventArgs e)
        {         
            if (((PictureBox)contextMenuStrip1.SourceControl).Image != null)
            {
                ((PictureBox)contextMenuStrip1.SourceControl).Image.Dispose();
                ((PictureBox)contextMenuStrip1.SourceControl).Image = null;
            }            
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            f2picpath[1] = picboxes[((PictureBox)contextMenuStrip1.SourceControl).Name];
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((PictureBox)contextMenuStrip1.SourceControl).Image = Image.FromFile(f2picpath[1]);
        }
        private void инверсияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PictureBox pb = ((PictureBox)contextMenuStrip1.SourceControl);
            picInversion(pb);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                flowLayoutPanel1.Width = flowLayoutPanel1.Height;
                drawPicBoxMatrix(false);
            }
            else
            {
                flowLayoutPanel1.Width = 370;
                drawPicBoxMatrix(false);
            }

        }

        private void preshow(object sender)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name.Equals("f2zoom"))
                {
                    form.Focus();
                    return;
                }
            }

            try
            {
                f2picpath[1] = picboxes[((PictureBox)sender).Name];
                if (f2picpath.Equals(null) || ((PictureBox)sender).Image == null)
                {
                    MessageBox.Show("Изображение не найдено");
                    return;
                }

                Form f2 = new f2zoom();
                f2.Size = new Size(this.ClientRectangle.Width - (flowLayoutPanel1.Location.X + 15),
                    this.ClientRectangle.Height - (treeView.Location.Y) - 3);
                f2.Location = flowLayoutPanel1.PointToScreen(Point.Empty);
                //f2.TransparencyKey = f2.BackColor;
                pic4picform = (Bitmap)((PictureBox)sender).Image;
                f2.Show();
                f2.TopMost = true;

                f2w = f2.Width;
                f2h = f2.Height;
            }
            catch(Exception exc) { MessageBox.Show(exc.Message, "что-то пошло не так"); }
        }        

        private void picInversion(PictureBox pb)
        {            
            if (pb.Image != null)
            {
                Bitmap pic = new Bitmap(pb.Image);
                for (int y = 0; (y <= (pic.Height - 1)); y++)
                {
                    for (int x = 0; (x <= (pic.Width - 1)); x++)
                    {
                        Color inv = pic.GetPixel(x, y);
                        inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                        pic.SetPixel(x, y, inv);
                    }
                }
                pb.Image = pic;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{                
            //    toolTip1.Show(Path.GetFileName(treeView.SelectedNode.FullPath.ToString()), this, new Point(50,50), Int32.MaxValue);
            //    toolTip1.ShowAlways = true;
            //}            
        }

        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragsource.Equals("tree") && treeView.SelectedNode != treeView.Nodes[0] && e.Button == MouseButtons.Left)
            //{
            //    string s = Path.GetFileName(treeView.SelectedNode.FullPath.ToString());
            //    this.Cursor = CreateCursor((Bitmap)DrawText(s), 5, 5);
            //}
        }
        private Image DrawText(String s)
        {
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            SizeF sSize = drawing.MeasureString(s, this.Font);
            img.Dispose();
            drawing.Dispose();
            img = new Bitmap((int)sSize.Width, (int)sSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(Color.White);
            Brush textBrush = new SolidBrush(Color.Black);
            drawing.DrawString(s, this.Font, textBrush, 0, 0);
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }


        ////////// мой курсор
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);        

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }
        /////////// конец курсора
        
    }//class
}
