using System.Collections.Generic;

namespace LNU.Scrapper.Web
{
    public class RootObject
    {
        public string Status { get; set; }

        public int TotalResults { get; set; }

        public List<Article> Articles { get; set; }
    }
}
