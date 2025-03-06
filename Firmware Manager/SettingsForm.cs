using System;
using System.Windows.Forms;

namespace AxisFirmwareUpgradeApp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            txtFirmwareFolder.Text = Program.FirmwareFolder;
            chkWaitForFirmwareUpdate.Checked = Program.WaitForFirmwareUpdate;
            chkWaitUntilReboot.Checked = Program.WaitUntilReboot; // New checkbox
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFirmwareFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.FirmwareFolder = txtFirmwareFolder.Text.Trim();
            Program.WaitForFirmwareUpdate = chkWaitForFirmwareUpdate.Checked;
            Program.WaitUntilReboot = chkWaitUntilReboot.Checked; // Save new setting
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
