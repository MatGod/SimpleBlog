using System.Diagnostics;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationLayer;
using WebApplication.Models;

namespace WebApplication.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ServiceManager _serviceManager;

        public HomeController(ILogger<HomeController> logger, DataManager dataManager) {
            _logger = logger;
            _serviceManager = new ServiceManager(dataManager);
        }

        public IActionResult Index() {
            var dirs = _serviceManager.DirectoryService.GetDirectoriesList();
            return View(dirs);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}