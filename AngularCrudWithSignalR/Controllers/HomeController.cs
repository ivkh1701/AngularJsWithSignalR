using AngularCrudWithSignalR.Data.Entites;
using AngularCrudWithSignalR.Factories;
using AngularCrudWithSignalR.Models;
using AngularCrudWithSignalR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AngularCrudWithSignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerfactory _customFactory;


        public HomeController(ILogger<HomeController> logger,
            ICustomerfactory customFactory)
        {
            _logger = logger;
            _customFactory = customFactory;
        }

        public IActionResult Index()
        {
            return View();
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