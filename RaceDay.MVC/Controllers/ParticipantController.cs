using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for participant pages
    public class ParticipantController : Controller
    {
        // Displays participant dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}
