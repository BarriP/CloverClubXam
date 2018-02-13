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
	public partial class IngredientDetailPage : ContentPage
	{
	    private IngredientDetailViewModel viewModel;

	    public IngredientDetailPage(IngredientDetailViewModel viewModel)
	    {
	        InitializeComponent();

	        BindingContext = this.viewModel = viewModel;
	    }

	    public IngredientDetailPage()
	    {
	        InitializeComponent();

	        var ing = new Ingredient();

	        viewModel = new IngredientDetailViewModel(ing);
	        BindingContext = viewModel;
	    }
    }
}