using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;
using System.Text;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for Login, Registration and Logout
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Display Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handle Login form submission
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _apiService
                .GetClient()
                .PostAsync("Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData =
                    await response.Content.ReadAsStringAsync();

                dynamic userData =
                    JsonConvert.DeserializeObject(responseData);

                HttpContext.Session.SetString(
                    "UserEmail",
                    (string)userData.email);

                HttpContext.Session.SetString(
                    "UserRole",
                    (string)userData.role);

                if ((string)userData.role == "Organiser")
                {
                    return RedirectToAction(
                        "Index",
                        "Dashboard");
                }

                return RedirectToAction(
                    "Index",
                    "Participant");
            }

            ViewBag.ErrorMessage =
                "Invalid email or password.";

            return View(model);
        }

        // Display Register page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Handle Register form submission
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _apiService
                .GetClient()
                .PostAsync("Auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ViewBag.ErrorMessage =
                await response.Content.ReadAsStringAsync();

            return View(model);
        }

        // Logout user
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}