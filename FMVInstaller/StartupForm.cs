using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FMVInstaller {
    public partial class StartupForm : Telerik.WinControls.UI.RadForm {
        public StartupForm() {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e) {
            bool checkIntegrity = false;

            if (MessageBox.Show("Would you like to check file integrity?", "QuickSFV", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                checkIntegrity = true;
            }

            if (checkIntegrity) {
                // TODO: deal with this later
            }

            var dlg = new DriveScan();

            if(dlg.ShowDialog() == DialogResult.OK) {
                using(var openFile = new OpenFileDialog()) {
                    openFile.Filter = "Portable Executable|*.exe";
                    openFile.Title = "Select the game's main executable";

                    if(openFile.ShowDialog() == DialogResult.OK) {
                        string selectedPath = openFile.FileName;

                        if(File.Exists(selectedPath)) {
                            var game = GameInfo.games[new FileInfo(selectedPath).Name];

                            string gameFolderPath = Path.GetDirectoryName(selectedPath);

                            if(gameFolderPath != null) {
                                var outputWindow = new OutputWindow();

                                outputWindow.Show();

                                GameSpecific.Bully.CheckForBackupFolder();

                                outputWindow.Write("Backing up old files...");

                                FileEx.CopyDirectory($"{gameFolderPath}\\Movies", $"{gameFolderPath}\\backup\\Movies");

                                outputWindow.Write("Completed backup.");

                                Installer.InstallFMV(game, gameFolderPath, outputWindow);
                            }
                        }
                    }
                }
            }
        }
    }
}
