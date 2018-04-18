using LNU.JAVA.Core;
using LNU.JAVA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LNU.JAVA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string url;

        public HomeController()
        {
            url = "http://localhost:61274/api/values/";
        }

        public async Task<ActionResult> PartialIndex()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}getall");
                response.EnsureSuccessStatusCode();

                var stringData = await response.Content.ReadAsStringAsync();
                var array = JsonConvert.DeserializeObject<List<Article>>(stringData);
                return PartialView(array);
            }
        }

        public async Task<ActionResult> Index(Query query = null)
        {
            if (query == null)
            {
                query = new Query();
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}getall");
                response.EnsureSuccessStatusCode();

                var stringData = await response.Content.ReadAsStringAsync();
                var array = JsonConvert.DeserializeObject<List<Article>>(stringData);
                return View(new IndexModel
                {
                    Query = query,
                    Articles = array
                });
            }
        }

        public async Task<ActionResult> Load(Query query)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(
                   JsonConvert.SerializeObject(query),
                   Encoding.UTF8,
                   "application/json");

                var response = await client.PostAsync($"{url}Load", content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", query);
            }
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        public async Task<ActionResult> Create(ArticleModel article)
        {
            if (!ModelState.IsValid) { return View(article.ToArticle()); }
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(
                       JsonConvert.SerializeObject(article, Formatting.None, new JsonSerializerSettings
                       {
                           NullValueHandling = NullValueHandling.Ignore
                       }),
                       Encoding.UTF8,
                       "application/json");

                    var response = await client.PostAsync($"{url}Add", content);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(article.ToArticle());
            }
        }

        // GET: Article/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await GetArticleById(id);
            return View(model);
        }

        // POST: Article/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ArticleModel article)
        {
            try
            {
                if (!ModelState.IsValid) { return View(article.ToArticle()); }
                using (var client = new HttpClient())
                {
                    article.ID = id;
                    StringContent content = new StringContent(
                       JsonConvert.SerializeObject(article, Formatting.None, new JsonSerializerSettings
                       {
                           NullValueHandling = NullValueHandling.Ignore
                       }),
                       Encoding.UTF8,
                       "application/json");

                    var response = await client.PostAsync($"{url}Update", content);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(article.ToArticle());
            }
        }

        // GET: Article/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var model = await GetArticleById(id);
            return View(model);
        }

        // POST: Article/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, Article article)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"{url}Delete?id={id}");
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                article.ID = id;
                return View(article);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private async Task<Article> GetArticleById(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}GetById?id={id}");
                response.EnsureSuccessStatusCode();
                var model = JsonConvert.DeserializeObject<Article>(await response.Content.ReadAsStringAsync());
                return model;
            }
        }
    }
}