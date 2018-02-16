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
    public class IngredientDetailViewModel : PublicBaseViewModel
    {
        private Ingredient _ingredient;
        public Ingredient Ingredient
        {
            get => _ingredient;
            set => SetProperty(ref _ingredient, value);
        }
        public SimpleIngredient SimpleIngredient { get; }
        public ObservableCollection<Product> Products { get; }

        public Command LoadIngredientCommand { get; }

        public IngredientDetailViewModel(SimpleIngredient item)
        {
            Title = item?.Name;
            SimpleIngredient = item;
            Ingredient = new Ingredient();
            Products = new ObservableCollection<Product>();
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
                Products.Add(await CoctelService.RetrieveProduct(SimpleIngredient.Name));

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
