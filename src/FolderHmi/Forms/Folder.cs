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
    public partial class Folder : Form
    {
        OpcManager _OpcManager = new OpcManager();

        public Folder()
        {
            InitializeComponent();
            _OpcManager.DataChanged += _OpcManager_DataChanged;
        }

        private void Folder_Load(object sender, EventArgs e)
        {

        }

        private void _OpcManager_DataChanged(object sender, Objects.OpcItemEventArgs e)
        {
            switch (e.ItemHandle)
            {
                case 1:
                    textBox7.Text = "" + e.ItemValue;
                    break;
                case 2:
                    textBox8.Text = "" + e.ItemValue;
                    break;
                case 3:
                    textBox9.Text = "" + e.ItemValue;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
