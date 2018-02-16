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
	public partial class CoctelDetailPage : ContentPage
	{
	    private CoctelDetailViewModel viewModel;
	    private bool initialized;

		public CoctelDetailPage (CoctelDetailViewModel viewModel)
		{
			InitializeComponent ();

		    BindingContext = this.viewModel = viewModel;
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

	        if (initialized) return;

	        viewModel.LoadItemsCommand.Execute(null);
	        initialized = true;
	    }

	    private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        viewModel.LoadActiveCollectionCommand.Execute(Picker.Items[Picker.SelectedIndex]);
	    }
	}
}