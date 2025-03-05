using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Firmware_Manager
{
    public partial class FirmwareUpgradeProgressForm : Form
    {
        public FirmwareUpgradeProgressForm()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int current, int total, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(current, total, message)));
                return;
            }
            int percent = total > 0 ? (int)((current / (double)total) * 100) : 0;
            progressBar.Value = Math.Min(percent, 100);
            lblProgress.Text = message;
            AppendLog(message);
        }

        public void UpdateUploadProgress(int percent, string deviceName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateUploadProgress(percent, deviceName)));
                return;
            }
            uploadProgressBar.Value = Math.Min(percent, 100);
            lblUploadProgress.Text = $"Uploading firmware to {deviceName}: {percent}%";
        }

        public void AppendLog(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AppendLog(message)));
                return;
            }
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        }
    }
}
