using LNU.JAVA.Core;
using LNU.JAVA.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LNU.JAVA.API.Controllers
{
    public class ValuesController : ApiController
    {
        private string url;
        private string connectionString;

        public ValuesController()
        {
            url = ConfigurationManager.AppSettings["Url"];
            connectionString = ConfigurationManager.AppSettings["RssConnectionString"];
        }

        public IHttpActionResult GetAll()
        {
            var repo = new Repository(connectionString);

            return Ok(repo.GetAll());
        }

        public async Task<IHttpActionResult> Load(Query query)
        {
            var provider = new CNNDataProvider(url);
            var list = await provider.GetRSS(query);

            var repo = new Repository(connectionString);
            await repo.Update(list);

            return Ok();
        }
    }
}
