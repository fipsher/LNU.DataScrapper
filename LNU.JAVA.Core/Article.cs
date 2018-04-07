using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LNU.JAVA.Core
{
    public class Article
    {
        [BsonId]
        public Guid ID { get; set; }

        public Source Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
