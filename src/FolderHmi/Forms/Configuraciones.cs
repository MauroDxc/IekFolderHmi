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
            DataTable dt = DbManager.GetDataTable("SELECT handle,value FROM limites");
            foreach (DataRow dr in dt.Rows)
            {
                int handle = int.Parse(dr["handle"].ToString());
                decimal value = decimal.Parse(dr["value"].ToString());

                Controls.Find("L" + handle, true).FirstOrDefault().Text = value.ToString();
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            decimal value = (sender as NumericUpDown).Value;
            AppStatics.ValueList.SetValue(value, index);
            OpcManager.Instance.Write(index);
            string tag = AppStatics.TagList.GetValue(index).ToString();
            DbManager.Update("limites"
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("value", value.ToString()) }
                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("handle", index.ToString()) }
                );

        }
    }
}
