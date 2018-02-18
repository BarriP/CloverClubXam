﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloverClubApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
	    UserViewModel viewModel;
	    private bool loggedIn;

	    public UserPage()
	    {
	        InitializeComponent();

	        BindingContext = viewModel = new UserViewModel();
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        if (loggedIn)
	        {
	            LoginLayout.IsVisible = true;
	            UserLayout.IsVisible = false;
                return;
	        }

	        if(!viewModel.LoginStatus)
	        {
	            LoginLayout.IsVisible = true;
	            UserLayout.IsVisible = false;
                return;
	        }

            viewModel.LoadItemsCommand.Execute(null);
	        LoginLayout.IsVisible = false;
	        UserLayout.IsVisible = true;
	    }

	    private async void ButtonLogin_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new LoginPage());
        }

	    private async void ButtonRegister_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new RegisterPage());
        }
	}
}