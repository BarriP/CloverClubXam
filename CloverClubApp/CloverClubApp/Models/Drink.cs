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
        public IEnumerable<DrinkIngredient> Ingredients { get; set; }
        //No, no es el "iva" del producto, se refiere al registro en la International Bartenders Association
        public string Iba { get; set; }

        /* Calculated */
        public ImageSource Thumbnail => String.IsNullOrEmpty(ThumbnailURL) ? null : ImageSource.FromUri(new Uri("http://" + ThumbnailURL));
        public string DisplayCategory => $"{Category} - {Alcoholic}";
        public string DisplayServe => $"To be served with: {Glass ?? "A fancy glass"}";
    }

    public class DrinkIngredient
    {
        public string IngredientName { get; set; }
        public string Measure { get; set; }
        public string IngredientUrl { get; set; }
        public string ThumbnailUrl { get; set; }

        public ImageSource Thumbnail => String.IsNullOrEmpty(ThumbnailUrl) ? null : ImageSource.FromUri(new Uri(ThumbnailUrl));
        public string DisplayText => $"{IngredientName} - {Measure}";

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
