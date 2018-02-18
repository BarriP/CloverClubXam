using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using CloverClubApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloverClubApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

		    BindingContext = this;

		    this.Title = "Entra con tu usuario";
		}

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        var loginData = new LoginData
	        {
	            Email = EmailEntry.Text,
	            Password = PasswordEntry.Text,
	        };

            DateTime now = DateTime.Now.AddHours(4);
	        UserService client = new UserService();
	        string token = client.Login(loginData);

	        App.Current.Properties["tokenDate"] = now;
	        App.Current.Properties["token"] = token;

	        await Navigation.PopAsync();
	    }
    }
}