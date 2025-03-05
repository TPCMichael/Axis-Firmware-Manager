namespace Firmware_Manager
{
    partial class AboutForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtAbout;
        private System.Windows.Forms.Button btnClose;

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
            txtAbout = new TextBox();
            btnClose = new Button();
            lblDeveloper = new LinkLabel();
            lblAxis = new LinkLabel();
            label1 = new Label();
            lblDeviceManager = new LinkLabel();
            lblDeviceManagerExtend = new LinkLabel();
            lblLicense = new LinkLabel();
            SuspendLayout();
            // 
            // txtAbout
            // 
            txtAbout.Location = new Point(12, 12);
            txtAbout.Multiline = true;
            txtAbout.Name = "txtAbout";
            txtAbout.ReadOnly = true;
            txtAbout.ScrollBars = ScrollBars.Both;
            txtAbout.Size = new Size(360, 200);
            txtAbout.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(297, 218);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblDeveloper
            // 
            lblDeveloper.AutoSize = true;
            lblDeveloper.LinkArea = new LinkArea(13, 24);
            lblDeveloper.Location = new Point(28, 28);
            lblDeveloper.Name = "lblDeveloper";
            lblDeveloper.Size = new Size(150, 21);
            lblDeveloper.TabIndex = 2;
            lblDeveloper.TabStop = true;
            lblDeveloper.Text = "Developed by Michael Cox";
            lblDeveloper.UseCompatibleTextRendering = true;
            lblDeveloper.LinkClicked += lblDeveloper_Click;
            // 
            // lblAxis
            // 
            lblAxis.LinkArea = new LinkArea(57, 19);
            lblAxis.Location = new Point(28, 101);
            lblAxis.Name = "lblAxis";
            lblAxis.Size = new Size(324, 52);
            lblAxis.TabIndex = 3;
            lblAxis.TabStop = true;
            lblAxis.Text = "This software is not officially endorsed or supported by Axis Communications. Use at your own risk or explore officially endorsed Axis solutions below.";
            lblAxis.UseCompatibleTextRendering = true;
            lblAxis.LinkClicked += lblAxis_LinkClicked;
            // 
            // label1
            // 
            label1.Location = new Point(28, 58);
            label1.Name = "label1";
            label1.Size = new Size(300, 33);
            label1.TabIndex = 4;
            label1.Text = "This software is written exclusively for Axis devices with firmware 10.0 or higher.";
            // 
            // lblDeviceManager
            // 
            lblDeviceManager.AutoSize = true;
            lblDeviceManager.LinkArea = new LinkArea(0, 19);
            lblDeviceManager.Location = new Point(28, 157);
            lblDeviceManager.Name = "lblDeviceManager";
            lblDeviceManager.Size = new Size(215, 21);
            lblDeviceManager.TabIndex = 5;
            lblDeviceManager.TabStop = true;
            lblDeviceManager.Text = "Axis Device Manager (for LAN devices)";
            lblDeviceManager.UseCompatibleTextRendering = true;
            lblDeviceManager.LinkClicked += lblDeviceManager_LinkClicked;
            // 
            // lblDeviceManagerExtend
            // 
            lblDeviceManagerExtend.AutoSize = true;
            lblDeviceManagerExtend.LinkArea = new LinkArea(0, 26);
            lblDeviceManagerExtend.Location = new Point(28, 178);
            lblDeviceManagerExtend.Name = "lblDeviceManagerExtend";
            lblDeviceManagerExtend.Size = new Size(271, 21);
            lblDeviceManagerExtend.TabIndex = 6;
            lblDeviceManagerExtend.TabStop = true;
            lblDeviceManagerExtend.Text = "Axis Device Manager Extend (for remote devices)";
            lblDeviceManagerExtend.UseCompatibleTextRendering = true;
            lblDeviceManagerExtend.LinkClicked += lblDeviceManagerExtend_LinkClicked;
            // 
            // lblLicense
            // 
            lblLicense.AutoSize = true;
            lblLicense.Location = new Point(210, 28);
            lblLicense.Name = "lblLicense";
            lblLicense.Size = new Size(46, 15);
            lblLicense.TabIndex = 7;
            lblLicense.TabStop = true;
            lblLicense.Text = "License";
            lblLicense.LinkClicked += lblLicense_LinkClicked;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 253);
            Controls.Add(lblLicense);
            Controls.Add(lblDeviceManagerExtend);
            Controls.Add(lblDeviceManager);
            Controls.Add(label1);
            Controls.Add(lblAxis);
            Controls.Add(lblDeveloper);
            Controls.Add(btnClose);
            Controls.Add(txtAbout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel lblDeveloper;
        private LinkLabel lblAxis;
        private Label label1;
        private LinkLabel lblDeviceManager;
        private LinkLabel lblDeviceManagerExtend;
        private LinkLabel lblLicense;
    }
}
