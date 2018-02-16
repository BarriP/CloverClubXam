using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(CloverClubApp.Services.CoctelService))]
namespace CloverClubApp.Services
{
    class CoctelService : ICoctelService
    {
        public const string URL = "http://156.35.98.52:8081/api/";

        public const string COCTELES = "cocteles";
        public const string INGREDIENTES = "ingredientes";
        

        private RESTClient client = new RESTClient();


        public async Task<IEnumerable<Drink>> RetrieveDrinks() => await client.Get<IEnumerable<Drink>>($"{URL}{COCTELES}");

        public async Task<Drink> RetrieveDrink(int drinkId) => await client.Get<Drink>($"{URL}{COCTELES}/{drinkId}");

        public async Task<IEnumerable<SimpleIngredient>> RetrieveIngredients() => await client.Get<IEnumerable<SimpleIngredient>>($"{URL}{INGREDIENTES}");

        public async Task<Ingredient> RetrieveIngredient(string ingredientName) => await client.Get<Ingredient>($"{URL}{INGREDIENTES}/{ingredientName}");

        public async Task<IEnumerable<Drink>> RetrieveRelated(string ingredientName) => await client.Get<IEnumerable<Drink>>($"{URL}{INGREDIENTES}/{ingredientName}/{COCTELES}");

        public async Task<Product> RetrieveProduct(string ingredientName) => await client.Get<Product>($"{URL}{INGREDIENTES}/{ingredientName}/precio");
    }
}
