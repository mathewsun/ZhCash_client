using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication10.Models;
using WebApplication10.Clients;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ZhCashClient _zhclient;

        public HomeController(ILogger<HomeController> logger, ZhCashClient zhclient)
        {
            _logger = logger;
            _zhclient = zhclient;
        }

        public IActionResult Index()
        {
            RpcResponseMessage result = _zhclient.GetZhCashResponse();

            return View(result.result);
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
