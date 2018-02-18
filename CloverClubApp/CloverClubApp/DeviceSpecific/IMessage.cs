using System;
using System.Collections.Generic;
using System.Text;

namespace CloverClubApp.DeviceSpecific
{
    interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
