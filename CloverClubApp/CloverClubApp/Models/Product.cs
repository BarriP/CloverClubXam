using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloverClubApp.Models
{
    public class Product
    {
        public string IngredientName { get; set; }
        public string Photo { get; set; }
        public string Price { get; set; }

        /* CALCULATED */
        public ImageSource Thumbnail => new UriImageSource{ Uri = new Uri(Photo)};
    }
}
