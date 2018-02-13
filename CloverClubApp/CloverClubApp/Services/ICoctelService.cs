using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;

namespace CloverClubApp.Services
{
    public interface ICoctelService
    {
        Task<List<Drink>> RetrieveDrinks();
        Task<Drink> RetrieveDrink(int drinkId);
        Task<List<SimpleIngredient>> RetrieveIngredients();
        Task<Ingredient> RetrieveIngredient(string ingredientName);
        Task<List<Drink>> RetrieveRelated(string ingredientName);

        // TODO Precios
    }
}
