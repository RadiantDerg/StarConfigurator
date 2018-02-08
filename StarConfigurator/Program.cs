using System;
using System.Text;
using System.IO;
using System.Configuration;

namespace StarConfigurator
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                string starboundDirectory = ConfigurationManager.AppSettings.Get("StarboundDirectory");
                string workshopFolder = ConfigurationManager.AppSettings.Get("WorkshopFolder");
                Console.WriteLine("[DEBUG] Listing mods...");

                listModDirectories(workshopFolder);

                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred. (Maybe StarConfigurator.exe.config is missing?) Details: \n" +
                    "\n" + ex.Message);
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
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
                var output = input.Replace(ConfigurationManager.AppSettings.Get("WorkshopFolder"), "");

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

            Console.WriteLine("[DEBUG] Building string...");

            var modList = sb.ToString();

            try
            {
                Console.WriteLine("[DEBUG] Writing file...");
                StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings.Get("StarboundDirectory") + @"\sbinit.config");
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