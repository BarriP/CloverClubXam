using Android.App;
using Android.Widget;
using CloverClubApp.DeviceSpecific;
using CloverClubApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMessage))]
namespace CloverClubApp.Droid
{
    public class AndroidMessage : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}