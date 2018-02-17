using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CloverClubApp.Models
{
    public class Product
    {
        public string IngredientName { get; set; }
        public string Photo { get; set; }
        [JsonProperty("price")]
        public string PriceString { get; set; }
        public double Price => Double.Parse(PriceString, CultureInfo.InvariantCulture);
        public string PriceText => $"{Price} €";

        /* CALCULATED */
        public ImageSource Thumbnail => String.IsNullOrEmpty(Photo) ? null : ImageSource.FromUri(new Uri(Photo));
    }
}
