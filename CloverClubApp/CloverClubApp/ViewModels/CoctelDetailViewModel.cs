using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using CloverClubApp.ViewModels;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
   public class CoctelDetailViewModel : PublicBaseViewModel
   {
        private int _listLength;
        public int ListLength
        {
            get => _listLength;
            set => SetProperty(ref _listLength, value);
        }
        public Drink Coctel { get; set; }
        public Command LoadStoreItemsCommand { get; }
        public Command LoadIngredientsCommand { get; }
        public ObservableCollection<Product> StoreItems { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public CoctelDetailViewModel(Drink item = null)
        {
            Title = item?.Name;
            Coctel = item;
            StoreItems = new ObservableCollection<Product>();
            Ingredients = new ObservableCollection<Ingredient>();
            LoadStoreItemsCommand = new Command(async () => await ExecuteLoadStoreItemsCommand());
            LoadIngredientsCommand = new Command(async () => await ExecuteLoadIngredientsCommand());
        }

        //TODO ASYNC
        // AQUI NOS TOCAN 2 ASYNC
        // * CARGAR TIENDA
        // * CARGAR INGREDIENTES

        async Task ExecuteLoadIngredientsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Ingredients.Clear();
                foreach (var drinkIngredient in Coctel.Ingredients)
                {
                    var ing = await CoctelService.RetrieveIngredient(drinkIngredient.IngredientName);
                    ing.Description = drinkIngredient.ToString();
                    Ingredients.Add(ing);
                }

                ListLength = Ingredients.Count * 50 + Ingredients.Count * 15;
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

        async Task ExecuteLoadStoreItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                /*
                StoreItems.Clear();
                var items = await CoctelService.RetrieveIngredients();
                foreach (var item in items)
                {
                    StoreItems.Add(item);
                }
                */
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
