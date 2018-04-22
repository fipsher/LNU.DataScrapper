using LNU.JAVA.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LNU.JAVA.Data
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

        public async Task Add(Article article)
        {
            var elementExist = collection.AsQueryable().Any(el => el.Title == article.Title && article.PublishedAt == el.PublishedAt);
            if (!elementExist)
            {
                await collection.InsertOneAsync(article);
            }
        }

        public async Task Add(List<Article> articles)
        {
            foreach (var item in articles)
            {
                await Add(item);
            }
        }

        public async Task Update(Article article)
        {
            var filter = Builders<Article>.Filter.Eq(s => s.ID, article.ID);
            await collection.ReplaceOneAsync(filter, article);
        }

        public async Task Delete(Guid id)
        {
            var filter = Builders<Article>.Filter.Eq(s => s.ID, id);
            await collection.DeleteOneAsync(filter);
        }

        public IQueryable<Article> GetAll()
        {
            return collection.AsQueryable();
        }

        public Article GetById(Guid id)
        {
            return collection.AsQueryable().SingleOrDefault(el => el.ID == id);
        }

        public List<Article> WhereTitleContains(string searchTerm)
        {
            var lower = searchTerm.ToLower();
           
            return collection.AsQueryable().Where(el =>
    el.Title.ToLower().Contains(lower)).ToList();
        }
    }
}
