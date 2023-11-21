using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Universal_BepInEx_Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please Put Your Game Path:");
                string FolderPath = Console.ReadLine();

                string zipUrl = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.22/BepInEx_x64_5.4.22.0.zip";
                string zipFileName = Path.Combine(FolderPath, "BepInEx_x64_5.4.22.0.zip");

                using (WebClient client = new WebClient())
                {
                    Console.WriteLine("Downloading BepInEx...");
                    client.DownloadFile(zipUrl, zipFileName);
                    Console.WriteLine("Download complete.");

                    Console.WriteLine("Extracting BepInEx...");
                    ZipFile.ExtractToDirectory(zipFileName, FolderPath);
                    Console.WriteLine("Extraction complete.");
                    Console.WriteLine("BepInEx installation successful.");
                    File.Delete(zipFileName);
                    Console.WriteLine("Please Put Your Game .exe name [ WITH .exe ]:");
                    string exename = Console.ReadLine();
                    Process Process = Process.Start(Path.Combine(FolderPath, exename));
                    Task.Delay(7500).Wait();
                    if (Process != null && !Process.HasExited)
                    {
                        Process.CloseMainWindow();
                        Process.WaitForExit(5000);
                        Console.WriteLine($"Cloesing {exename}");
                    }
                    Task.Delay(1000).Wait();
                    string configFolderPath = Path.Combine(FolderPath, "BepInEx", "config");
                    string configFileUrl = "https://notfishvr.dev/cdn/BepInEx.cfg";
                    string configFilePath = Path.Combine(configFolderPath, "BepInEx.cfg");
                    client.DownloadFile(configFileUrl, configFilePath);
                    Console.WriteLine("Done.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
