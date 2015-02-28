using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FesterMapEditor
{
    public partial class StartupDialog : Form
    {
        public int LevelWidth
        {
            get { return int.Parse(txtLevelWidth.Text); }
        }
        public int LevelHeight
        {
            get { return int.Parse(txtLevelHeight.Text); }
        }
        public String BackGroundImage
        {
            get { return txtLevelBackground.Text; }
        }

        public string FileName
        {
            get { return txtLoad.Text; }
        }

        public StartupDialog()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtLevelBackground.Text = ofd.FileName;
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtLoad.Text = ofd.FileName;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
      
        }
    }
}
