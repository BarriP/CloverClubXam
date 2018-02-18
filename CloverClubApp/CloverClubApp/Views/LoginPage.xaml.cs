using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.DeviceSpecific;
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
	            Password = PasswordEntry.Text
	        };

	        if (loginData.Email == null || loginData.Password == null)
	        {
	            DependencyService.Get<IMessage>().ShortAlert("Los campos no pueden tener menos de 3 caracteres");
	            return;
            }	            

	        if (loginData.Email.Length < 3 || loginData.Password.Length < 3)
	        {
	            DependencyService.Get<IMessage>().ShortAlert("Los campos no pueden tener menos de 3 caracteres");
                return;
	        }

	        try
	        {
	            DateTime now = DateTime.Now.AddHours(4);
	            UserService client = new UserService();
	            string token = client.Login(loginData);

	            App.Current.Properties["tokenDate"] = now;
	            App.Current.Properties["token"] = token;
	            App.Current.Properties["loggedIn"] = true;

	            await Navigation.PopAsync();

	        }
	        catch (HttpRequestException ex)
	        {
	            Debug.WriteLine(ex);
	            DependencyService.Get<IMessage>()
	                .ShortAlert("No se ha podido registrar el usuario, compruebe la conexión de red");
	        }
	        catch (NullReferenceException)
	        {
	            DependencyService.Get<IMessage>()
	                .ShortAlert("Datos incorrectos");
            }
	        catch (Exception ex)
	        {
	            Debug.WriteLine(ex);
	            DependencyService.Get<IMessage>()
	                .ShortAlert("Login Incorrecto");
	        }
	    }
    }
}