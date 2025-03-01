using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    public class Global
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}
