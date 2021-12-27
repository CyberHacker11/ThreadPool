using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadPool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Open = new OpenFileDialog();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(Open.ShowDialog() == DialogResult.OK)
            {
                tbxBrowse.Text = Open.FileName;
            }
            if (tbxPassword.Text.Length > 2 & tbxBrowse.Text != "") btnStart.Enabled = true;
            else btnStart.Enabled = false;
        }

        OpenFileDialog Open { get; set; }
        Timer Timer { get; set; }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbxPassword.Text.Length > 2 & tbxBrowse.Text != "") btnStart.Enabled = true;
            else btnStart.Enabled = false;
        }

        private void tbxBrowse_TextChanged(object sender, EventArgs e)
        {
            if (tbxPassword.Text.Length > 2 & tbxBrowse.Text != "") btnStart.Enabled = true;
            else btnStart.Enabled = false;
        }

        private void Progress(object sender, EventArgs e)
        {
            if (progressBar.Value < 100) progressBar.Value++;
            else
            {
                Timer.Stop();
                progressBar.Value = 0;
                MessageBox.Show("File Copied Successfully");
            }
        }

        private void Tick()
        {
            Timer = new Timer();
            Timer.Start();
            Timer.Enabled = true;
            Timer.Interval = 1000;
            progressBar.Maximum = 100;
            Timer.Tick += new EventHandler(Progress);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            rbtnEncrypt.Enabled = false;
            rbtnDecrypt.Enabled = true;
            rbtnDecrypt.Checked = true;
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            Tick();
        }
    }
}
