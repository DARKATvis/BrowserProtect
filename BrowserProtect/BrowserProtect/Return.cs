using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserProtect
{
    public class Return
    {
        public enum IsProtected
        {
            TRUE,
            FALSE,
            NO_DLL,
            CANT_READ,
            UNKNOWN_ERR
        }

        public enum SwitchProtect
        {
            SUCCESS,
            NO_DLL,
            NO_LOCALSTATE,
            PROTECTION_STATE_ERR,
            ALREADY,
            CANT_CLOSE,
            CANT_READ,
            CANT_WRITE,
            CANT_MOVE,
            CANT_DEL,
            UNKNOWN_ERR
        }
    }
}
