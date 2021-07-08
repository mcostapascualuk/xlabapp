using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XlabApp.Models;

namespace XlabApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            List<Leedsbeerquest> lbqlist = new List<Leedsbeerquest>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://xlabapitest.azurewebsites.net/xlabapi"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lbqlist = JsonConvert.DeserializeObject<List<Leedsbeerquest>>(apiResponse);
                }
            }
            return View(lbqlist);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
