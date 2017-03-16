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
    public partial class Configuraciones : Form
    {
        public Configuraciones()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Configuraciones_Load(object sender, EventArgs e)
        {
            List<Tag> h = Module1.TagList.Select(x => x).Where(x => x.FormId == 6).ToList();
            foreach (var i in h)
            {
                 (this.Controls.Find("Z" + i.Handle, true).FirstOrDefault() as NumericUpDown).Value = (decimal)i.Value;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button)) return;
            Button b = sender as Button;
            if (b.ImageIndex == 1) return;
            b.ImageIndex = 2;
            //b.Enabled = false;
            int handle = int.Parse(b.Tag.ToString().Split(',')[0]);
            bool value = bool.Parse(b.Tag.ToString().Split(',')[1]);
            Module1.TagList[handle].Value = !(bool)Module1.TagList[handle].Value;
            OpcManager.Instance.Write(handle);
        }

        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            ((Control)sender).Enabled = false;
            int index = int.Parse((sender as Control).Name.Replace("Z", ""));
            decimal value = (sender as NumericUpDown).Value;
            Module1.TagList[index].Value = value;
            OpcManager.Instance.Write(index);
            string tag = Module1.TagList[index].Name.ToString();
            DbManager.Update("limites"
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("value", value.ToString()) }
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("handle", index.ToString()) }
                );
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(8000);
                NumericUpDown n = sender as NumericUpDown;
                if (!n.Enabled)
                {
                    n.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    n.Enabled = true;
                    n.BackColor = Color.White;
                }
            })).Start();
        }
    }
}
