using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remastered_FMVs_Installer.Game_Specific_Files
{
    internal class Bully
    {
        public static void CheckForBackupFolder()
        {
            if (Directory.Exists($"{Installer_Main.GameFolderPath}\\Original_FMVs_Backup\\Movies"))
            {
                Directory.CreateDirectory($"{Installer_Main.GameFolderPath}\\Original_FMVs_Backup\\Movies");
            }

            else
            {

            }
        }
    }
}
