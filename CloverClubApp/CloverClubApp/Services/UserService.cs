using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(CloverClubApp.Services.UserService))]
namespace CloverClubApp.Services
{
    public class UserService : IUserService
    {
        public const string USER_URL = "https://cloverclubrest.azurewebsites.net/api/User";
        public const string TOKENS_URL = "https://cloverclubrest.azurewebsites.net/api/Tokens";

        public const string COCTELES = "cocteles";
        public const string INGREDIENTES = "ingredientes";

        private RESTClient client = new RESTClient();

        public User RegistrarUsuario(RegisterData regData)
        {
            return client.PostJson<User>($"{TOKENS_URL}/register", regData).Result;
        }

        public string Login(LoginData loginData)
        {
            return client.PostJson<Token>($"{TOKENS_URL}/login", loginData).Result.token;
        }

        public async Task<User> GetUser(string token)
        {
            return await client.GetSecure<User>(USER_URL, token);
        }
    }

    public class Token
    {
        public string token { get; set; }
    }
}
