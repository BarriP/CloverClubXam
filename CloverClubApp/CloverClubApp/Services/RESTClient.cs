using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloverClubApp.Services
{
    class RESTClient : IRESTClient
    {
        private HttpClient client;
        public RESTClient()
        {
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<T> Get<T>(string uri)
        {
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<T>(content);
                return items;
            }

            return default(T);
        }
    }
}
