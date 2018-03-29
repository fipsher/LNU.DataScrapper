using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LNU.Scrapper.Web.Models
{
    public class Query
    {
        public string Q { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
    }
}