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
            new OpcManager();
        }

        private void MainWin_Load(object sender, EventArgs e)
        {
            full_maximize(sender, e);
        }

        private void full_maximize(object sender, EventArgs e)
        {   
            // First, Hide the taskbar

            int hWnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hWnd, SW_HIDE);

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
    }
}
