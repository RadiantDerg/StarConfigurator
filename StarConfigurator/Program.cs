using System;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace StarConfigurator
{
    class Program
    {
        static string starboundDirectory;
        static string workshopFolder;
        static string dirSuccess = "0";

        static void Main(string[] args)
        {
            Console.WriteLine("Checking if Starbound is installed...");

            findFolders();

            if (dirSuccess == "1")
            {
                try
                {
                    listModDirectories(workshopFolder);
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unknown error occurred. Details: \n" +
                        "\n" + ex.Message);
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
        }

        static void findFolders()
        {
            try
            {
                var installCheck = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam\Apps\211820", "Installed", 0);
                if (installCheck != null)
                {
                    var isGameInstall = installCheck.ToString() == "1";
                    if (isGameInstall)
                    {
                        Console.WriteLine("Fetching Starbound install folder...");

                        string gamePath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 211820", "InstallLocation", null);
                        if (gamePath != null)
                        {
                            string sd = gamePath + @"\win64";
                            string wd = gamePath.Replace(@"\common\Starbound", @"\workshop\content\211820");

                            starboundDirectory = sd.ToString();
                            workshopFolder = wd.ToString();
                            dirSuccess = "1";
                        }
                        else
                        {
                            throw new Exception("Failed to find directories");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Starbound not installed.");
                    }
                }
                else
                {
                    Console.WriteLine("Starbound possibly not installed through steam.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Unable to check for installation. " + ex.Message);
            }
        }

        static void listModDirectories(string workDirectory)
        {
            string[] directories = Directory.GetDirectories(workDirectory);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("\"assetDirectories\" : [");
            sb.AppendLine("\"..\\\\assets\\\\\",");
            foreach (string directory in directories)
            {
                var input = (directory);
                var output = input.Replace(workshopFolder, "");

                sb.AppendLine("\"..\\\\..\\\\..\\\\workshop\\\\content\\\\211820\\" + output + "\\\\\",");
                Console.WriteLine("Created entry for " + output);
            }
            sb.AppendLine("\"..\\\\mods\\\\\"");
            sb.AppendLine("");
            sb.AppendLine("],");
            sb.AppendLine("");
            sb.AppendLine("\"storageDirectory\" : \"..\\\\storage\\\\\",");
            sb.AppendLine("");
            sb.AppendLine("\"defaultConfiguration\" : {");
            sb.AppendLine("\"gameServerBind\" : \"*\",");
            sb.AppendLine("\"queryServerBind\" : \"*\",");
            sb.AppendLine("\"rconServerBind\" : \"*\"");
            sb.AppendLine("}");
            sb.AppendLine("}");

            var modList = sb.ToString();

            try
            {
                Console.WriteLine("Writing file...");
                StreamWriter sw = new StreamWriter(starboundDirectory + @"\sbinit.config");
                sw.Write(modList);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save file. Details: \n" +
                    "\n" + ex.Message);
            }
        }
    }
}