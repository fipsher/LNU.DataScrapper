using LNU.Models;
using LNU.Scrapper.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LNU.Scrapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private string url;

        public HomeController()
        {
            url = "http://localhost:64438/api/values/";
        }

        public async Task<ActionResult> PartialIndex()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}getall");
                response.EnsureSuccessStatusCode();

                var stringData = await response.Content.ReadAsStringAsync();
                var array = JsonConvert.DeserializeObject<List<Article>>(stringData);
                return PartialView("Index", array);
            }
        }

        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}getall");
                response.EnsureSuccessStatusCode();

                var stringData = await response.Content.ReadAsStringAsync();
                var array = JsonConvert.DeserializeObject<List<Article>>(stringData);
                return View(array);
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
                return RedirectToAction("Index");
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