using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Fba.Api.Helper
{
	public static class RestRequestHelper<T,T2> where T : class
	{
        public static async Task<T?> GetService(string url)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(result) || !response.IsSuccessStatusCode)
            {
              response.EnsureSuccessStatusCode();
              return null;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }


        public static async Task<T?> PostService(string url , T2 model)
        {
            using var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(result) || !response.IsSuccessStatusCode)
            {
               response.EnsureSuccessStatusCode();
               return null;
            }

            return JsonConvert.DeserializeObject<T?>(result);
        }

    }
}

