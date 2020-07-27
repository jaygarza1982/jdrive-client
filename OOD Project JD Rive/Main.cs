using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace OOD_Project_JD_Rive
{
    public partial class Main : Form
    {

        private WatchDog watchDog;

        public Main()
        {
            InitializeComponent();
            TrayMenu();
            watchDog = new WatchDog();
        }

        private void TrayMenu()
        {
            this.notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon.ContextMenuStrip.Items.Add("Exit", null, this.notifyIcon_Exit);
            this.notifyIcon.ContextMenuStrip.Items.Add("Show", null, this.notifyIcon_Show);
        }

        private void notifyIcon_Show(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon_Exit(object sender, EventArgs e)
        {
            //Exit with with no error code
            Environment.Exit(0);
        }

        private void lboxDirectories_DoubleClick(object sender, EventArgs e)
        {
            //Open the directory in file explorer when double clicking the item
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Arguments = lboxDirectories.SelectedItem.ToString();
                startInfo.FileName = "explorer.exe";

                Process.Start(startInfo);
            } catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Subscribe the updates list box to updates
            watchDog.addToListBox(lboxUpdates);
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            folderBrowse.ShowDialog();
            lboxDirectories.Items.Add(folderBrowse.SelectedPath);
            watchDog.addWatcher(folderBrowse.SelectedPath);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Do not close the window when the X button is clicked
            //We can exit through the tray icon
            e.Cancel = true;
            this.Hide();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
