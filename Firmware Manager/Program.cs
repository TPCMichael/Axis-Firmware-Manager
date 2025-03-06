using System;
using System.Net.Http;
using System.Windows.Forms;
using Firmware_Manager.Properties;

namespace AxisFirmwareUpgradeApp
{
    public static class Program
    {
        // Shared HTTP client (used elsewhere if needed)
        public static HttpClient client = new HttpClient();

        // Firmware folder stored permanently via application settings.
        public static string FirmwareFolder
        {
            get => Settings.Default.FirmwareFolder;
            set
            {
                Settings.Default.FirmwareFolder = value;
                Settings.Default.Save();
            }
        }

        public static bool WaitForFirmwareUpdate
        {
            get => Settings.Default.WaitForFirmwareUpdate;
            set
            {
                Settings.Default.WaitForFirmwareUpdate = value;
                Settings.Default.Save();
            }
        }

        public static bool WaitUntilReboot
        {
            get => Settings.Default.WaitUntilReboot;
            set
            {
                Settings.Default.WaitUntilReboot = value;
                Settings.Default.Save();
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
