using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.DeviceSpecific;
using CloverClubApp.Models;
using CloverClubApp.Services;
using CloverClubApp.Views;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    class UserViewModel : PublicBaseViewModel
    {
        public IUserService userService => DependencyService.Get<IUserService>() ?? new UserService();

        public bool LoginStatus
        {
            get
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);
                App.Current.Properties.TryGetValue("tokenDate", out object tokenDateObject);

                if (tokenObject == null || tokenDateObject == null)
                    return false;

                var date = tokenDateObject as DateTime?;

                return DateTime.Compare(DateTime.Now, date.Value) < 0;
            }
        }

        public Command LoadItemsCommand { get; }
        public ObservableCollection<Drink> Drinks { get; }
        public ObservableCollection<SimpleIngredient> Ingredients { get; }

        public UserViewModel()
        {
            Title = "Area Personal";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Drinks = new ObservableCollection<Drink>();
            Ingredients = new ObservableCollection<SimpleIngredient>();

            MessagingCenter.Subscribe<CoctelDetailPage, int>(this, "RemoveCoctelFav", (obj, item) =>
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);
                try
                {
                    userService.BorrarCoctelFav(item, tokenObject.ToString());
                    DependencyService.Get<IMessage>().ShortAlert("Coctel borrado de favoritos");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    DependencyService.Get<IMessage>().ShortAlert("No se ha podido borrar de favoritos, intentelo de nuevo");
                }
            });

            MessagingCenter.Subscribe<CoctelDetailPage, int>(this, "AddCoctelFav", (obj, item) =>
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);
                try { 
                    userService.AñadirCoctelFav(item, tokenObject.ToString());
                    DependencyService.Get<IMessage>().ShortAlert("Coctel añadido a favoritos");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    DependencyService.Get<IMessage>().ShortAlert("No se han podido añadir a favoritos, intentelo de nuevo");
                }
            });

            MessagingCenter.Subscribe<IngredientDetailPage, string>(this, "RemoveIngredienteFav", (obj, item) =>
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);
                try { 
                    userService.BorrarIngredienteFav(item, tokenObject.ToString());
                    DependencyService.Get<IMessage>().ShortAlert("Ingrediente borrado de favoritos");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    DependencyService.Get<IMessage>().ShortAlert("No se ha podido borrar de favoritos, intentelo de nuevo");
                }
            });

            MessagingCenter.Subscribe<IngredientDetailPage, string>(this, "AddIngredienteFav", (obj, item) =>
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);
                try { 
                    userService.AñadirIngredienteFav(item, tokenObject.ToString());
                    DependencyService.Get<IMessage>().ShortAlert("Ingrediente añadido a favoritos");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    DependencyService.Get<IMessage>().ShortAlert("No se ha podido borrar de favoritos, intentelo de nuevo");
                }
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                App.Current.Properties.TryGetValue("token", out object tokenObject);

                var user = await userService.GetUser(tokenObject.ToString());

                App.Current.Properties.Add("coctelesFav", user.CoctelesFavList);
                App.Current.Properties.Add("ingredientesFav", user.IngredientesFavList);


                var cocteles = await CoctelService.RetrieveDrinks();
                var ingredientes = await CoctelService.RetrieveIngredients();

                Drinks.Clear();
                foreach (var coctelFav in user.CoctelesFavList)
                {
                    Drinks.Add(cocteles.FirstOrDefault(x => Int32.Parse(x.Id).Equals(coctelFav)));
                }

                Ingredients.Clear();
                foreach (var ingredientFav in user.IngredientesFavList)
                {
                    Ingredients.Add(ingredientes.FirstOrDefault(x => x.Name.Equals(ingredientFav)));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                DependencyService.Get<IMessage>().ShortAlert("No se han podido recuperar los datos de usuario. Compruebe la conexion de red");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
