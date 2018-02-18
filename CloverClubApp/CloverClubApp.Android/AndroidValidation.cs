using CloverClubApp.DeviceSpecific;
using CloverClubApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidValidation))]
namespace CloverClubApp.Droid
{
    public class AndroidValidation : IValidation
    {
        public bool ValidateEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
    }
}