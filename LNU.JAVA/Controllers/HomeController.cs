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
    }
}