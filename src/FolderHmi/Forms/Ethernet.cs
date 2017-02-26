using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderHmi.Forms
{
    public partial class Ethernet : Form
    {
        public Ethernet()
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
            int handle = int.Parse(b.Name.Replace("B", ""));
            object prevValue = Module1.ValueList.GetValue(handle);
            if (prevValue.GetType() == typeof(bool))
            {
                Module1.ValueList.SetValue(!(bool)prevValue, handle);
                OpcManager.Instance.Write(handle);
            }
            b.Enabled = true;
        }
    }
}
