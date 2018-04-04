using LNU.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNU.Scrapper.Data
{
    public class Repository
    {
        private IMongoCollection<Article> collection;

        public Repository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("rss");
            collection = database.GetCollection<Article>("article");
        }

        public async Task Update(List<Article> articles)
        {
            foreach (var item in articles)
            {
                var elementExist = collection.AsQueryable().Any(el => el.Title == item.Title && item.PublishedAt == el.PublishedAt);
                if (!elementExist)
                {
                    await collection.InsertOneAsync(item);
                }
            }
        }

        public IQueryable<Article> GetAll()
        {
            return collection.AsQueryable();
        }
    }
}
