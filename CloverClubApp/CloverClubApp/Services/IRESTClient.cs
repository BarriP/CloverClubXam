using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloverClubApp.Services
{
    interface IRESTClient
    {
        Task<T> Get<T>(string uri);
    }
}
