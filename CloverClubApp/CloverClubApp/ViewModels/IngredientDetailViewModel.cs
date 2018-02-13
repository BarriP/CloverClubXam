using System;
using System.Collections.Generic;
using System.Text;
using CloverClubApp.Models;

namespace CloverClubApp.ViewModels
{
    public class IngredientDetailViewModel : PublicBaseViewModel
    {
        public Ingredient Ingredient { get; set; }
        public IngredientDetailViewModel(SimpleIngredient item = null)
        {
            Title = item?.Name;

            if (item == null)
            {
                Ingredient = new Ingredient();
                return;
            }
        }
    }
}
