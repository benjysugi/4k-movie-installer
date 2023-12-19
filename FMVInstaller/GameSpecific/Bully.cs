using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMVInstaller.GameSpecific {
    public class Bully : InstallableGame {
        public override void CheckForBackupFolder(string path) {
            if (Directory.Exists($"{path}\\Original_FMVs_Backup\\Movies")) {
                Directory.CreateDirectory($"{path}\\Original_FMVs_Backup\\Movies");
            }
        }
    }
}
