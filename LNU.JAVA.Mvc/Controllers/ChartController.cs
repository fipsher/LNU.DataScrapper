using LNU.JAVA.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LNU.JAVA.Mvc.Controllers
{
    public class ChartController : Controller
    {
        private string url;

        public ChartController()
        {
            url = $"{ConfigurationManager.AppSettings["apiUrl"]}/api/chart/";
        }

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetChartData(string searchTerm)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{url}GetChart/{searchTerm}");
                response.EnsureSuccessStatusCode();

                var stringData = await response.Content.ReadAsStringAsync();
                var array = JsonConvert.DeserializeObject<List<ChartDataElement>>(stringData);

                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
    }
}