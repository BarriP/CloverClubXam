using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
using CloverClubApp.ViewModels;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    public class CoctelDetailViewModel : PublicBaseViewModel
    {
        /* Fix de longitud de listView */
        private int _ingredientListLength;
        public int IngredientListLength
        {
            get => _ingredientListLength;
            set => SetProperty(ref _ingredientListLength, value);
        }

        private string _precioFinal;
        public string PrecioFinal
        {
            get => _precioFinal;
            set => SetProperty(ref _precioFinal, value);
        }

        public Command LoadItemsCommand { get; }
        public Command<string> LoadActiveCollectionCommand { get; } 

        public Drink Coctel { get; set; }

        private ObservableCollection<Product> _actualStoreItems;
        public ObservableCollection<Product> StoreItems
        {
            get => _actualStoreItems;
            set => SetProperty(ref _actualStoreItems, value);
        }
        private IDictionary<string, ObservableCollection<Product>> produtcs;


        public CoctelDetailViewModel(Drink item)
        {
            Title = item?.Name;
            Coctel = item;
            IngredientListLength = Coctel.Ingredients.Count() * 50 + Coctel.Ingredients.Count() * 15;

            LoadItemsCommand = new Command(async () => await ExecuteLoadStoreItemsCommand());
            LoadActiveCollectionCommand = new Command<string>(ing => StoreItems = produtcs[ing]);

            StoreItems = new ObservableCollection<Product>();
            produtcs = new Dictionary<string, ObservableCollection<Product>>();
            foreach (var ing in Coctel.Ingredients)
            {
                produtcs.Add(ing.IngredientName, new ObservableCollection<Product>());
            }
        }

        async Task ExecuteLoadStoreItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                double price = 0.0;
                foreach (var ing in Coctel.Ingredients)
                {
                    var collection = produtcs[ing.IngredientName];
                    var product = await CoctelService.RetrieveProduct(ing.IngredientName);
                    collection.Add(product);
                    price += product.Price;
                }
                PrecioFinal = $"{price} €/coctel";
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