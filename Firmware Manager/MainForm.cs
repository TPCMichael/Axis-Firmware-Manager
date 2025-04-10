using Firmware_Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxisFirmwareUpgradeApp
{
    public partial class MainForm : Form
    {
        private List<Device> devices = new List<Device>();

        public MainForm()
        {
            InitializeComponent();
        }

        // Button event to load a CSV file.
        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string csvPath = openFileDialog.FileName;
                    LoadDevicesFromCSV(csvPath);
                }
            }
        }

        // Load devices from CSV and create Device objects.
        private void LoadDevicesFromCSV(string csvPath)
        {
            try
            {
                devices.Clear();
                string[] lines = File.ReadAllLines(csvPath);

                // Assuming the first line is a header.
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // CSV format: Site Name, Device Name, IP Address, Port, Username, Password
                    string[] parts = line.Split(',');
                    if (parts.Length >= 6)
                    {
                        // Create device without DeviceModel (retrieved later).
                        Device device = new Device(
                            parts[0].Trim(),
                            parts[1].Trim(),
                            parts[2].Trim(),
                            parts[3].Trim(),
                            parts[4].Trim(),
                            parts[5].Trim());
                        devices.Add(device);
                    }
                }
                RefreshDeviceList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading CSV: " + ex.Message);
            }
        }

        // Refresh the DataGridView to display devices, update stats, and apply column formatting.
        private void RefreshDeviceList()
        {
            dgvDevices.DataSource = null;
            dgvDevices.DataSource = devices;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Hide sensitive columns.
            if (dgvDevices.Columns["Username"] != null)
                dgvDevices.Columns["Username"].Visible = false;
            if (dgvDevices.Columns["Password"] != null)
                dgvDevices.Columns["Password"].Visible = false;

            // Optionally move columns for Status, Firmware, Uptime, VmdStatus, and ObjectAnalyticsStatus.
            if (dgvDevices.Columns["Status"] != null)
                dgvDevices.Columns["Status"].DisplayIndex = 0;
            if (dgvDevices.Columns["Firmware"] != null)
                dgvDevices.Columns["Firmware"].DisplayIndex = 1;
            if (dgvDevices.Columns["Uptime"] != null)
                dgvDevices.Columns["Uptime"].DisplayIndex = 2;
            if (dgvDevices.Columns["VmdStatus"] != null)
                dgvDevices.Columns["VmdStatus"].DisplayIndex = 3;
            if (dgvDevices.Columns["ObjectAnalyticsStatus"] != null)
                dgvDevices.Columns["ObjectAnalyticsStatus"].DisplayIndex = 4;

            // Color-code columns.
            foreach (DataGridViewRow row in dgvDevices.Rows)
            {
                // For Status
                var statusCell = row.Cells["Status"];
                if (statusCell.Value != null)
                {
                    string status = statusCell.Value.ToString();
                    if (status.Equals("Online", StringComparison.OrdinalIgnoreCase))
                        statusCell.Style.BackColor = Color.LightGreen;
                    else if (status.Equals("Offline", StringComparison.OrdinalIgnoreCase))
                        statusCell.Style.BackColor = Color.LightCoral;
                    else
                        statusCell.Style.BackColor = Color.LightYellow;
                }

                // For VmdStatus
                var vmdCell = row.Cells["VmdStatus"];
                if (vmdCell.Value != null)
                {
                    string vmdStatus = vmdCell.Value.ToString();
                    // If restarted, use Yellow; if Running, use LightGreen; otherwise, use LightCoral.
                    if (vmdStatus.Equals("Restarted", StringComparison.OrdinalIgnoreCase))
                        vmdCell.Style.BackColor = Color.Yellow;
                    else if (vmdStatus.Equals("Running", StringComparison.OrdinalIgnoreCase))
                        vmdCell.Style.BackColor = Color.LightGreen;
                    else
                        vmdCell.Style.BackColor = Color.LightCoral;
                }

                // For ObjectAnalyticsStatus
                var oaCell = row.Cells["ObjectAnalyticsStatus"];
                if (oaCell.Value != null)
                {
                    string oaStatus = oaCell.Value.ToString();
                    if (oaStatus.Equals("Restarted", StringComparison.OrdinalIgnoreCase))
                        oaCell.Style.BackColor = Color.Yellow;
                    else if (oaStatus.Equals("Running", StringComparison.OrdinalIgnoreCase))
                        oaCell.Style.BackColor = Color.LightGreen;
                    else
                        oaCell.Style.BackColor = Color.LightCoral;
                }
            }

            int loaded = devices.Count;
            int onlineCount = devices.Count(d => d.Status == "Online");
            lblStats.Text = $"Devices Loaded: {loaded} | Online: {onlineCount}";
        }

        // Button event to remove selected device(s) from the list.
        private void btnRemoveDevice_Click(object sender, EventArgs e)
        {
            if (dgvDevices.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDevices.SelectedRows)
                {
                    if (row.DataBoundItem is Device device)
                        devices.Remove(device);
                }
                RefreshDeviceList();
            }
            else
            {
                MessageBox.Show("Please select at least one device to remove.");
            }
        }

        // Button event to check if devices are online and determine firmware update availability.
        private async void btnCheckOnline_Click(object sender, EventArgs e)
        {
            // Create and show the modal device-check progress window.
            DeviceCheckProgressForm checkForm = new DeviceCheckProgressForm();
            this.Enabled = false;  // Disable the main form so the modal takes focus.
            checkForm.Show();

            int totalDevices = devices.Count;
            int processed = 0;

            foreach (var device in devices)
            {
                checkForm.AppendLog($"Checking device: {device.DeviceName}");

                // Call the system ready endpoint to check if the device is online and get uptime.
                var (isReady, uptimeFormatted) = await device.CheckSystemReady();
                if (isReady)
                {
                    device.Status = "Online";
                    device.Uptime = uptimeFormatted;
                    device.DeviceModel = await device.GetDeviceProperty("ProdNbr");
                    device.Firmware = await device.GetDeviceProperty("Version");
                    device.TargetFirmware = GetTargetFirmwareForDevice(device);
                    checkForm.AppendLog($"{device.DeviceName} is online. Uptime: {uptimeFormatted}");
                }
                else
                {
                    device.Status = "Offline";
                    device.Uptime = "";
                    device.DeviceModel = "";
                    device.Firmware = "";
                    device.TargetFirmware = "";
                    checkForm.AppendLog($"{device.DeviceName} is offline.");
                }
                processed++;
                checkForm.UpdateProgress(processed, totalDevices, $"Checked {processed} of {totalDevices} devices.");
                RefreshDeviceList();
            }

            checkForm.AppendLog("Device online check complete.");
            checkForm.Close();
            this.Enabled = true; // Re-enable the main form.
        }

        private async void btnCheckApps_Click(object sender, EventArgs e)
        {
            // Create and show the modal application check progress window.
            ApplicationCheckProgressForm progressForm = new ApplicationCheckProgressForm();
            this.Enabled = false; // disable main form while checking
            progressForm.Show();

            int totalDevices = devices.Count;
            int processed = 0;

            foreach (var device in devices)
            {
                // Call the device's CheckApplications method.
                // It will update the device's VmdStatus and ObjectAnalyticsStatus properties.
                await device.CheckApplications((msg) =>
                {
                    progressForm.AppendLog($"Device '{device.DeviceName}': {msg}");
                    Debug.WriteLine($"Device '{device.DeviceName}': {msg}");
                });

                processed++;
                progressForm.UpdateProgress(processed, totalDevices, $"Checked {processed} of {totalDevices} devices.");
                RefreshDeviceList();
            }

            progressForm.AppendLog("Application check complete.");
            progressForm.Close();
            this.Enabled = true; // re-enable main form
            MessageBox.Show("Application check complete. See log for details.");
        }

        /// <summary>
        /// Returns a firmware file name if a firmware update is available.
        /// If no firmware file exists or if the available firmware is not newer than the device's current firmware,
        /// returns an appropriate message.
        /// </summary>
        private string GetTargetFirmwareForDevice(Device device)
        {
            if (string.IsNullOrWhiteSpace(Program.FirmwareFolder))
                return "Firmware folder not set";

            // Normalize the device model by replacing spaces with underscores.
            string normalizedModel = device.DeviceModel.Replace(" ", "_");

            // Build the search pattern using the normalized model.
            string pattern = $"{normalizedModel}_*.bin";
            var files = Directory.GetFiles(Program.FirmwareFolder, pattern);

            if (files.Length == 0)
                return "Firmware file not found";

            // Parse the device's firmware version.
            Version deviceVersion;
            if (!Version.TryParse(device.Firmware, out deviceVersion))
                deviceVersion = new Version(0, 0, 0);

            Version bestFileVersion = new Version(0, 0, 0);
            string bestFile = "";
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                // Ensure the file name starts with the normalized model and an underscore.
                if (!fileName.StartsWith(normalizedModel + "_"))
                    continue;
                // Remove the model part and underscore to get the version portion.
                string versionPart = fileName.Substring(normalizedModel.Length + 1);
                // Convert underscores to dots.
                string versionStr = versionPart.Replace('_', '.');
                if (Version.TryParse(versionStr, out Version fileVersion))
                {
                    // Look for the highest version available.
                    if (fileVersion > bestFileVersion)
                    {
                        bestFileVersion = fileVersion;
                        bestFile = Path.GetFileName(file);
                    }
                }
            }

            if (string.IsNullOrEmpty(bestFile))
                return "Firmware file not found";
            else if (bestFileVersion <= deviceVersion)
                return "Device firmware is up-to-date";
            else
                return bestFile;
        }


        // Button event to open the settings screen.
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog();
            }
        }

        // Button event to initiate firmware upgrade on online devices.
        private async void btnUpgradeFirmware_Click(object sender, EventArgs e)
        {
            var devicesToUpgrade = devices.Where(d =>
                d.Status == "Online" &&
                !string.IsNullOrEmpty(d.TargetFirmware) &&
                !d.TargetFirmware.Contains("up-to-date") &&
                !d.TargetFirmware.Contains("Firmware file not found") &&
                !d.TargetFirmware.Contains("Firmware folder not set")
            ).ToList();

            FirmwareUpgradeProgressForm progressForm = new FirmwareUpgradeProgressForm();
            progressForm.Show();

            int totalDevices = devicesToUpgrade.Count;
            int processed = 0;

            foreach (var device in devicesToUpgrade)
            {
                string firmwarePath = Path.Combine(Program.FirmwareFolder, device.TargetFirmware);
                progressForm.AppendLog($"Starting firmware upgrade for device '{device.DeviceName}' (Model: {device.DeviceModel}).");
                progressForm.AppendLog($"Uploading firmware file '{device.TargetFirmware}'...");

                // Call UploadFirmware with both progress and polling callbacks.
                string result = await device.UploadFirmware(firmwarePath,
                    (sent, total) =>
                    {
                        int percent = total > 0 ? (int)((sent / (double)total) * 100) : 0;
                        progressForm.UpdateUploadProgress(percent, device.DeviceName);
                    },
                    (pollMsg) =>
                    {
                        progressForm.AppendLog(pollMsg);
                    });

                progressForm.AppendLog($"Result for device '{device.DeviceName}': {result}");
                processed++;
                progressForm.UpdateProgress(processed, totalDevices, $"Processed {processed} of {totalDevices} devices.");
                RefreshDeviceList();
            }

            progressForm.AppendLog("Firmware upgrade process complete.");
            progressForm.Close();
            MessageBox.Show("Firmware upgrade process complete.");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }
    }
}
