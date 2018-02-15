﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CloverClubApp.Models
{
    public class SimpleIngredient
    {
        public string Name { get; set; }
        public string IngredientUrl { get; set; }
        public string ThumbnailUrl { get; set; }

        [JsonProperty("image")]
        public string ImageUrl { get; set; }

        /* calculated */
        public ImageSource Thumbnail => ImageSource.FromUri(new Uri(ThumbnailUrl));
        public ImageSource Iamge => ImageSource.FromUri(new Uri(ImageUrl));

    }
}