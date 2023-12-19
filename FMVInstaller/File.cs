using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMVInstaller {
    public class FileEx {
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories()) {
                CopyDirectory(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles()) {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        public static void CopyDirectory(string source, string target) {
            CopyDirectory(new DirectoryInfo(source), new DirectoryInfo(target));
        }
    }
}
