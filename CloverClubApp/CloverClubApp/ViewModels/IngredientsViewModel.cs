using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    class IngredientsViewModel : PublicBaseViewModel
    {
        private bool _showEmptyText;
        public bool ShowEmptyText
        {
            get => _showEmptyText;
            set => SetProperty(ref _showEmptyText, value);
        }

        public ObservableCollection<SimpleIngredient> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public IngredientsViewModel()
        {
            Title = "Listado de Ingredientes";
            Items = new ObservableCollection<SimpleIngredient>();
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
                var items = await CoctelService.RetrieveIngredients();
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
                ShowEmptyText = Items.Count == 0;
                IsBusy = false;
            }
        }
    }
}
