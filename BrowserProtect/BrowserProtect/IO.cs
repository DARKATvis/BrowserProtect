using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    public class IO
    {
        public static List<int> SearchPattern(byte[] bytes, byte[] pattern)
        {
            List<int> offsets = new List<int>();

            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == pattern[0]) //check if the byte is equal to the first byte of the pattern
                {
                    bool found = true;
                    for (int j = 0; j < pattern.Length; j++) //iterate trough the pattern bytes
                    {
                        if (i + j > bytes.Length) //avoid out of range
                        {
                            found = false;
                            break;
                        }

                        if (bytes[i + j] != pattern[j]) //check if bytes are not same
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        offsets.Add(i);
                    }

                }
            }

            return offsets;
        }

        public static byte[] ReplacePattern(byte[] bytes, byte[] pattern, byte[] newPattern)
        {
            List<int> offsets = SearchPattern(bytes, pattern);

            foreach (int offset in offsets) //iterate trough every offsets to replace them
            {
                for (int j = 0; j < newPattern.Length; j++)
                {
                    bytes[offset + j] = newPattern[j];
                }
            }

            return bytes;

        }

        public static byte[] ConvertToUTF16(byte[] bytes)
        {
            List<byte> result = new List<byte>();

            foreach (byte b in bytes)
            {
                result.Add(b);
                result.Add(0);
            }
            result.RemoveAt(result.Count - 1); //remove the last 0
            return result.ToArray();
        }
    }
}
