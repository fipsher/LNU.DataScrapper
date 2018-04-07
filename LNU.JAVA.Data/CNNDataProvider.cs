using LNU.JAVA.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LNU.JAVA.Data
{
    public class CNNDataProvider
    {
        private string url;

        public CNNDataProvider(string url)
        {
            this.url = url;
        }

        public async Task<List<Article>> GetRSS(Query query)
        {
            using (var client = new HttpClient())
            {
                url = $"{url}{query.Type}?sources={query.Source}&apiKey=88467a0580364682aa756073633bf4b0&q={query.Q}&country={query.Country}";

                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();

                string stringified = await result.Content.ReadAsStringAsync();

                RootObject obj = JsonConvert.DeserializeObject<RootObject>(stringified);

                if (obj.Status == "ok")
                {
                    return obj.Articles;
                }
                else
                {
                    throw new Exception("Rss server error");
                }
            }
        }
    }
}
