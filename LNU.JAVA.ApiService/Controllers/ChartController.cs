using LNU.JAVA.Core;
using LNU.JAVA.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LNU.JAVA.ApiService.Controllers
{
    [RoutePrefix("api/Chart")]
    public class ChartController : ApiController
    {
        private string connectionString;
        private readonly Repository repository;

        public ChartController()
        {
            connectionString = ConfigurationManager.AppSettings["RssConnectionString"];
            repository = new Repository(connectionString);
        }

        [HttpGet, Route("GetChart/{searchTerm}")]
        public IHttpActionResult GetChartData(string searchTerm)
        {
            var items = repository.WhereTitleContains(searchTerm)
                .Select(article => new Article
                {
                    PublishedAt = article.PublishedAt.Date,
                    Title = article.Title,
                })
                .GroupBy(article => article.PublishedAt)
                .Select(el => new ChartDataElement
                {
                    Date = el.Key,
                    Y = el.Count()
                });

            return Ok(items);
        }
    }
}
