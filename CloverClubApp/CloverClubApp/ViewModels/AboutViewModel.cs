using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    public class AboutViewModel : PublicBaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}