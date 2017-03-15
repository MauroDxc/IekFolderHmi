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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            DataTable dt = DbManager.GetDataTable("SELECT handle,valor FROM pulsos");
            foreach (DataRow dr in dt.Rows)
            {
                Controls.Find("Z" + dr["handle"], true).FirstOrDefault().Text = "" + dr["valor"];
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            string h = (sender as TextBox).Name.Replace("Z", "");
            string v = (sender as TextBox).Text;
            if (string.IsNullOrEmpty(v)) return;
            DbManager.Update("pulsos", new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("valor", "" + v) }, new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("handle", "" + h) });
            Module1.TagList[int.Parse(h)].Value = int.Parse(v);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = (e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5
                && e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.D0 && e.KeyCode != Keys.Back);
        }
    }
}
