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
    public partial class Alarmas : Form
    {
        public Alarmas()
        {
            InitializeComponent();
        }

        private void Alarmas_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = DbManager.GetDataTable("SELECT id as Fault,tagname as Tag,date as Fecha,clearHandle as clear FROM faults WHERE active=1 ORDER BY date desc");
            //dataGridView2.DataSource = DbManager.GetDataTable("SELECT id as Fault,tagname as Tag,date as Fecha,clearHandle as clear FROM faults WHERE active=0 ORDER BY date desc LIMIT 0, 30");
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("handle", "H");
            dataGridView1.Columns.Add("Tag", "Tag");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("clear", "Clear");

            dataGridView1.Columns["Fecha"].DefaultCellStyle.Format = "MMM dd yyyy HH:mm";
            //dataGridView2.Columns["Fecha"].DefaultCellStyle.Format = "MMM dd yyyy HH:mm";
            dataGridView1.Columns["clear"].Visible = false;
            //dataGridView2.Columns["clear"].Visible = false;

            UpdateFaults();
        }

        private void UpdateFaults()
        {
            List<Tag> h = Module1.TagList.Select(x => x).Where(x => x.FormId == 2 && (bool)x.Value).ToList();

            dataGridView1.Rows.Clear();
            if (h.Count < 1) return;
            dataGridView1.Rows.Add(h.Count);
            int j = 0;
            foreach (var i in h)
            {
                dataGridView1.Rows[j++].SetValues(new object[] { i.Handle, i.Name,i.Timestamp, i.Corr });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            int index = int.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            Module1.TagList[index].Value = true;
            OpcManager.Instance.Write(index);
            DbManager.Update("faults", new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("active", "0") }
                                , new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("id", id.ToString()) });
            UpdateFaults();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DbManager.GetDataTable("SELECT id as Fault,tagname as Tag,date0 as Fecha,clearHandle as clear FROM faults WHERE active=0 AND date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ORDER BY date desc LIMIT 0, 30");
            dataGridView2.Columns["Fecha"].DefaultCellStyle.Format = "MMM dd yyyy HH:mm";
            dataGridView2.Columns["clear"].Visible = false;
        }
    }
}
