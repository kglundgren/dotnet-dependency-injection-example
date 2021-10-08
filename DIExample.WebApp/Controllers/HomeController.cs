using DIExample.WebApp.Models;
using DIExample.WebApp.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DIExample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IOptionsMonitor<ApiOptions> ApiOptions;

        public HomeController(ILogger<HomeController> logger, IOptionsMonitor<ApiOptions> apiOptions)
        {
            Logger = logger;
            ApiOptions = apiOptions;
        }

        public IActionResult Index()
        {
            var model = new IndexModel { ApiUrl = ApiOptions.CurrentValue.Url };
            return View(model);
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
