using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace PowerPlanToggle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = new Icon(@"C:\Projects\PowerPlanToggle\GreenIcon.ico"); // College mode icon at startup
            notifyIcon1.Visible = true; // This shows the icon in the system tray
            notifyIcon1.Text = "College Plan";
            this.Hide();
            SetStartup();
            CheckStartup();

        }
        // Replace these GUIDs with your real power plan GUIDs later
        private string collegePlanGUID = "2dcf1035-f98a-4788-a7cf-645d2b5665db";
        private string ultimatePlanGUID = "14ff874d-88ca-40d3-b80d-82e3c9f8ccfc";
        private bool isCollegeMode = true;

        private void SetStartup()
        {
            string appName = "PowerPlanToggle";
            string exePath = Application.ExecutablePath;

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (reg.GetValue(appName) == null)
            {
                reg.SetValue(appName, exePath);
            }
        }
        private void CheckStartup()
        {
            string appName = "PowerPlanToggle";
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (reg.GetValue(appName) != null)
            {
                startWithWindowsToolStripMenuItem.Checked = true;
            }
            else
            {
                startWithWindowsToolStripMenuItem.Checked = false;
            }
        }



        //del

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string appName = "PowerPlanToggle";
            string exePath = Application.ExecutablePath;

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (startWithWindowsToolStripMenuItem.Checked)
            {
                reg.SetValue(appName, exePath);
            }
            else
            {
                reg.DeleteValue(appName, false);
            }

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Only toggle when left-clicked
                if (isCollegeMode)
                {
                    Process.Start("powercfg", "/setactive " + ultimatePlanGUID);
                    notifyIcon1.Text = "Ultimate Performance";
                    notifyIcon1.Icon = new Icon(@"C:\Projects\PowerPlanToggle\RedIcon.ico");
                    isCollegeMode = false;
                }
                else
                {
                    Process.Start("powercfg", "/setactive " + collegePlanGUID);
                    notifyIcon1.Text = "College Plan";
                    notifyIcon1.Icon = new Icon(@"C:\Projects\PowerPlanToggle\GreenIcon.ico");
                    isCollegeMode = true;
                }
            }
            // If right-click, do nothing (the context menu will open automatically)
        }

    }
}
