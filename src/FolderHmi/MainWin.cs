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

        public MainWin()
        {
            InitializeComponent();
            try
            {
                OpcManager.Instance.DataChanged += _opcManager_DataChanged;
                OpcManager.Instance.StatusMessageChanged += _opcManager_StatusMessageChanged;
            }
            catch (Exception)
            {
                connectBtn.Image = new Bitmap(FolderHmi.Properties.Resources.light_off);
                MessageBox.Show("OpcManager desconectado!", "OpcManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "OpcManager desconectado";

            }
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), CuelloA, CuelloAMov, CuelloAObj), 0);
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), CuelloB, CuelloBMov, CuelloBObj), 1);
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), CuelloC, CuelloCMov, CuelloCObj), 2);
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), CuelloD, CuelloDMov, CuelloDObj), 3);
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), RegE, RegEMov, RegEObj), 4);
            AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), RegF, RegFMov, RegFObj), 5);
            //AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), POS_BRAZO_LO, BrazoLoMov, NPOS_BRAZO_LO), 6);
            //AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), POS_BRAZO_LT, BrazoLtMov, NPOS_BRAZO_LT), 7);
            //AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), POS_BRAZO_GM, BrazoGmMov, NPOS_BRAZO_GM), 8);
            //AppStatics.CachedTags.SetValue(new Tag(new decimal(0.0), POS_BRAZO_CDR, BrazoCdrMov, NPOS_BRAZO_CDR), 9);

        }

        private void _opcManager_StatusMessageChanged(object sender, string message)
        {
            toolStripStatusLabel1.Text = message;
        }

        private void _opcManager_DataChanged(object sender, Objects.OpcItemEventArgs e)
        {
            if (e.IsFault)
            {
                string tag = (string)AppStatics.TagList.GetValue(e.ItemHandle);
                DateTime date = DateTime.Now;
                DbManager.Insert("faults", "0,'" + tag + "','" + date.ToString("yyyy-MM-dd hh:mm") + "',1");
            }
            //TextBox t = (TextBox)Controls.Find(((string)AppStatics.TagList.GetValue(e.ItemHandle)).Replace("CHANNEL1.PLC_FOLDER.", ""), true).FirstOrDefault();
            //if (t != null)
            //{
            //    t.Text = e.ItemValue + "";
            //}
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
            Form a = new Forms.Alarmas();
            a.ShowDialog();
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
            Form a = new Forms.Configuraciones();
            a.ShowDialog();
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
            Form a = new Forms.Slotter();
            a.ShowDialog();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            AppStatics.ValueList.SetValue(0.0, 1);
            //_opcManager.Write(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form a = new Forms.Ordenes();
            a.FormClosing += Ordenes_FormClosing;
            a.ShowDialog();
        }

        private void Ordenes_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < AppStatics.CachedTags.Length; i++)
            {
                Tag t = (Tag)AppStatics.CachedTags.GetValue(i);
                TextBox tx = (TextBox)(Controls.Find(t.Objetivo.Name, true)[0]);
                tx.Text = "" + t.Value;
                for (int j = 1; j < AppStatics.TagList.Length; j++)
                {
                    if (tx.Tag == AppStatics.TagList.GetValue(j))
                    {
                        AppStatics.ValueList.SetValue(t.Value, j);
                        OpcManager.Instance.Write(j);
                    }
                }
            }
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Procesando...";

            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString().Split(',')[0]);
            int action = int.Parse(btn.Tag.ToString().Split(',')[1]);
            AppStatics.ValueList.SetValue(action, index);
            OpcManager.Instance.Write(index);

            toolStripStatusLabel1.Text = "Listo";
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Procesando...";

            NumericUpDown nud = (NumericUpDown)sender;
            int index = int.Parse(nud.Tag.ToString().Split(',')[0]);
            decimal action = nud.Value;
            AppStatics.ValueList.SetValue(action, index);
            OpcManager.Instance.Write(index);

            toolStripStatusLabel1.Text = "Listo";
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
