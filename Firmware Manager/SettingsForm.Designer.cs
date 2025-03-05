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
            this.lblFirmwareFolder = new System.Windows.Forms.Label();
            this.txtFirmwareFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblWaitForFirmware = new System.Windows.Forms.Label();
            this.chkWaitForFirmwareUpdate = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFirmwareFolder
            // 
            this.lblFirmwareFolder.AutoSize = true;
            this.lblFirmwareFolder.Location = new System.Drawing.Point(12, 15);
            this.lblFirmwareFolder.Name = "lblFirmwareFolder";
            this.lblFirmwareFolder.Size = new System.Drawing.Size(95, 15);
            this.lblFirmwareFolder.TabIndex = 0;
            this.lblFirmwareFolder.Text = "Firmware Folder:";
            // 
            // txtFirmwareFolder
            // 
            this.txtFirmwareFolder.Location = new System.Drawing.Point(113, 12);
            this.txtFirmwareFolder.Name = "txtFirmwareFolder";
            this.txtFirmwareFolder.Size = new System.Drawing.Size(300, 23);
            this.txtFirmwareFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(419, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 25);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblWaitForFirmware
            // 
            this.lblWaitForFirmware.AutoSize = true;
            this.lblWaitForFirmware.Location = new System.Drawing.Point(12, 50);
            this.lblWaitForFirmware.Name = "lblWaitForFirmware";
            this.lblWaitForFirmware.Size = new System.Drawing.Size(183, 15);
            this.lblWaitForFirmware.TabIndex = 3;
            this.lblWaitForFirmware.Text = "Wait for device to update firmware:";
            // 
            // chkWaitForFirmwareUpdate
            // 
            this.chkWaitForFirmwareUpdate.AutoSize = true;
            this.chkWaitForFirmwareUpdate.Location = new System.Drawing.Point(201, 50);
            this.chkWaitForFirmwareUpdate.Name = "chkWaitForFirmwareUpdate";
            this.chkWaitForFirmwareUpdate.Size = new System.Drawing.Size(15, 14);
            this.chkWaitForFirmwareUpdate.TabIndex = 4;
            this.chkWaitForFirmwareUpdate.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(419, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 117);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkWaitForFirmwareUpdate);
            this.Controls.Add(this.lblWaitForFirmware);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFirmwareFolder);
            this.Controls.Add(this.lblFirmwareFolder);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
