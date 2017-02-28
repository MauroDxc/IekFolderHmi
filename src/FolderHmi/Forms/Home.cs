using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderHmi.Forms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            int handle = int.Parse(((Control)sender).Name.Replace("B", ""));
            object value;
            object prevValue = Module1.TagList[handle].Value;
            switch (handle)
            {
                case 77:
                    value = numericUpDown1.Value;
                    break;
                case 78:
                    value = numericUpDown2.Value;
                    break;
                default:
                    new Thread(new ThreadStart(() =>
                    {
                        Module1.TagList[handle].Value = true;
                        OpcManager.Instance.Write(handle);
                        Thread.Sleep(1000);
                        Module1.TagList[handle].Value = false;
                        OpcManager.Instance.Write(handle);
                    })).Start();
                    b.Enabled = true;
                    return;
            }
            new Thread(new ThreadStart(() =>
            {
                Module1.TagList[handle].Value = value;
                OpcManager.Instance.Write(handle);
            })).Start();
            b.Enabled = true;
        }
    }
}
