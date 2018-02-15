using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using CloverClubApp.Views;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    class CoctelsViewModel : PublicBaseViewModel
    {
        public ObservableCollection<Drink> Items { get; set; }

        public bool ShowEmptyText => Items?.Count == 0;

        public Command LoadItemsCommand { get; set; }

        public CoctelsViewModel()
        {
            Title = "Listado de Cocteles";
            Items = new ObservableCollection<Drink>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            /*
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
            */
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await CoctelService.RetrieveDrinks();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
