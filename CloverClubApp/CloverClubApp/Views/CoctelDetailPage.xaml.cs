using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using CloverClubApp.Models;
using CloverClubApp.Services;
using CloverClubApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloverClubApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoctelDetailPage : ContentPage
	{
	    private CoctelDetailViewModel viewModel;
	    private bool initialized;

		public CoctelDetailPage (CoctelDetailViewModel viewModel)
		{
			InitializeComponent ();

		    BindingContext = this.viewModel = viewModel;

            AddFav.Command = new Command(AddFav_OnClicked);
            RemoveFav.Command = new Command(RemoveFav_OnClicked);
        }

	    public CoctelDetailPage()
	    {
	        InitializeComponent();

	        var coctel = new Drink();

	        viewModel = new CoctelDetailViewModel(coctel);
	        BindingContext = viewModel;
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if (logged != null && (bool) logged)
	        {
	            App.Current.Properties.TryGetValue("coctelesFav", out object cocteles);
	            if (cocteles is IEnumerable<int> list)
	            {
	                int id = int.Parse(viewModel.Coctel.Id);
	                this.AddFav.IsVisible = !list.Contains(id);
	                this.RemoveFav.IsVisible = !this.AddFav.IsVisible;
	            }
	        }

            if (initialized) return;

	        viewModel.LoadItemsCommand.Execute(null);
	        initialized = true;
	    }

	    private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        viewModel.LoadActiveCollectionCommand.Execute(Picker.Items[Picker.SelectedIndex]);
	    }

	    private async void AddFav_OnClicked()
	    {
	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if ((bool) logged)
	        {
	            MessagingCenter.Send(this, "AddCoctelFav", Int32.Parse(viewModel.Coctel.Id));
	            App.Current.Properties.TryGetValue("coctelesFav", out object cocteles);
	            var list = cocteles as List<int> ?? new List<int>();
                list.Add(Int32.Parse(viewModel.Coctel.Id));
                await this.Navigation.PopAsync();
	        }

	    }

	    private async void RemoveFav_OnClicked()
	    {
	        App.Current.Properties.TryGetValue("loggedIn", out object logged);
	        if ((bool) logged)
	        {
	            MessagingCenter.Send(this, "RemoveCoctelFav", Int32.Parse(viewModel.Coctel.Id));
	            App.Current.Properties.TryGetValue("coctelesFav", out object cocteles);
	            var list = cocteles as List<int> ?? new List<int>();
	            list.Remove(Int32.Parse(viewModel.Coctel.Id));
                await this.Navigation.PopAsync();
            }
	    }
	}
}