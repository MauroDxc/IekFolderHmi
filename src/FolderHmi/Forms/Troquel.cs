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
    public partial class Troquel : Form
    {
        public Troquel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = DbManager.Insert("caja (`descripcion`,`apariencia`,`flauta`,`pzxbulto`,`cvecaja`,`v1`,`v2`,`type`)",
                "'" + textBox1.Text + "'," +
                "''," +
                "1," +
                numericUpDown7.Value + "," +
                "''," +
                numericUpDown1.Value + "," +
                numericUpDown2.Value + ",1").ToString();
            MessageBox.Show("Orden <<" + textBox4.Text + ">> agregada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Troquel_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cargar c = new Cargar(table: "caja", type: 1);
            c.LoadComplete += (
                (object s, Objects.FormClosedEventArgsS f) =>
                {
                    textBox4.Text = (f.CloseArguments as Array).GetValue(0).ToString();
                    textBox1.Text = (f.CloseArguments as Array).GetValue(1).ToString();
                    numericUpDown7.Value = int.Parse((f.CloseArguments as Array).GetValue(2).ToString());
                    numericUpDown1.Value = decimal.Parse((f.CloseArguments as Array).GetValue(3).ToString());
                    numericUpDown2.Value = decimal.Parse((f.CloseArguments as Array).GetValue(4).ToString());
                }
                );
            c.ShowDialog();
        }

    }
}
