using MongoDB.Driver;
using System.Configuration;
using System.Linq;

namespace LNU.DataScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings["Url"];
            var connectionString = ConfigurationManager.AppSettings["RssConnectionString"];

            CNNDataProvider provider = new CNNDataProvider(url);

            var list = provider.GetRSS().Result;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("rss");
            var collection = database.GetCollection<Article>("article");

            foreach (var item in list)
            {
                var elementExist = collection.AsQueryable().Any(el => el.Title == item.Title && item.PublishedAt == el.PublishedAt);
                if (!elementExist)
                {
                    collection.InsertOne(item);
                }
            }
        }
    }
}
