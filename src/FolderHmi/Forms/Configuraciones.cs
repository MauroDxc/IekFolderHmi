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
    public partial class Configuraciones : Form
    {
        public Configuraciones()
        {
            InitializeComponent();
        }

        private void Configuraciones_Load(object sender, EventArgs e)
        {
            OpcManager.Instance.DataChanged += _opcManager_DataChanged;
        }

        private void _opcManager_DataChanged(object sender, Objects.OpcItemEventArgs e)
        {
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Name.Replace("Z", ""));
            decimal value = (sender as NumericUpDown).Value;
            Module1.ValueList.SetValue(value, index);
            OpcManager.Instance.Write(index);
            string tag = Module1.TagList.GetValue(index).ToString();
            DbManager.Update("limites"
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("value", value.ToString()) }
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("handle", index.ToString()) }
                );

        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button)) return;
            Button b = sender as Button;
            b.ImageIndex = 2;
            //b.Enabled = false;
            int handle = int.Parse(b.Tag.ToString().Split(',')[0]);
            bool value = bool.Parse(b.Tag.ToString().Split(',')[1]);
            Module1.ValueList.SetValue(!(bool)Module1.ValueList.GetValue(handle), handle);
            OpcManager.Instance.Write(handle);
        }
    }
}
