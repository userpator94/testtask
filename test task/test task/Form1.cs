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

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            string imagepath = treeView.SelectedNode.FullPath.ToString();
            int sl = rootpath.LastIndexOf('\\');
            string ss = rootpath.Substring(0, sl+1);
            textBox1.Text = ss + imagepath;
        }

        private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        
    }
}
