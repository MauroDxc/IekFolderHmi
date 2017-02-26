using FolderHmi.Objects;
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
    public delegate void FormClosed2(object sender, FormClosedEventArgsS e);
    public partial class Cargar : Form
    {
        public event FormClosed2 LoadComplete;
        private string _loadQquery;
        private int _type;
        public Cargar()
        {
            InitializeComponent();
        }

        public Cargar(string table, int type) : this()
        {
            _type = type;
            switch (table)
            {
                case "caja":
                    _loadQquery = "SELECT idreq as 'Núm Req', descripcion as 'Descripción', apariencia, pzxbulto, v1, v2, v3, v4, v5, v6 FROM caja WHERE type=" + (type < 0 ? "type" : "" + type);
                    break;
                default:
                    break;
            }
        }

        private void Ordenes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DbManager.GetDataTable(_loadQquery);
            foreach (DataGridViewTextBoxColumn col in dataGridView1.Columns)
            {
                col.Visible = col.Index < 2;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal v1 = 0;
            decimal v2 = 0;
            decimal v3 = 0;
            decimal v4 = 0;
            decimal v5 = 0;
            decimal v6 = 0;
            int id = 0;
            string desc = "";
            int pz = 0;

            id = int.Parse(dataGridView1.SelectedRows[0].Cells[0]?.Value.ToString());
            pz = int.Parse(dataGridView1.SelectedRows[0].Cells[3]?.Value.ToString());
            desc = dataGridView1.SelectedRows[0].Cells[1]?.Value.ToString();
            v1 = (decimal)dataGridView1.SelectedRows[0].Cells[4]?.Value;
            v2 = (decimal)dataGridView1.SelectedRows[0].Cells[5]?.Value;
            v3 = (decimal)dataGridView1.SelectedRows[0].Cells[6]?.Value;
            v4 = (decimal)dataGridView1.SelectedRows[0].Cells[7]?.Value;
            v5 = (decimal)dataGridView1.SelectedRows[0].Cells[8]?.Value;
            v6 = (decimal)dataGridView1.SelectedRows[0].Cells[9]?.Value;

            //((Tag)AppStatics.CachedTags.GetValue(0)).Value = v5 - v3;                         //Cuello A
            //((Tag)AppStatics.CachedTags.GetValue(1)).Value = v3;                              //Cuello B                             
            //((Tag)AppStatics.CachedTags.GetValue(2)).Value = v4;                              //Cuello C
            //((Tag)AppStatics.CachedTags.GetValue(3)).Value = v6 - v4;                         //Cuello D
            //((Tag)AppStatics.CachedTags.GetValue(4)).Value = 0;                               //Registro E
            //((Tag)AppStatics.CachedTags.GetValue(5)).Value = v1;                              //Registro F
            //((Tag)AppStatics.CachedTags.GetValue(6)).Value = v4;                              //Brazo A
            //((Tag)AppStatics.CachedTags.GetValue(7)).Value = v3;                              //Brazo B
            //((Tag)AppStatics.CachedTags.GetValue(8)).Value = v5 - AppStatics.ApBrazo;         //Brazo Ap
            //((Tag)AppStatics.CachedTags.GetValue(9)).Value = v1 + v2 + v3;                    //Largo Ceja
            FormClosedEventArgsS closeArgs = new FormClosedEventArgsS(CloseReason.UserClosing, new object[] { id, desc, pz, v1, v2, v3, v4, v5, v6 });
            LoadComplete(this, closeArgs);
            Close();
        }
    }
}
