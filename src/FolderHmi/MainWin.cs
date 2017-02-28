using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Management;
using MySql;
using MySql.Data;
using OPCAutomation;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Resources;
using FolderHmi.Forms;

namespace FolderHmi
{
    public partial class MainWin : Form
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpszClassName, string lpszWindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        String PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private Form _frmConfig = new Forms.Configuraciones();
        private Form _frmEthernet = new Forms.Ethernet();
        private Form _frmHome = new Forms.Home();
        private Form _frmFaults = new Forms.Alarmas();

        public MainWin()
        {
            InitializeComponent();
            try
            {
                OpcManager.Instance.DataChanged += _opcManager_DataChanged;
                OpcManager.Instance.StatusMessageChanged += _opcManager_StatusMessageChanged;
                //Set drives limits
                DataTable dt = DbManager.GetDataTable("SELECT handle,value FROM limites");
                foreach (DataRow dr in dt.Rows)
                {
                    int handle = int.Parse(dr["handle"].ToString());
                    decimal value = decimal.Parse(dr["value"].ToString());
                    Module1.TagList[handle].Value = value;
                    OpcManager.Instance.Write(handle);
                }
                //Activate ethernet tags
                dt = DbManager.GetDataTable("SELECT handle FROM tags where formid=7");
                foreach (DataRow dr in dt.Rows)
                {
                    int handle = int.Parse(dr["handle"].ToString());
                    Module1.TagList[handle].Value = true;
                    OpcManager.Instance.Write(handle);
                }
            }
            catch (Exception e)
            {
                connectBtn.Image = new Bitmap(FolderHmi.Properties.Resources.light_off);
                MessageBox.Show("OpcManager desconectado!", "OpcManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //toolStripStatusLabel1.Text = "OpcManager desconectado";

            }

        }

        private void _opcManager_StatusMessageChanged(object sender, string message)
        {
            //toolStripStatusLabel1.Text = message;
        }

        private void _opcManager_DataChanged(object sender, Objects.OpcItemEventArgs e)
        {
            int formid = Module1.TagList.Select(x => x).Where(x => x.Handle == e.ItemHandle).FirstOrDefault().FormId;
            switch (formid)
            {
                case 1:
                    AssignValueToControl(this, "Z" + e.ItemHandle, null, e.ItemValue);
                    AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, null, e.ItemValue);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, null, e.ItemValue);
                    break;
                case 7:
                    AssignValueToControl(_frmEthernet, "B" + e.ItemHandle, "BackColor", (bool)e.ItemValue ? Color.Green : Color.Red);
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 12: //11
                    switch (e.ItemHandle)
                    {
                        case 34:
                            AssignValueToControl(_frmHome, "B33", "ImageIndex", 2);
                            break;
                        case 1:
                            AssignValueToControl(_frmHome, "B33", "ImageIndex", 0);
                            break;
                        case 119:
                            AssignValueToControl(_frmHome, "B117", "ImageIndex", 2);
                            break;
                        case 126:
                            AssignValueToControl(_frmHome, "B117", "ImageIndex", 0);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (e.IsFault)
            {
                button16.BackColor = Color.Red;
                string tag = (string)Module1.TagList[e.ItemHandle].Name;
                DateTime date = DateTime.Now;
                DbManager.Insert("faults", "0,'" + tag + "','" + date.ToString("yyyy-MM-dd hh:mm") + "',1,(SELECT h FROM (SELECT corr as h FROM tags WHERE handle=" + e.ItemHandle + ") AS G)");
            }
        }

        private void AssignValueToControl(Form form, string controlName, string controlProperty, object value)
        {
            if (value == null) return;
            Control ctrl = form.Controls.Find(controlName, true).FirstOrDefault();
            if (ctrl == null) return;
            if (string.IsNullOrEmpty(controlProperty))
            {
                if (ctrl?.GetType() == typeof(Label))
                {
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Text").FirstOrDefault().SetValue(ctrl, value.ToString());
                }
                else if (ctrl?.GetType() == typeof(NumericUpDown))
                {
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Value").FirstOrDefault().SetValue(ctrl, decimal.Parse(value.ToString()));
                }
                else if (ctrl?.GetType() == typeof(PictureBox))
                {
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Image").FirstOrDefault().SetValue(ctrl, (bool)value ? new Bitmap(FolderHmi.Properties.Resources.check) : new Bitmap(FolderHmi.Properties.Resources.bad));
                }
                else if (ctrl?.GetType() == typeof(Button))
                {
                    Button b = ctrl as Button;
                    b.ImageIndex = (bool)value ? 1 : 0;
                }
            }
            else
            {
                ctrl?.GetType().GetProperties().Select(x => x).Where(x => x.Name == controlProperty).FirstOrDefault().SetValue(ctrl, value);
            }
            ctrl.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Forms.Clientes().ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Forms.Folder().ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        }

        private void MainWin_Load(object sender, EventArgs e)
        {
            full_maximize(sender, e);
        }

        private void full_maximize(object sender, EventArgs e)
        {
            // First, Hide the taskbar

            int hWnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hWnd, SW_SHOW);

            // Then, format and size the window. 
            // Important: Borderstyle -must- be first, 
            // if placed after the sizing functions, 
            // it'll strangely firm up the taskbar distance.

            FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;

            //        The following is optional, but worth knowing.

            //        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //        this.TopMost = true;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            _frmFaults.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
            _frmConfig.ShowDialog();
        }

        private void button34_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form a = new Forms.Slotter();
            a.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Form a = new Forms.Troquel();
            a.ShowDialog();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            Module1.TagList[1].Value = 0.0;
            //_opcManager.Write(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Cargar a = new Forms.Cargar("caja", -1);
            a.LoadComplete += (
                (object s, Objects.FormClosedEventArgsS f) =>
                    {
                        object[] values = f.CloseArguments as object[];
                        textBox1.Text = values[0].ToString().PadLeft(3, '0');
                        Z145.Value = (decimal)values[3] > Z145.Maximum ? Z145.Maximum : (decimal)values[3] < Z145.Minimum ? Z145.Minimum : (decimal)values[3];
                        Z146.Value = (decimal)values[4] > Z146.Maximum ? Z146.Maximum : (decimal)values[4] < Z146.Minimum ? Z146.Minimum : (decimal)values[4];
                        Z147.Value = (decimal)values[5] > Z147.Maximum ? Z147.Maximum : (decimal)values[5] < Z147.Minimum ? Z147.Minimum : (decimal)values[5];
                        Z148.Value = (decimal)values[6] > Z148.Maximum ? Z148.Maximum : (decimal)values[6] < Z148.Minimum ? Z148.Minimum : (decimal)values[6];
                        Z149.Value = (decimal)values[7] > Z149.Maximum ? Z149.Maximum : (decimal)values[7] < Z149.Minimum ? Z149.Minimum : (decimal)values[7];
                        Z150.Value = (decimal)values[8] > Z150.Maximum ? Z150.Maximum : (decimal)values[8] < Z150.Minimum ? Z150.Minimum : (decimal)values[8];

                        bGOa.PerformClick();
                        bGOb.PerformClick();
                        bGOc.PerformClick();
                        bGOd.PerformClick();
                        bGOe.PerformClick();
                        bGOf.PerformClick();

                    });
            a.ShowDialog();
        }


        private void actionButton_Click(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Procesando...";

            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString().Split(',')[0]);
            int action = int.Parse(btn.Tag.ToString().Split(',')[1]);
            Module1.TagList[index].Value = action;
            OpcManager.Instance.Write(index);

            //toolStripStatusLabel1.Text = "Listo";
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Procesando...";

            NumericUpDown nud = (NumericUpDown)sender;
            int index = int.Parse(nud.Name.Replace("Z", ""));
            decimal action = nud.Value;
            Module1.TagList[index].Value = action;
            OpcManager.Instance.Write(index);

            //toolStripStatusLabel1.Text = "Listo";
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _frmEthernet.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _frmHome.ShowDialog();
        }
    }
}
