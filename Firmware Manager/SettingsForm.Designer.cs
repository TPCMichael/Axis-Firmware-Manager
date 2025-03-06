namespace AxisFirmwareUpgradeApp
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFirmwareFolder;
        private System.Windows.Forms.TextBox txtFirmwareFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblWaitForFirmware;
        private System.Windows.Forms.CheckBox chkWaitForFirmwareUpdate;
        private System.Windows.Forms.Button btnSave;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblFirmwareFolder = new Label();
            txtFirmwareFolder = new TextBox();
            btnBrowse = new Button();
            lblWaitForFirmware = new Label();
            chkWaitForFirmwareUpdate = new CheckBox();
            btnSave = new Button();
            chkWaitUntilReboot = new CheckBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblFirmwareFolder
            // 
            lblFirmwareFolder.AutoSize = true;
            lblFirmwareFolder.Location = new Point(12, 15);
            lblFirmwareFolder.Name = "lblFirmwareFolder";
            lblFirmwareFolder.Size = new Size(95, 15);
            lblFirmwareFolder.TabIndex = 0;
            lblFirmwareFolder.Text = "Firmware Folder:";
            // 
            // txtFirmwareFolder
            // 
            txtFirmwareFolder.Location = new Point(113, 12);
            txtFirmwareFolder.Name = "txtFirmwareFolder";
            txtFirmwareFolder.Size = new Size(300, 23);
            txtFirmwareFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(419, 11);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 25);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblWaitForFirmware
            // 
            lblWaitForFirmware.AutoSize = true;
            lblWaitForFirmware.Location = new Point(12, 50);
            lblWaitForFirmware.Name = "lblWaitForFirmware";
            lblWaitForFirmware.Size = new Size(193, 15);
            lblWaitForFirmware.TabIndex = 3;
            lblWaitForFirmware.Text = "Wait for device to update firmware:";
            // 
            // chkWaitForFirmwareUpdate
            // 
            chkWaitForFirmwareUpdate.AutoSize = true;
            chkWaitForFirmwareUpdate.Location = new Point(201, 50);
            chkWaitForFirmwareUpdate.Name = "chkWaitForFirmwareUpdate";
            chkWaitForFirmwareUpdate.Size = new Size(15, 14);
            chkWaitForFirmwareUpdate.TabIndex = 4;
            chkWaitForFirmwareUpdate.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(419, 80);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 25);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // chkWaitUntilReboot
            // 
            chkWaitUntilReboot.AutoSize = true;
            chkWaitUntilReboot.Location = new Point(231, 81);
            chkWaitUntilReboot.Name = "chkWaitUntilReboot";
            chkWaitUntilReboot.Size = new Size(15, 14);
            chkWaitUntilReboot.TabIndex = 7;
            chkWaitUntilReboot.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 80);
            label1.Name = "label1";
            label1.Size = new Size(213, 15);
            label1.TabIndex = 6;
            label1.Text = "Wait for device to be ready after reboot";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 117);
            Controls.Add(chkWaitUntilReboot);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(chkWaitForFirmwareUpdate);
            Controls.Add(lblWaitForFirmware);
            Controls.Add(btnBrowse);
            Controls.Add(txtFirmwareFolder);
            Controls.Add(lblFirmwareFolder);
            Name = "SettingsForm";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkWaitUntilReboot;
        private Label label1;
    }
}
