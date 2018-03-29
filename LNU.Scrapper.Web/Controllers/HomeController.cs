using LNU.Scrapper;
using LNU.Scrapper.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LNU.Scrapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private string url;
        private string connectionString;

        public HomeController()
        {
            url = ConfigurationManager.AppSettings["Url"];
            connectionString = ConfigurationManager.AppSettings["RssConnectionString"];
        }

        public ActionResult Index()
        {
            var repo = new Repository(connectionString);

            return View(repo.GetAll());
        }

        public async Task<ActionResult> Load(Query query)
        {
            var provider = new CNNDataProvider(url);
            var list = await provider.GetRSS(query);

            var repo = new Repository(connectionString);
            await repo.Update(list);

            return View("Index", list);
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