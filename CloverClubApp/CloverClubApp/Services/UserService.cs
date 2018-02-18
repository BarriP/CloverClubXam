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

        public bool AñadirCoctelFav(int id, string token)
        {
            var response = client.PostSecureJson<dynamic>($"{USER_URL}/{COCTELES}", token, id).Result;
            return response == null;
        }

        public bool AñadirIngredienteFav(string ing, string token)
        {
            var response = client.PostSecureJson<dynamic>($"{USER_URL}/{INGREDIENTES}", token, ing).Result;
            return response == null;
        }

        public bool BorrarCoctelFav(int id, string token)
        {
            var response = client.DeleteSecure<dynamic>($"{USER_URL}/{COCTELES}/{id}", token).Result;
            return response == null;
        }

        public bool BorrarIngredienteFav(string ing, string token)
        {
            var response = client.DeleteSecure<dynamic>($"{USER_URL}/{INGREDIENTES}/{ing}", token).Result;
            return response == null;
        }
    }

    public class Token
    {
        public string token { get; set; }
    }
}
