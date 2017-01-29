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
            dataGridView1.DataSource = new DbManager().GetDataTable("SELECT idreq, descripcion, apariencia, pzxbulto, v1, v2, v3, v4, v5, v6 FROM caja");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal v1 = (decimal)dataGridView1.SelectedRows[0].Cells[4].Value;
            decimal v2 = (decimal)dataGridView1.SelectedRows[0].Cells[5].Value;
            decimal v3 = (decimal)dataGridView1.SelectedRows[0].Cells[6].Value;
            decimal v4 = (decimal)dataGridView1.SelectedRows[0].Cells[7].Value;
            decimal v5 = (decimal)dataGridView1.SelectedRows[0].Cells[8].Value;
            decimal v6 = (decimal)dataGridView1.SelectedRows[0].Cells[9].Value;

            AppStatics.OrderValueList.SetValue(v5 - v3, 0);                         //Cuello A
            AppStatics.OrderValueList.SetValue(v3, 1);                              //Cuello B                             
            AppStatics.OrderValueList.SetValue(v4, 2);                              //Cuello C
            AppStatics.OrderValueList.SetValue(v6 - v4, 3);                         //Cuello D
            AppStatics.OrderValueList.SetValue(0, 4);                               //Registro E
            AppStatics.OrderValueList.SetValue(v1, 5);                              //Registro F
            AppStatics.OrderValueList.SetValue(v4, 6);                              //Brazo A
            AppStatics.OrderValueList.SetValue(v3, 7);                              //Brazo B
            AppStatics.OrderValueList.SetValue(v5 - AppStatics.ApBrazo, 8);         //Brazo Ap
            AppStatics.OrderValueList.SetValue(v1 + v2 + v3, 9);                    //Largo Ceja

            Close();
        }
    }
}
