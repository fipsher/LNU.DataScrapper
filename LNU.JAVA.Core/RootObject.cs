using System.Collections.Generic;

namespace LNU.JAVA.Core
{
    public class RootObject
    {
        public string Status { get; set; }

        public int TotalResults { get; set; }

        public List<Article> Articles { get; set; }
    }
}
