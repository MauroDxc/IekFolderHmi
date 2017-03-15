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
    public delegate void TagChangedDelegate(Tag tag);
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
        public event TagChangedDelegate TagChanged;

        public MainWin()
        {
            InitializeComponent();
            try
            {
                OpcManager.Instance.DataChanged += _opcManager_DataChanged;
                OpcManager.Instance.StatusMessageChanged += _opcManager_StatusMessageChanged;
                _frmConfig.FormClosing += frm_FormClosing;
                _frmFaults.FormClosed += (object sender, FormClosedEventArgs e) =>
                {
                    if (Module1.TagList.Select(x => x).Where(x => x.FormId == 2 && (bool)x.Value).ToList().Count > 0)
                    {
                        button16.BackColor = Color.Red;
                    }
                    else
                    {
                        button16.BackColor = Color.White;
                    }
                };
                //Set drives limits
                //DataTable dt = DbManager.GetDataTable("SELECT handle,value FROM limites");
                //foreach (DataRow dr in dt.Rows)
                //{
                //    int handle = int.Parse(dr["handle"].ToString());
                //    decimal value = decimal.Parse(dr["value"].ToString());
                //    Module1.TagList[handle].Value = value;
                //    OpcManager.Instance.Write(handle);
                //}
                ////Activate ethernet tags
                //dt = DbManager.GetDataTable("SELECT handle FROM tags where formid=7");
                //foreach (DataRow dr in dt.Rows)
                //{
                //    int handle = int.Parse(dr["handle"].ToString());
                //    Module1.TagList[handle].Value = true;
                //    OpcManager.Instance.Write(handle);
                //}
            }
            catch (Exception e)
            {
                connectBtn.Image = new Bitmap(FolderHmi.Properties.Resources.light_off);
                MessageBox.Show("OpcManager desconectado!", "OpcManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //toolStripStatusLabel1.Text = "OpcManager desconectado";

            }

        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                (sender as Form).Hide();
            }
        }

        private void _opcManager_StatusMessageChanged(object sender, string message)
        {
            //toolStripStatusLabel1.Text = message;
        }

        private void _opcManager_DataChanged(object sender, Objects.OpcItemEventArgs e)
        {
            Tag t = Module1.TagList.Select(x => x).Where(x => x.Handle == e.ItemHandle).FirstOrDefault();
            TagChanged?.Invoke(t);
            switch (t.FormId)
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
                    //AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, "Enabled", !(bool)e.ItemValue);

                    //if (e.ItemValue.GetType() == typeof(bool))
                    //{
                    //    AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, "ImageIndex", (bool)e.ItemValue ? 1 : 0);
                    //    AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, "Enabled", !(bool)e.ItemValue);
                    //}
                    //else
                    //{
                    //    AssignValueToControl(_frmConfig, "Z" + e.ItemHandle, null, e.ItemValue);
                    //}
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
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Text").FirstOrDefault().SetValue(ctrl, ((double)value).ToString("###0.00"));
                }
                else if (ctrl?.GetType() == typeof(NumericUpDown))
                {
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Value").FirstOrDefault().SetValue(ctrl, decimal.Parse(value.ToString()));
                    ctrl.Enabled = true;

                }
                else if (ctrl?.GetType() == typeof(PictureBox))
                {
                    ctrl.GetType().GetProperties().Select(x => x).Where(x => x.Name == "Image").FirstOrDefault().SetValue(ctrl, (bool)value ? new Bitmap(FolderHmi.Properties.Resources.check) : new Bitmap(FolderHmi.Properties.Resources.bad));

                }
                else if (ctrl?.GetType() == typeof(Button))
                {
                    Button b = ctrl as Button;
                    b.ImageIndex = (bool)value ? 1 : 0;
                    b.Enabled = !(bool)value;
                }
            }
            else
            {
                ctrl?.GetType().GetProperties().Select(x => x).Where(x => x.Name == controlProperty).FirstOrDefault().SetValue(ctrl, value);
            }
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
            PasswordDialog d = new PasswordDialog();
            d.AuthenticationFinished += (bool result) =>
                {
                    if (result)
                    {
                        _frmConfig.ShowDialog();
                    }
                };
            d.ShowDialog();
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
                        N145.Value = (decimal)values[3] > N145.Maximum ? N145.Maximum : (decimal)values[3] < N145.Minimum ? N145.Minimum : (decimal)values[3];
                        N146.Value = (decimal)values[4] > N146.Maximum ? N146.Maximum : (decimal)values[4] < N146.Minimum ? N146.Minimum : (decimal)values[4];
                        N147.Value = (decimal)values[5] > N147.Maximum ? N147.Maximum : (decimal)values[5] < N147.Minimum ? N147.Minimum : (decimal)values[5];
                        N148.Value = (decimal)values[6] > N148.Maximum ? N148.Maximum : (decimal)values[6] < N148.Minimum ? N148.Minimum : (decimal)values[6];
                        N149.Value = (decimal)values[7] > N149.Maximum ? N149.Maximum : (decimal)values[7] < N149.Minimum ? N149.Minimum : (decimal)values[7];
                        N150.Value = (decimal)values[8] > N150.Maximum ? N150.Maximum : (decimal)values[8] < N150.Minimum ? N150.Minimum : (decimal)values[8];
                        /* Escribir npos
                         N145.SendKey(Keys.Enter);
                         */

                        Tag ta, tb, tc, td;
                        Tag tan, tbn, tcn, tdn;
                        
                        ta = Module1.TagList.Select(x => x).Where(x => x.Handle == 151).FirstOrDefault();
                        tb = Module1.TagList.Select(x => x).Where(x => x.Handle == 152).FirstOrDefault();
                        tc = Module1.TagList.Select(x => x).Where(x => x.Handle == 153).FirstOrDefault();
                        td = Module1.TagList.Select(x => x).Where(x => x.Handle == 154).FirstOrDefault();
                        tan = Module1.TagList.Select(x => x).Where(x => x.Handle == 145).FirstOrDefault();
                        tbn = Module1.TagList.Select(x => x).Where(x => x.Handle == 146).FirstOrDefault();
                        tcn = Module1.TagList.Select(x => x).Where(x => x.Handle == 147).FirstOrDefault();
                        tdn = Module1.TagList.Select(x => x).Where(x => x.Handle == 148).FirstOrDefault();

                        switch ((decimal)ta.Value >= (decimal)tan.Value)
                        {
                            case true:
                                bGOb.PerformClick();
                                bGOc.PerformClick();
                                TagChanged += (Tag t) =>
                                {
                                    switch (t.Handle)
                                    {
                                        case 171:
                                            bGOa.PerformClick();
                                            break;
                                        case 170:
                                            bGOd.PerformClick();
                                            break;
                                        default:
                                            break;
                                    }
                                };
                                break;
                            case false:
                                bGOa.PerformClick();
                                bGOd.PerformClick();
                                TagChanged += (Tag t) =>
                                {
                                    switch (t.Handle)
                                    {
                                        case 172:
                                            bGOc.PerformClick();
                                            break;
                                        case 169:
                                            bGOb.PerformClick();
                                            break;
                                        default:
                                            break;
                                    }
                                };
                                break;
                            default:
                                break;
                        }

                        //bGOe.PerformClick();
                        //bGOf.PerformClick();

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

        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.OemMinus) (sender as NumericUpDown).Tag = "-1";
            //if (e.KeyCode == Keys.Oemplus) (sender as NumericUpDown).Tag = "1";
            if (e.KeyCode != Keys.Enter) return;

            //toolStripStatusLabel1.Text = "Procesando...";

            NumericUpDown nud = (NumericUpDown)sender;
            int index = int.Parse(nud.Name.Replace("N", ""));
            Module1.TagList[index].Value = nud.Value;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PasswordDialog d = new PasswordDialog();
            d.AuthenticationFinished += (bool result) =>
            {
                if (result)
                {
                    new Admin().ShowDialog();
                }
            };
            d.ShowDialog();
        }
    }
}
