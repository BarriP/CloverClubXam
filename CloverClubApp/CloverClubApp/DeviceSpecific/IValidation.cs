using System;
using System.Collections.Generic;
using System.Text;

namespace CloverClubApp.DeviceSpecific
{
    public interface IValidation
    {
        bool ValidateEmail(string email);
    }
}
