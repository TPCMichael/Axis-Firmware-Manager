namespace Firmware_Manager
{
    partial class FirmwareUpgradeProgressForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ProgressBar uploadProgressBar;
        private System.Windows.Forms.Label lblUploadProgress;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.lblUploadProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 40);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 15);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(166, 15);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Starting firmware upload...";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 140);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(360, 100);
            this.txtLog.TabIndex = 2;
            // 
            // uploadProgressBar
            // 
            this.uploadProgressBar.Location = new System.Drawing.Point(12, 80);
            this.uploadProgressBar.Name = "uploadProgressBar";
            this.uploadProgressBar.Size = new System.Drawing.Size(360, 23);
            this.uploadProgressBar.TabIndex = 3;
            // 
            // lblUploadProgress
            // 
            this.lblUploadProgress.AutoSize = true;
            this.lblUploadProgress.Location = new System.Drawing.Point(12, 110);
            this.lblUploadProgress.Name = "lblUploadProgress";
            this.lblUploadProgress.Size = new System.Drawing.Size(125, 15);
            this.lblUploadProgress.TabIndex = 4;
            this.lblUploadProgress.Text = "Upload progress: 0%";
            // 
            // FirmwareUpgradeProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 252);
            this.Controls.Add(this.lblUploadProgress);
            this.Controls.Add(this.uploadProgressBar);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FirmwareUpgradeProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Firmware Upgrade Progress";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
