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
	public partial class RegisterPage : ContentPage
	{
	    public RegisterPage()
	    {
	        InitializeComponent();

	        BindingContext = this;

	        Title = "Registrate";
	    }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        var registerData = new RegisterData
	        {
	            Email = EmailEntry.Text,
	            Password = PasswordEntry.Text,
                Name = NombreEntry.Text
	        };

            UserService client = new UserService();
	        var user = client.RegistrarUsuario(registerData);
	    }
	}
}