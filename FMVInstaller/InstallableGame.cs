using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMVInstaller {
    public abstract class InstallableGame {
        public abstract void CheckForBackupFolder(string path);
    }
}
