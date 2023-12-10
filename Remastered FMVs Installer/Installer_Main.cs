using Spectre.Console;
using System.Diagnostics;
using System.Windows.Forms;

namespace Remastered_FMVs_Installer
{
    internal class Installer_Main
    {
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
                                userQuickSFVPrompt = true;
                                break;

                            case "No":
                                InstallFMVs();
                                userQuickSFVPrompt = true;
                                break;
                        }
                    }
                    break;
            }
        }

        static void InstallFMVs()
        {
            void GetGameInformation(string ExecutableName)
            {
                switch (ExecutableName)
                {
                    case "Bully Scholarship Edition.exe":

                        ExternalFunctions.CreateGameInfoTable(new[] { "Bully: Scholarship Edition", "Rockstar North", "21st of October 2008", "RAGE Engine" });

                        AnsiConsole.MarkupLine("Game: Bully: Scholarship Edition (2005)");
                        break;

                    default:
                        AnsiConsole.MarkupLine($"[red]Unknown game selected: {ExecutableName}.[/]\n[{ExternalFunctions.UserTextColour}]If this game's FMVs has already been remastered, but the automatic installation is not supported by this installer, most likely I have either forgotton, or, is currently being worked on.[/]");
                        break;
                }
            }

            string processName = "";
            string folderPath = "";

            AnsiConsole.MarkupLine("[red]PLEASE NOTE; if you have external HDDs connected to your PC, Windows 10 and 11 will wake these drives up from sleep. This may cause this app to freeze until the drives have woken up.[/]\n");

            ExternalFunctions.DisplayStorageDriveInfo();

            AnsiConsole.MarkupLine($"[{ExternalFunctions.UserTextColour}]Please locate the game's executable. After you press any key to continue; a window will appear, asking you to open the game's executable file.[/]");

            ExternalFunctions.ContinuePrompt();

            AnsiConsole.WriteLine();

            using OpenFileDialog openExecutableDialog = new OpenFileDialog();
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

                GetGameInformation(selectedExecutableName);

                AnsiConsole.WriteLine($"Selected executable: {selectedExecutableName}");
                AnsiConsole.WriteLine($"Folder path: {gameFolderPath}");
            }
        }
    }
}