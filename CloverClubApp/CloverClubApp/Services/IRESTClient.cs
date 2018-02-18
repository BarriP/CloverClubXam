using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloverClubApp.Services
{
    interface IRESTClient
    {
        Task<T> Get<T>(string uri);
        Task<T> GetSecure<T>(string uri, string token);
        Task<T> PostSecureJson<T>(string uri, string token, object body);
        Task<T> PostJson<T>(string uri, object body);
        Task<T> DeleteSecure<T>(string uri, string token);
    }
}
