using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using CloverClubApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloverClubApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
	    UserViewModel viewModel;

	    public UserPage()
	    {
	        InitializeComponent();

	        BindingContext = viewModel = new UserViewModel();
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        if(!viewModel.LoginStatus)
	        {
	            LoginLayout.IsVisible = true;
	            UserLayout.IsVisible = false;
	            Logout.IsEnabled = false;
                return;
	        }

            viewModel.LoadItemsCommand.Execute(null);
	        LoginLayout.IsVisible = false;
	        UserLayout.IsVisible = true;
	        Logout.IsEnabled = true;
        }

	    private async void ButtonLogin_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new LoginPage());
        }

	    private async void ButtonRegister_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new RegisterPage());
        }

	    private void Logout_OnClicked(object sender, EventArgs e)
	    {
	        viewModel.userService.Disconnect();

	        LoginLayout.IsVisible = true;
	        UserLayout.IsVisible = false;
	        Logout.IsEnabled = false;
        }

	    private async void OnDrinkSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var item = args.SelectedItem as Drink;
	        if (item == null)
	            return;

	        await Navigation.PushAsync(new CoctelDetailPage(new CoctelDetailViewModel(item)));

	        // Manually deselect item.
	        this.DrinkList.SelectedItem = null;
	    }

	    private async void OnIngredientSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (!(args.SelectedItem is SimpleIngredient item))
	            return;

	        await Navigation.PushAsync(new IngredientDetailPage(new IngredientDetailViewModel(item)));

	        // Manually deselect item.
	        this.IngredientList.SelectedItem = null;
        }
    }
}