using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;

namespace CloverClubApp.Services
{
    public interface ICoctelService
    {
        Task<IEnumerable<Drink>> RetrieveDrinks();
        Task<Drink> RetrieveDrink(int drinkId);
        Task<IEnumerable<SimpleIngredient>> RetrieveIngredients();
        Task<Ingredient> RetrieveIngredient(string ingredientName);
        Task<IEnumerable<Drink>> RetrieveRelated(string ingredientName);
        Task<Product> RetrieveProduct(string ingredientName);
    }
}
