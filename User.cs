using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityApp
{
    public class User
    {
        public async Task<JObject> GetUser(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //var response = Task.Run(() => client.GetAsync(url)).Result;
            var response = Task.Run(async() => await client.GetAsync(url)).Result;
            //Task.WaitAll(task1, task2);
            if (response.IsSuccessStatusCode)
            {
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
            return new JObject { response.StatusCode };
        }
    }
}
