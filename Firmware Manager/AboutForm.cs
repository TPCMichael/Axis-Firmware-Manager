using System;
using System.Windows.Forms;

namespace Firmware_Manager
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblDeveloper_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use ProcessStartInfo with UseShellExecute for .NET Core/.NET 5+.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/TPCMichael",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        private void lblLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use ProcessStartInfo with UseShellExecute for .NET Core/.NET 5+.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/TPCMichael/Axis-Firmware-Manager/blob/15a3082a3a975f4adddb795ce9cad0ffde96cabe/LICENSE",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        private void lblDeviceManager_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use ProcessStartInfo with UseShellExecute for .NET Core/.NET 5+.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.axis.com/support/tools/axis-device-manager",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        private void lblDeviceManagerExtend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use ProcessStartInfo with UseShellExecute for .NET Core/.NET 5+.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.axis.com/products/axis-device-manager-extend",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        private void lblAxis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use ProcessStartInfo with UseShellExecute for .NET Core/.NET 5+.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.axis.com/en-us/about-axis",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
    }
}
