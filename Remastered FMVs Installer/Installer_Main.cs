using Remastered_FMVs_Installer.Game_Specific_Files;
using Spectre.Console;
using System.Diagnostics;
using System.Windows.Forms;

namespace Remastered_FMVs_Installer
{
    internal class Installer_Main
    {
        public static string GameFolderPath = "";
        public static string ExecutableName = "";

        [STAThread]
        public static void Main()
        {
            AnsiConsole.Write(new FigletText("Remastered FMVs Installer").Color(Spectre.Console.Color.Blue).Centered());
            AnsiConsole.Write(new Markup($"[bold][underline][{ExternalFunctions.UserTextColour}]Created by: NomNom[/][/][/]\n" +
                $"[{ExternalFunctions.UserTextColour}]Join the [link=https://discord.gg/KnsDNgFm2V]Discord Server.[/][/]\n" +
                $"[italic][grey]CTRL + Left Click to open the link.[/][/]").Centered());
            AnsiConsole.Write(new Markup($"[{ExternalFunctions.UserTextColour}]\nWelcome to the Remastered FMVs Installer![/]").Centered());
            ExternalFunctions.CreateHeader("Version: 0.0.1 - PreAlpha", true);

            ExternalFunctions.CreateMenuPrompt("What would you like to do?", new[] { "Install the 4K FMVs", "Uninstall the 4K FMVs", "About", "More", "Exit" }, false, false);

            switch (ExternalFunctions.UserChoice)
            {
                case "Install the 4K FMVs":
                    bool userQuickSFVPrompt = false;

                    while (userQuickSFVPrompt == false)
                    {
                        ExternalFunctions.CreateMenuPrompt("Would you like to check the file's integrity by using QuickSFV?", new[] { "Yes", "No" }, true, true);

                        switch (ExternalFunctions.UserChoice)
                        {
                            case "Yes":
                                Process.Start("Data\\QuickSFV\\QuickSFV.EXE");
                                AnsiConsole.MarkupLine($"[{ExternalFunctions.UserTextColour}]When you are done, hit any key to continue.\n\nWhen QuickSFV is opened, you must open the database file.");
                                ExternalFunctions.ContinuePrompt();
                                userQuickSFVPrompt = true;
                                break;

                            case "No":
                                InitialInstallFMVsSetup();
                                userQuickSFVPrompt = true;
                                break;
                        }
                    }
                    break;
            }
        }

        public static void InitialInstallFMVsSetup()
        {
            AnsiConsole.MarkupLine("[red]\nPLEASE NOTE; if you have external HDDs connected to your PC, Windows 10 and 11 will wake these drives up from sleep. This may cause this app to freeze until the drives have woken up.[/]\n");

            ExternalFunctions.DisplayStorageDriveInfo();

            AnsiConsole.MarkupLine($"[{ExternalFunctions.UserTextColour}]Please locate the game's executable. After you press any key to continue; a window will appear, asking you to open the game's executable file.[/]");

            ExternalFunctions.ContinuePrompt();

            AnsiConsole.WriteLine();

            using OpenFileDialog openExecutableDialog = new();
            openExecutableDialog.Filter = "Game's Exectuable (*.exe)|*.exe";
            openExecutableDialog.Title = "Locate the game's executable";

            if (openExecutableDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedExecutablePath = openExecutableDialog.FileName;

                // Get the folder path from the selected file path
                string gameFolderPath = Path.GetDirectoryName(selectedExecutablePath);

                // Get the selected file path
                string selectedExecutableName = Path.GetFileName(gameFolderPath);

                selectedExecutableName += ".exe";

                ExternalFunctions.GameListInformation(selectedExecutableName);

                AnsiConsole.WriteLine($"Selected executable: {selectedExecutableName}");
                AnsiConsole.WriteLine($"Folder path: {gameFolderPath}");

                ExecutableName = selectedExecutableName;
                GameFolderPath = gameFolderPath;
            }

            ExternalFunctions.CheckGameExecutableForBackup(ExecutableName);

            ExternalFunctions.CreateMenuPrompt("Would you like to continue to install the 4K FMVs?", new string[] { "Yes", "No" }, true, false);

            switch (ExternalFunctions.UserChoice)
            {
                case "Yes":
                    Bully.CheckForBackupFolder();
                    AnsiConsole.MarkupLine($"[{ExternalFunctions.UserTextColour}]Backing up the original FMVs...[/]");
                    ExternalFunctions.CopyDirectory($"{GameFolderPath}\\Movies", $"{GameFolderPath}\\Original_FMVs_Backup\\Movies");
                    break;

                case "No":
                    Main();
                    break;
            }
        }

        public static void InstallFMVs()
        {
            AnsiConsole.MarkupLine($"[{ExternalFunctions.UserTextColour}]Installing the 4K FMVs...[/]");
        }
    }
}