using LNU.JAVA.Core;
using System.Collections.Generic;

namespace LNU.JAVA.Mvc.Models
{
    public class IndexModel
    {
        public Query Query { get; set; }
        public List<Article> Articles { get; set; }
    }
}