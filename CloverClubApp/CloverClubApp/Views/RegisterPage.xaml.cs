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
	public partial class RegisterPage : ContentPage
	{
	    public RegisterPage()
	    {
	        InitializeComponent();

	        BindingContext = this;

	        Title = "Registrate";
	    }

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        if (EmailEntry.Text != null)
	        {
	            if (!DependencyService.Get<IValidation>().ValidateEmail(EmailEntry.Text))
	            {
	                DependencyService.Get<IMessage>().ShortAlert("El email debe estar en formato correcto");
	                return;
	            }
	        }

	        var registerData = new RegisterData
	        {
	            Email = EmailEntry.Text,
	            Password = PasswordEntry.Text,
                Name = NombreEntry.Text
	        };

	        if (registerData.Email == null || registerData.Password == null || registerData.Name == null)
	        {
	            DependencyService.Get<IMessage>().ShortAlert("Los campos no pueden tener menos de 3 caracteres");
	            return;
	        }

	        if (registerData.Email.Length < 3 || registerData.Password.Length < 3 || registerData.Name.Length < 3)
	        {
	            DependencyService.Get<IMessage>().ShortAlert("Los campos no pueden tener menos de 3 caracteres");
	            return;
	        }

	        try
	        {
	            UserService client = new UserService();
	            var user = client.RegistrarUsuario(registerData);
                if(user == null)
                    DependencyService.Get<IMessage>().ShortAlert($"El email [{registerData.Email}] ya se ha registrado");
                else
	            {
	                DependencyService.Get<IMessage>().ShortAlert($"Usuario [{registerData.Email}] registrado");
                    await Navigation.PopAsync();
	            }
	        }
	        catch (HttpRequestException ex)
	        {
	            Debug.WriteLine(ex);
	            DependencyService.Get<IMessage>()
	                .ShortAlert("No se ha podido registrar el usuario, compruebe la conexión de red");
	        }
	        catch (Exception ex)
	        {
	            Debug.WriteLine(ex);
	            DependencyService.Get<IMessage>()
	                .ShortAlert("Error en el registro");
            }
	    }
	}
}