using LNU.JAVA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LNU.JAVA.Models
{
    public class IndexModel
    {
        public Query Query { get; set; }
        public List<Article> Articles { get; set; }
    }
}