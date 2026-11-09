using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for Results Management
    public class ResultController : Controller
    {
        // Displays Results page
        public IActionResult Index()
        {
            return View();
        }
    }
}
