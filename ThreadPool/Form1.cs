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

namespace ThreadPool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Open = new OpenFileDialog();
            Timer = new System.Windows.Forms.Timer();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(Open.ShowDialog() == DialogResult.OK)
            {
                FileText = File.ReadAllText(Open.FileName);
                tbxBrowse.Text = Open.FileName;
            }
            if (tbxPassword.Text.Length > 2 & tbxBrowse.Text != "") btnStart.Enabled = true;
            else btnStart.Enabled = false;
        }

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
            if (progressBar.Value < 3) progressBar.Value++;
            else
            {
                Timer.Stop();
                progressBar.Value = 0;


                btnCancel.Enabled = false;
                EncryptingAndDecrypting();
            }
        }

        private void Tick()
        {
            Timer.Start();
            Timer.Enabled = true;
            Timer.Interval = 1000;
            progressBar.Maximum = 3;
            Timer.Tick += new EventHandler(Progress);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            Tick();
        }

        private void EncryptingAndDecrypting()
        {
            StringBuilder sb = new StringBuilder();
            int len = int.Parse(tbxPassword.Text);
            if (rbtnEncrypt.Enabled)
            {
                for (int i = 0; i < len; i++)
                {
                    sb.Append((char)(FileText[i] ^ 2));
                }
                for (int i = len; i < FileText.Length; i++)
                {
                    sb.Append(FileText[i]);
                }
                FileText = sb.ToString();
                MessageBox.Show("Text Sucessfully Encrypted!");
                File.WriteAllText(Open.FileName, FileText);
                
                btnStart.Enabled = rbtnDecrypt.Enabled = rbtnDecrypt.Checked = true;
                tbxPassword.Enabled = rbtnEncrypt.Enabled = false;
            }
            else if (rbtnDecrypt.Enabled)
            {
                for (int i = 0; i < len; i++)
                {
                    sb.Append((char)(FileText[i] ^ 2));
                }
                for (int i = len; i < FileText.Length; i++)
                {
                    sb.Append(FileText[i]);
                }
                FileText = sb.ToString();
                MessageBox.Show("Text Sucessfully Decrypting!");
                File.WriteAllText(Open.FileName, FileText);
            }
        }

        OpenFileDialog Open { get; set; }
        string FileText { get; set; }
        System.Windows.Forms.Timer Timer { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            progressBar.Value = 0;
            btnCancel.Enabled = btnStart.Enabled = tbxPassword.Enabled = true;
        }
    }
}
