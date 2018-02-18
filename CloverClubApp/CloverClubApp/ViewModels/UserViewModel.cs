using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CloverClubApp.Models;
using CloverClubApp.Services;
using CloverClubApp.Views;
using Xamarin.Forms;

namespace CloverClubApp.ViewModels
{
    class UserViewModel : PublicBaseViewModel
    {
        public IUserService userService => DependencyService.Get<IUserService>() ?? new UserService();
        public UserViewModel()
        {
            Title = "Area Personal";

            MessagingCenter.Subscribe<LoginPage, LoginData>(this, "login", async (obj, item) =>
            {
                try
                {
                    var a = item;

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });

            MessagingCenter.Subscribe<RegisterPage, RegisterData>(this, "register", async (obj, item) =>
            {
                try
                {
                    var registered = userService.RegistrarUsuario(item);

                    var a = "";

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }
    }
}
