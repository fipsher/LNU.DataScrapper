using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LNU.JAVA.Core
{
    public class Article
    {
        [BsonId]
        public Guid ID { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTime PublishedAt { get; set; }
    }

    public class ArticleModel
    {
        public Guid? ID { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "urlToImage")]
        public string UrlToImage { get; set; }

        [JsonProperty(PropertyName = "publishedAt")]
        public DateTime PublishedAt { get; set; }

        public Article ToArticle()
        {
            return new Article
            {
               Author = Author,
               Description = Description,
               ID = ID .HasValue ? ID.Value : new Guid(),
               PublishedAt= PublishedAt,
               Title = Title,
               Url = Url,
               UrlToImage = UrlToImage
            };
        }
    }
}
