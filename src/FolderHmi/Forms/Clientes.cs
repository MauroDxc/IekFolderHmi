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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DbManager().GetDataTable("SELECT * FROM cliente ORDER BY nombre");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
