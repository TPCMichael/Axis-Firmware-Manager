using System;
using System.Windows.Forms;

namespace AxisFirmwareUpgradeApp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            // Initialize controls from stored settings.
            txtFirmwareFolder.Text = Program.FirmwareFolder;
            chkWaitForFirmwareUpdate.Checked = Program.WaitForFirmwareUpdate;
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
