using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CloverClubApp.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        [JsonProperty("image")]
        public string ImageUrl { get; set; }

        private string _description;
        public string Description
        {
            get => String.IsNullOrEmpty(_description) ? "The perfect ingredient for a drink" : _description;
            set => _description = value;
        }

        public string Type { get; set; }

        /* calculated */
        public ImageSource Thumbnail => ImageSource.FromUri(new Uri(ThumbnailUrl));
        public ImageSource Image => ImageSource.FromUri(new Uri(ImageUrl));
    }
}
