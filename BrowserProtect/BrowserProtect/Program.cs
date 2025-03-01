using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Basic implementation for Chrome
            Dictionary<string, Browser> browsers = Browser.InitBrowsers();
            Browser chrome = browsers["chrome"];
            Return.SwitchProtect result = Protection.Disable(chrome);
            Console.WriteLine(result);
            Console.ReadKey();

        }
    }
}
