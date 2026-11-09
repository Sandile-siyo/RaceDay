using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for Registration Management
    public class RegistrationController : Controller
    {
        // Service used to communicate with the API
        private readonly ApiService _apiService;

        // Constructor receives ApiService through dependency injection
        public RegistrationController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Display registrations
        public async Task<IActionResult> Index()
        {
            // Create empty registration list
            var registrations = new List<Registration>();

            // Send request to API
            var response = await _apiService
                .GetClient()
                .GetAsync("Registration");

            // Check if request succeeded
            if (response.IsSuccessStatusCode)
            {
                // Read JSON response
                var json =
                    await response.Content.ReadAsStringAsync();

                // Convert JSON to Registration objects
                registrations =
                    JsonConvert.DeserializeObject<List<Registration>>(json)
                    ?? new List<Registration>();
            }

            return View(registrations);
        }
    }
}