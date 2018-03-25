using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LNU.DataScrapper
{
    public class CNNDataProvider
    {
        private readonly string url;

        public CNNDataProvider(string url)
        {
            this.url = url;
        }

        public async Task<List<Article>> GetRSS()
        {
            using (var client = new HttpClient())
            {
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
