using System;
using System.Collections.Generic;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(CloverClubApp.Services.UserService))]
namespace CloverClubApp.Services
{
    public class UserService : IUserService
    {
    }
}
