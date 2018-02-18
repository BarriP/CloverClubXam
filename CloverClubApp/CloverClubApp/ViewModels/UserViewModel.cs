using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
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

        public string User { get; set; }

        public UserViewModel()
        {
            Title = "Area Personal";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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
                var cocteles = await CoctelService.RetrieveDrinks();
                var ingredientes = await CoctelService.RetrieveIngredients();


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
