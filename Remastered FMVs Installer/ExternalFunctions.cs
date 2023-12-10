using Spectre.Console;
using System.Diagnostics;

namespace Remastered_FMVs_Installer
{
    internal class ExternalFunctions
    {
        public static string UserTextColour = "blue";
        public static string UserChoice = "";

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

        public static void GameList()
        {

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
