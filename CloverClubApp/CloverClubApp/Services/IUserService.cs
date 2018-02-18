using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;

namespace CloverClubApp.Services
{
    interface IUserService
    {
        User RegistrarUsuario(RegisterData user);
        string Login(LoginData loginData);
        Task<User> GetUser(string token);
    }
}
