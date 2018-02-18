using System;
using System.Collections.Generic;
using System.Text;

namespace CloverClubApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public List<int> CoctelesFavList { get; set; }
        public List<string> IngredientesFavList { get; set; }
    }
}
