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
	public partial class IngredientsPage : ContentPage
	{
	    private IngredientsViewModel viewModel;

		public IngredientsPage ()
		{
			InitializeComponent ();

		    BindingContext = viewModel = new IngredientsViewModel();
        }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (!(args.SelectedItem is SimpleIngredient item))
	            return;

	        await Navigation.PushAsync(new IngredientDetailPage(new IngredientDetailViewModel(item)));

	        // Manually deselect item.
	        IngredientsListView.SelectedItem = null;
	    }


	    /*
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
        */

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        if (viewModel.Items.Count == 0)
	            viewModel.LoadItemsCommand.Execute(null);
	    }
    }
}