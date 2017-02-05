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
    public partial class Ordenes : Form
    {
        public Ordenes()
        {
            InitializeComponent();
        }

        private void Ordenes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DbManager.GetDataTable("SELECT idreq, descripcion, apariencia, pzxbulto, v1, v2, v3, v4, v5, v6 FROM caja");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal v1 = (decimal)dataGridView1.SelectedRows[0].Cells[4].Value;
            decimal v2 = (decimal)dataGridView1.SelectedRows[0].Cells[5].Value;
            decimal v3 = (decimal)dataGridView1.SelectedRows[0].Cells[6].Value;
            decimal v4 = (decimal)dataGridView1.SelectedRows[0].Cells[7].Value;
            decimal v5 = (decimal)dataGridView1.SelectedRows[0].Cells[8].Value;
            decimal v6 = (decimal)dataGridView1.SelectedRows[0].Cells[9].Value;

            ((Tag)AppStatics.CachedTags.GetValue(0)).Value = v5 - v3;                         //Cuello A
            ((Tag)AppStatics.CachedTags.GetValue(1)).Value = v3;                              //Cuello B                             
            ((Tag)AppStatics.CachedTags.GetValue(2)).Value = v4;                              //Cuello C
            ((Tag)AppStatics.CachedTags.GetValue(3)).Value = v6 - v4;                         //Cuello D
            ((Tag)AppStatics.CachedTags.GetValue(4)).Value = 0;                               //Registro E
            ((Tag)AppStatics.CachedTags.GetValue(5)).Value = v1;                              //Registro F
            ((Tag)AppStatics.CachedTags.GetValue(6)).Value = v4;                              //Brazo A
            ((Tag)AppStatics.CachedTags.GetValue(7)).Value = v3;                              //Brazo B
            ((Tag)AppStatics.CachedTags.GetValue(8)).Value = v5 - AppStatics.ApBrazo;         //Brazo Ap
            ((Tag)AppStatics.CachedTags.GetValue(9)).Value = v1 + v2 + v3;                    //Largo Ceja

            Close();
        }
    }
}
