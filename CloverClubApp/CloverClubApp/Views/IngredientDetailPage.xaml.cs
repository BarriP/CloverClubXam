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

	        if (initialized) return;

	        viewModel.LoadIngredientCommand.Execute(null);
	        initialized = true;
	    }
    }
}