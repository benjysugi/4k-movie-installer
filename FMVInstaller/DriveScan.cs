using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;  

namespace FMVInstaller {
    public partial class DriveScan : Telerik.WinControls.UI.RadForm {
        private List<DriveInfo> driveInfo = new List<DriveInfo>();
        public  List<DriveInfo> drivesAvailable = new List<DriveInfo>();

        public DriveScan() {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e) {
            var drive = drivesAvailable[radDropDownList1.SelectedIndex];
        }

        private void DriveScan_Load(object sender, EventArgs e) {
            driveInfo = DriveInfo.GetDrives().ToList();

            foreach (DriveInfo drive in driveInfo) {
                if(drive.DriveType != DriveType.CDRom) {
                    if(drive.DriveType != DriveType.NoRootDirectory) {
                        if(drive.DriveType != DriveType.Network) {
                            radDropDownList1.Items.Add($"{(drive.VolumeLabel == "" ? "Local Disk" : drive.VolumeLabel)} ({drive.Name})");
                            drivesAvailable.Add(drive);
                        }
                    }
                }
            }
        }
    }
}
