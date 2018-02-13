using System;
using System.Collections.Generic;
using System.Text;
using CloverClubApp.Models;
using CloverClubApp.ViewModels;

namespace CloverClubApp.ViewModels
{
   public class CoctelDetailViewModel : PublicBaseViewModel
    {
        public Drink Coctel { get; set; }
        public CoctelDetailViewModel(Drink item = null)
        {
            Title = item?.Name;
            Coctel = item;
        }
    }
}
