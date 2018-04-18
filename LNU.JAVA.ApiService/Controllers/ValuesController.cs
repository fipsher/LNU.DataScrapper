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
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        private string url;
        private string connectionString;
        private readonly Repository repository;

        public ValuesController()
        {
            url = ConfigurationManager.AppSettings["Url"];
            connectionString = ConfigurationManager.AppSettings["RssConnectionString"];
            repository = new Repository(connectionString);
        }

        [HttpGet, Route("getall")]
        public IHttpActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet, Route("getbyid")]
        public IHttpActionResult GetById(Guid id)
        {
            return Ok(repository.GetById(id));
        }

        [HttpPost, Route("Add")]
        public async Task<IHttpActionResult> Add(ArticleModel article)
        {
            await repository.Add(article.ToArticle());
            
            return Ok();
        }

        [HttpPost, Route("update")]
        public async Task<IHttpActionResult> Update(ArticleModel article)
        {
            await repository.Update(article.ToArticle());
            return Ok();
        }


        [HttpDelete, Route("delete")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await repository.Delete(id);
            return Ok();
        }

        [HttpPost, Route("load")]
        public async Task<IHttpActionResult> Load(Query query)
        {
            var provider = new CNNDataProvider(url);
            var list = await provider.GetRSS(query);

            var repo = new Repository(connectionString);
            await repo.Add(list);

            return Ok();
        }
    }
}
