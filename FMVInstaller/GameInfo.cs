using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMVInstaller {
    public class GameInfo {
        public enum EGame {
            BullyScholarshipEdition
        }

        public static Dictionary<string, EGame> games = new Dictionary<string, EGame>() {
            { "Bully Scholarship Edition.exe", EGame.BullyScholarshipEdition }
        };
    }
}
