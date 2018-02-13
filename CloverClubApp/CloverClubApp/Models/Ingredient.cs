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
        public string Description { get; set; }
        public string Type { get; set; }

        /* calculated */
        public ImageSource Thumbnail => ImageSource.FromUri(new Uri(ThumbnailUrl));
        public ImageSource Image => ImageSource.FromUri(new Uri(ImageUrl));
    }
}
