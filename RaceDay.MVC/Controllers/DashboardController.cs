using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for dashboard pages
    public class DashboardController : Controller
    {
        // Display organiser dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}