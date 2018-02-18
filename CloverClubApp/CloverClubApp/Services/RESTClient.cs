using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CloverClubApp.Models;
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

        public async Task<T> GetSecure<T>(string uri, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<T>(content);
                return items;
            }

            return default(T);
        }

        public async Task<T> PostSecureJson<T>(string uri, string token, object body)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringPayload = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<T>(content);
                return items;
            }

            return default(T);
        }

        public async Task<T> PostJson<T>(string uri, object body)
        {
            var stringPayload = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpClient newClient = new HttpClient();

            var response = newClient.PostAsync(new Uri(uri), httpContent).Result;
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
