using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    public class Protection
    {
        private static byte[] pattern = IO.ConvertToUTF16(Encoding.UTF8.GetBytes("Local State"));

        public static Return.IsProtected IsBrowserProtected(Browser browser)
        {
            string mainDllPath = Path.Combine(browser.VersionPath, browser.MainDllName);
            byte[] dllBytes = { };

            if (!File.Exists(mainDllPath))
                return Return.IsProtected.NO_DLL;

            try { dllBytes = File.ReadAllBytes(mainDllPath); }
            catch { return Return.IsProtected.CANT_READ; }

            if (IO.SearchPattern(dllBytes, pattern).Count > 0)
                return Return.IsProtected.FALSE;
            return Return.IsProtected.TRUE;
        }

        public static Return.SwitchProtect Enable(Browser browser)
        {
            string mainDllPath = Path.Combine(browser.VersionPath, browser.MainDllName);
            string localState = Path.Combine(browser.DatasPath, "Local State");
            string newPath = Path.Combine(browser.DatasPath, "ArkaProtect");
            byte[] dllBytes = { };

            Return.IsProtected isProtected = IsBrowserProtected(browser);

            if (isProtected == Return.IsProtected.TRUE)
                return Return.SwitchProtect.ALREADY;
            else if (isProtected != Return.IsProtected.FALSE)
                return Return.SwitchProtect.PROTECTION_STATE_ERR;

            //kill the browser
            Kill(browser.Name);

            //define the new pattern
            byte[] newPattern = IO.ConvertToUTF16(Encoding.UTF8.GetBytes("ArkaProtect")); // this MUST be 11 chars

            //detect if the dll exists
            if (!File.Exists(mainDllPath))
                return Return.SwitchProtect.NO_DLL;

            //detect if Local State exists
            if (!File.Exists(localState))
                return Return.SwitchProtect.NO_LOCALSTATE;

            //try to read the dll
            try { dllBytes = File.ReadAllBytes(mainDllPath); }
            catch { return Return.SwitchProtect.CANT_READ; }

            if (dllBytes == null || dllBytes.Length == 0)
                return Return.SwitchProtect.CANT_READ;

            //patch the bytes
            byte[] newDllBytes = IO.ReplacePattern(dllBytes, pattern, newPattern);

            //kill the browser
            Kill(browser.Name);

            //try to overwrite the file
            try { File.WriteAllBytes(mainDllPath, newDllBytes); }
            catch { return Return.SwitchProtect.CANT_WRITE; }

            //if ArkaProtect file somehow exists, we delete it
            if (File.Exists(newPath))
            {
                try
                {
                    File.Delete(newPath);
                }
                catch
                {
                    return Return.SwitchProtect.CANT_DEL;
                }
            }

            //finally try to move the Local State file
            try { File.Move(localState, newPath); }
            catch { return Return.SwitchProtect.CANT_MOVE; }

            return Return.SwitchProtect.SUCCESS;

        }

        public static Return.SwitchProtect Disable(Browser browser)
        {
            string mainDllPath = Path.Combine(browser.VersionPath, browser.MainDllName);
            string localState = Path.Combine(browser.DatasPath, "Local State");
            string newPath = Path.Combine(browser.DatasPath, "ArkaProtect");
            byte[] dllBytes = { };

            Return.IsProtected isProtected = IsBrowserProtected(browser);

            if (isProtected == Return.IsProtected.FALSE)
                return Return.SwitchProtect.ALREADY;
            else if (isProtected != Return.IsProtected.TRUE)
                return Return.SwitchProtect.PROTECTION_STATE_ERR;

            //kill the browser
            Kill(browser.Name);

            //define the new pattern
            byte[] newPattern = IO.ConvertToUTF16(Encoding.UTF8.GetBytes("ArkaProtect")); // this MUST be 11 chars

            //detect if the dll exists
            if (!File.Exists(mainDllPath))
                return Return.SwitchProtect.NO_DLL;

            //detect if ArkaProtect exists
            if (!File.Exists(newPath))
                return Return.SwitchProtect.NO_LOCALSTATE;

            //try to read the dll
            try { dllBytes = File.ReadAllBytes(mainDllPath); }
            catch { return Return.SwitchProtect.CANT_READ; }

            if (dllBytes == null || dllBytes.Length == 0)
                return Return.SwitchProtect.CANT_READ;

            //patch the bytes
            byte[] newDllBytes = IO.ReplacePattern(dllBytes, newPattern, pattern);

            //kill the browser
            Kill(browser.Name);

            //try to overwrite the file
            try { File.WriteAllBytes(mainDllPath, newDllBytes); }
            catch { return Return.SwitchProtect.CANT_WRITE; }

            //if Local State file somehow exists, we delete it
            if (File.Exists(localState))
            {
                try
                {
                    File.Delete(localState);
                }
                catch
                {
                    return Return.SwitchProtect.CANT_DEL;
                }
            }

            //finally try to move the ArkaProtect file
            try { File.Move(newPath, localState); }
            catch { return Return.SwitchProtect.CANT_MOVE; }

            return Return.SwitchProtect.SUCCESS;
        }

        private static void Kill(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);

            while (processes.Length > 0)
            {
                foreach (Process p in processes)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch
                    {

                    }
                }

                processes = Process.GetProcessesByName(name);
            }
        }
    }
}
