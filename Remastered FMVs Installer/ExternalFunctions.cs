using Spectre.Console;

namespace Remastered_FMVs_Installer
{
    internal class ExternalFunctions
    {
        public static string UserTextColour = null;

        public static void Options()
        {
            void chooseTextColour()
            {

            }
        }

        public static void CreateMenuPrompt(string HeadingText, string[] Choices, bool CreateHeading, bool CentreHeading)
        {
            if (CreateHeading)
            {
                CreateHeader(HeadingText, CentreHeading);
            }
        }

        public static void CreateHeader(string HeadingText, bool CentreHeading)
        {

        }
    }
}
