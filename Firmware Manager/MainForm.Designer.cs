namespace AxisFirmwareUpgradeApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnLoadCSV;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.Button btnCheckOnline;
        private System.Windows.Forms.Button btnUpgradeFirmware;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.Label lblStats;

        private void InitializeComponent()
        {
            btnLoadCSV = new Button();
            dgvDevices = new DataGridView();
            btnCheckOnline = new Button();
            btnUpgradeFirmware = new Button();
            lblStats = new Label();
            btnSettings = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            SuspendLayout();
            // 
            // btnLoadCSV
            // 
            btnLoadCSV.Location = new Point(12, 12);
            btnLoadCSV.Name = "btnLoadCSV";
            btnLoadCSV.Size = new Size(100, 23);
            btnLoadCSV.TabIndex = 0;
            btnLoadCSV.Text = "Load CSV";
            btnLoadCSV.UseVisualStyleBackColor = true;
            btnLoadCSV.Click += btnLoadCSV_Click;
            // 
            // dgvDevices
            // 
            dgvDevices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Location = new Point(12, 41);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.Size = new Size(1210, 300);
            dgvDevices.TabIndex = 1;
            // 
            // btnCheckOnline
            // 
            btnCheckOnline.Location = new Point(118, 12);
            btnCheckOnline.Name = "btnCheckOnline";
            btnCheckOnline.Size = new Size(100, 23);
            btnCheckOnline.TabIndex = 2;
            btnCheckOnline.Text = "Check Online";
            btnCheckOnline.UseVisualStyleBackColor = true;
            btnCheckOnline.Click += btnCheckOnline_Click;
            // 
            // btnUpgradeFirmware
            // 
            btnUpgradeFirmware.Location = new Point(224, 12);
            btnUpgradeFirmware.Name = "btnUpgradeFirmware";
            btnUpgradeFirmware.Size = new Size(120, 23);
            btnUpgradeFirmware.TabIndex = 3;
            btnUpgradeFirmware.Text = "Upgrade Firmware";
            btnUpgradeFirmware.UseVisualStyleBackColor = true;
            btnUpgradeFirmware.Click += btnUpgradeFirmware_Click;
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Location = new Point(12, 350);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(157, 15);
            lblStats.TabIndex = 4;
            lblStats.Text = "Devices Loaded: 0 | Online: 0";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(772, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(100, 23);
            btnSettings.TabIndex = 5;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1234, 381);
            Controls.Add(btnSettings);
            Controls.Add(lblStats);
            Controls.Add(btnUpgradeFirmware);
            Controls.Add(btnCheckOnline);
            Controls.Add(dgvDevices);
            Controls.Add(btnLoadCSV);
            Name = "MainForm";
            Text = "Axis Firmware Upgrade";
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Button btnSettings;
    }
}
