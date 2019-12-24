using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 项目12
{
    public partial class Form1 : Form
    {
        String s_FileName = "";
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Undo();
        }

        private void 重复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Redo();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Paste();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.SelectAll();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbContent.Clear();
            s_FileName = "";
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "文本文件|*.txt|c#文件|*.cs|所有文件|*.*";
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s_FileName = openFileDialog1.FileName;
                rtbContent.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (s_FileName.Length != 0)
            {
                s_FileName = saveFileDialog1.FileName;
                rtbContent.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);//注意存取类型，要一样！
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (s_FileName.Length != 0)
            {
                rtbContent.SaveFile(s_FileName, RichTextBoxStreamType.PlainText);
            }
            else
                另存为ToolStripMenuItem_Click(sender, e);//调用另存为。
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                rtbContent.SelectionFont = fontDialog1.Font;
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                rtbContent.SelectionColor = colorDialog1.Color;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本记事本由郑某独立完成\n加密暂时不支持加密太长的文件");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            //获取当前时间
        }

        private void 加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectString;
            connectString = Tool.Encrypt(rtbContent.Text);
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(saveFileDialog1.FileName);

                objFile.WriteLine(connectString);

                objFile.Close();
                objFile.Dispose();
            }
        }

        private void 解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectString;

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                System.IO.StreamReader objFile = new System.IO.StreamReader(openFileDialog1.FileName);

                connectString = objFile.ReadLine();
                connectString = Tool.Decrypt(connectString);
                rtbContent.Text = connectString;

                objFile.Close();
                objFile.Dispose();
            }
        }
    }
}
