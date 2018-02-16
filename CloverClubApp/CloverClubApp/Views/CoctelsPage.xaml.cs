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
	public partial class CoctelsPage : ContentPage
	{
	    CoctelsViewModel viewModel;

	    public CoctelsPage()
	    {
	        InitializeComponent();

	        BindingContext = viewModel = new CoctelsViewModel();
	    }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var item = args.SelectedItem as Drink;
	        if (item == null)
	            return;

	        await Navigation.PushAsync(new CoctelDetailPage(new CoctelDetailViewModel(item)));

	        // Manually deselect item.
	        CoctelsListView.SelectedItem = null;
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