using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CloverClubApp.Models
{
    public class Drink
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Video { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }

        [JsonProperty("thumbnailURL")]
        public string ThumbnailURL { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Iba { get; set; }

        /* Calculated */
        public ImageSource Thumbnail => ImageSource.FromUri(new Uri("http://" + ThumbnailURL));
    }

    public class DrinkIngredient
    {
        public string IngredientName { get; set; }
        public string Measure { get; set; }
        public string IngredientUrl { get; set; }
    }
}
