using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.DeviceSpecific;
using CloverClubApp.Models;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    public class IngredientDetailViewModel : PublicBaseViewModel
    {
        /* Fix de longitud de listView */
        private int _productListLength;
        public int ProductListLength
        {
            get => _productListLength;
            set => SetProperty(ref _productListLength, value);
        }

        private Ingredient _ingredient;
        public Ingredient Ingredient
        {
            get => _ingredient;
            set => SetProperty(ref _ingredient, value);
        }
        public SimpleIngredient SimpleIngredient { get; }
        public ObservableCollection<Product> Products { get; }
        public ObservableCollection<Drink> Related { get; } 

        public Command LoadIngredientCommand { get; }

        public IngredientDetailViewModel(SimpleIngredient item)
        {
            Title = item?.Name;
            SimpleIngredient = item;
            Ingredient = new Ingredient();
            Products = new ObservableCollection<Product>();
            Related = new ObservableCollection<Drink>();
            LoadIngredientCommand = new Command(async () => await ExecuteLoadStoreItemsCommand());
        }

        async Task ExecuteLoadStoreItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Ingredient = await CoctelService.RetrieveIngredient(SimpleIngredient.Name);

                var restProducts = await CoctelService.RetrieveProducts(SimpleIngredient.Name);
                foreach (var product in restProducts)
                {
                    Products.Add(product);
                }
                ProductListLength = Products.Count * 50 + Products.Count * 15;

                var relatedRest = await CoctelService.RetrieveRelated(SimpleIngredient.Name);
                foreach (var r in relatedRest)
                {
                    Related.Add(r);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                DependencyService.Get<IMessage>().ShortAlert("No se han podido recuperar los datos del ingrediente. Compruebe la conexion de red");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
