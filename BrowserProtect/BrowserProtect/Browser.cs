using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    //Chrominium based browsers
    public class Browser
    {
        private static string[] ExePaths = {
            //edge
            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
            @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",

            //chrome
            @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
            @"C:\Program Files\Google\Chrome\Application\chrome.exe",

            //brave
            @"C:\Program Files (x86)\BraveSoftware\Brave-Browser\Application\brave.exe",
            @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",

            //opera gx
            Path.Combine(Global.LocalAppData, @"Programs\Opera GX\opera.exe")
        };

        public string Name { get; set; }
        public string InstallPath { get; set; }
        public string VersionPath { get; set; }
        public string DatasPath { get; set; }
        public string MainDllName { get; set; }

        //init every browsers
        public static Dictionary<string, Browser> InitBrowsers()
        {
            Dictionary<string, Browser> browsers = new Dictionary<string, Browser>();
            Browser browser = new Browser();

            //add Edge
            Browser edge = new Browser
            {
                Name = "msedge",
                MainDllName = "msedge.dll",
                DatasPath = Path.Combine(Global.LocalAppData, @"Microsoft\Edge\User Data")
            };
            if (edge.Fill())
                browsers.Add(edge.Name, edge);


            // add Chrome
            Browser chrome = new Browser
            {
                Name = "chrome",
                MainDllName = "chrome.dll",
                DatasPath = Path.Combine(Global.LocalAppData, @"Google\Chrome\User Data")
            };
            if (chrome.Fill())
                browsers.Add(chrome.Name, chrome);


            // add Brave
            Browser brave = new Browser
            {
                Name = "brave",
                MainDllName = "chrome.dll",
                DatasPath = Path.Combine(Global.LocalAppData, @"BraveSoftware\Brave-Browser\User Data")
            };
            if (brave.Fill())
                browsers.Add(brave.Name, brave);

            // add Opera GX
            Browser opera = new Browser
            {
                Name = "opera",
                MainDllName = "opera_browser.dll",
                DatasPath = Path.Combine(Global.AppData, @"Opera Software\Opera GX Stable")
            };
            if (opera.Fill())
                browsers.Add(opera.Name, opera);


            return browsers;
        }


        //function to fill InstallPath & VersionPath
        public bool Fill()
        {
            if (Name == null)
                return false;

            //get install path
            InstallPath = GetInstallPath(Name);
            if (InstallPath == null)
                return false;

            //get the version path
            VersionPath = GetVersionPath(InstallPath);
            if (VersionPath == null)
                return false;

            return true;
        }

        private string GetInstallPath(string browser)
        {
            foreach (string path in ExePaths)
            {
                if (Path.GetFileNameWithoutExtension(path) == Name && File.Exists(path))
                {
                    return Path.GetDirectoryName(path);
                }
            }

            return null;
        }

        private string GetVersionPath(string directory)
        {
            foreach (string dir in Directory.GetDirectories(directory))
            {
                string dirName = Path.GetFileName(dir);
                if (char.IsDigit(dirName[0]) && dirName.Contains(".")) //basic filter to get the version directory
                {
                    return dir;
                }
            }

            return null;
        }
    }
}
