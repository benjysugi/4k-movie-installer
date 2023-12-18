using Remastered_FMVs_Installer.Game_Specific_Files;
using Spectre.Console;
using System.Diagnostics;

namespace Remastered_FMVs_Installer
{
    internal class ExternalFunctions
    {
        public static string UserTextColour = "blue";
        public static string UserChoice = "";
        public static string GameFolderMoviePath = "";
        public static string BackupFolderMoviePath = "";

        public static void Options()
        {
            void chooseTextColour()
            {
                CreateMenuPrompt("Choose a text colour:", new string[] { "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta", "White", "Black" }, true, true);

                switch (UserChoice)
                {
                    case "Red":
                        UserTextColour = "red";
                        break;

                    case "Green":
                        UserTextColour = "green";
                        break;

                    case "Blue":
                        UserTextColour = "blue";
                        break;

                    case "Yellow":
                        UserTextColour = "yellow";
                        break;

                    case "Cyan":
                        UserTextColour = "cyan";
                        break;

                    case "Magenta":
                        UserTextColour = "magenta";
                        break;

                    case "White":
                        UserTextColour = "white";
                        break;

                    case "Black":
                        UserTextColour = "black";
                        break;

                    default:
                        UserTextColour = "white";
                        break;
                }
            }
        }

        public static void GameListInformation(string ExecutableName)
        {
            switch (ExecutableName)
            {
                case "Bully Scholarship Edition.exe":

                    CreateGameInfoTable(new[] { "Bully: Scholarship Edition", "Rockstar North", "21st of October 2008", "RAGE Engine" });


                    //AnsiConsole.MarkupLine("Game: Bully: Scholarship Edition (2005)");
                    break;

                default:
                    AnsiConsole.MarkupLine($"[red]Unknown game selected: {ExecutableName}.[/]\n[{ExternalFunctions.UserTextColour}]If this game's FMVs has already been remastered, but the automatic installation is not supported by this installer, most likely I have either forgotton about it, or, is currently being worked on.[/]");
                    break;
            }
        }

        public static void CheckGameExecutableForBackup(string ExecutableName)
        {
            switch (ExecutableName)
            {
                case "Bully Scholarship Edition.exe":
                    Bully.CheckForBackupFolder();
                    GameFolderMoviePath = $"{Installer_Main.GameFolderPath}\\Movies";
                    BackupFolderMoviePath = $"{Installer_Main.GameFolderPath}\\Original_FMVs_Backup\\Movies";
                    break;

                default:

                    break;
            }
        }

        public static void CopyDirectory(string sourceDir, string destDir)
        {
            DirectoryInfo dir = new(sourceDir);
            
            // Create destination directory if it doesn't exist
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            // Copy files
            foreach (FileInfo file in dir.GetFiles())
            {
                string tempPath = Path.Combine(destDir, file.Name);
                file.CopyTo(tempPath, true);
            }

            // Copy subdirectories recursively
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                string tempPath = Path.Combine(destDir, subDir.Name);
                CopyDirectory(subDir.FullName, tempPath);
            }

            Installer_Main.InstallFMVs();
        }

        public static void ContinuePrompt()
        {
            AnsiConsole.MarkupLine("[grey][italic](Press any key to continue.)[/][/]");
            Console.ReadKey();
        }

        public static void CreateMenuPrompt(string HeadingText, string[] Choices, bool CreateHeading, bool CentreHeading)
        {
            if (CreateHeading)
            {
                CreateHeader(HeadingText, CentreHeading);
            }

            string userSelection = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .PageSize(4)
            .AddChoices(Choices)
            .MoreChoicesText("[grey][italic](Move up or down to reveal more options.)[/][/]")
            .HighlightStyle(Style.WithDecoration(Decoration.RapidBlink).Foreground(Spectre.Console.Color.Blue))
            );

            UserChoice = userSelection;
        }

        public static void CreateHeader(string HeadingText, bool CentreHeading)
        {
            Rule header = new();

            if (CentreHeading) { header.Centered(); }
            else { header.LeftJustified(); }

            header.DoubleBorder();
            header.Title = HeadingText;
            header.Style = Style.Parse($"{UserTextColour}");
            AnsiConsole.Write(header);
        }

        public static void CreateGameInfoTable(string[] RowInfo)
        {
            var gameInfoTable = new Table();

            gameInfoTable.AddColumn("[green]Game Name[/]");
            gameInfoTable.Columns[0].Centered();
            gameInfoTable.AddColumn("[green]Developed by[/]");
            gameInfoTable.Columns[1].Centered();
            gameInfoTable.AddColumn("[green]Released on[/]");
            gameInfoTable.Columns[2].Centered();
            gameInfoTable.AddColumn("[green]Game Engine Used[/]");
            gameInfoTable.Columns[3].Centered();
            gameInfoTable.Border(TableBorder.Heavy);
            gameInfoTable.DoubleBorder();
            gameInfoTable.BorderStyle(new Style(Spectre.Console.Color.Blue));
            gameInfoTable.Centered();
            gameInfoTable.Title("Game information", Style.Parse($"{UserTextColour}"));

            gameInfoTable.AddRow(RowInfo);

            AnsiConsole.Write(gameInfoTable);
        }

        public static void CreateRemasteredFMVInfoTable(string[] RowInfo)
        {
            var remasteredFMVInfoTable = new Table();

            remasteredFMVInfoTable.AddColumn("[green]Remastered Video Resolution[/]");
            remasteredFMVInfoTable.Columns[0].Centered();
            remasteredFMVInfoTable.AddColumn("[green]Remastered Video FPS[/]");
            remasteredFMVInfoTable.Columns[1].Centered();
            remasteredFMVInfoTable.AddColumn("[green]Remastered Video Codec[/]");
            remasteredFMVInfoTable.Columns[2].Centered();
            remasteredFMVInfoTable.AddColumn("[green]Space Required for Installation[/]");
            remasteredFMVInfoTable.Columns[3].Centered();
            remasteredFMVInfoTable.Border(TableBorder.Heavy);
            remasteredFMVInfoTable.DoubleBorder();
            remasteredFMVInfoTable.BorderStyle(new Style(Spectre.Console.Color.Blue));
            remasteredFMVInfoTable.Centered();
            remasteredFMVInfoTable.Title("Remastered FMV Pack information", Style.Parse($"{UserTextColour}"));

            remasteredFMVInfoTable.AddRow(RowInfo);

            AnsiConsole.Write(remasteredFMVInfoTable);
        }

        public static void DisplayStorageDriveInfo()
        {
            static DriveInfo[] GetDrivesInfo()
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                return drives;
            }

            static string FormatBytes(long bytes)
            {
                const int scale = 1024; // 1024 bytes in a kilobyte etc.
                string[] orders = { "TB", "GB", "MB", "KB", "Bytes" }; // the order of storage units
                long max = (long)Math.Pow(scale, orders.Length - 1);

                foreach (string order in orders)
                {
                    if (bytes > max)
                        return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

                    max /= scale;
                }

                return "0 Bytes";
            }

            var driveTable = new Table();

            driveTable.AddColumn("[green]Drive Letter[/]");
            driveTable.Columns[0].Centered();
            driveTable.AddColumn("[green]Drive Name[/]");
            driveTable.Columns[1].Centered();
            driveTable.AddColumn("[green]Drive Type[/]");
            driveTable.Columns[2].Centered();
            driveTable.AddColumn("[green]Format Type[/]");
            driveTable.Columns[3].Centered();
            driveTable.AddColumn("[green]Free Space[/]");
            driveTable.Columns[4].Centered();
            driveTable.AddColumn("[green]Total Space[/]");
            driveTable.Columns[5].Centered();
            driveTable.Border(TableBorder.Heavy);
            driveTable.DoubleBorder();
            driveTable.BorderStyle(new Style(Spectre.Console.Color.Blue));
            driveTable.Centered();
            driveTable.Title("Storage Devices", Style.Parse($"{UserTextColour}"));

            foreach (DriveInfo drive in GetDrivesInfo())
            {
                driveTable.AddRow(drive.Name, drive.VolumeLabel, drive.DriveFormat, drive.DriveType.ToString(), FormatBytes(drive.AvailableFreeSpace), FormatBytes(drive.TotalSize));
            }

            AnsiConsole.WriteLine();
            AnsiConsole.Write(driveTable);
        }

        public static bool IsTerminalProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            return processes.Length > 0;
        }
    }
}
