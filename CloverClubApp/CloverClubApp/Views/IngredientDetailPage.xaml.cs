using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using CloverClubApp.Models;
using CloverClubApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloverClubApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientDetailPage : ContentPage
	{
	    private IngredientDetailViewModel viewModel;
	    private bool initialized;

	    public IngredientDetailPage(IngredientDetailViewModel viewModel)
	    {
	        InitializeComponent();

	        BindingContext = this.viewModel = viewModel;

	        AddFav.Command = new Command(AddFav_OnClicked);
	        RemoveFav.Command = new Command(RemoveFav_OnClicked);
        }

	    public IngredientDetailPage()
	    {
	        InitializeComponent();

	        var ing = new SimpleIngredient();

	        viewModel = new IngredientDetailViewModel(ing);
	        BindingContext = viewModel;
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if (logged != null && (bool)logged)
	        {
	            App.Current.Properties.TryGetValue("ingredientesFav", out object ingredientes);
	            if (ingredientes is IEnumerable<string> list)
	            {
	                this.AddFav.IsVisible = !list.Contains(viewModel.SimpleIngredient.Name);
	                this.RemoveFav.IsVisible = !this.AddFav.IsVisible;
	            }
	        }

            if (initialized) return;

	        viewModel.LoadIngredientCommand.Execute(null);
	        initialized = true;
	    }

	    private async void AddFav_OnClicked()
	    {
	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if ((bool)logged)
	        {
	            MessagingCenter.Send(this, "AddIngredienteFav", viewModel.SimpleIngredient.Name);
	            App.Current.Properties.TryGetValue("ingrdientesFav", out object ingredientes);
	            var list = ingredientes as List<string> ?? new List<string>();
	            list.Add(viewModel.SimpleIngredient.Name);
	            await this.Navigation.PopAsync();
	        }

	    }

	    private async void RemoveFav_OnClicked()
	    {
	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if ((bool)logged)
	        {
	            MessagingCenter.Send(this, "RemoveIngredienteFav", viewModel.SimpleIngredient.Name);
	            App.Current.Properties.TryGetValue("ingrdientesFav", out object ingredientes);
	            var list = ingredientes as List<string> ?? new List<string>();
                list.Add(viewModel.SimpleIngredient.Name);
                await this.Navigation.PopAsync();
	        }
	    }
    }
}