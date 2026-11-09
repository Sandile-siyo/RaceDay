using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for Category Management
    public class CategoryController : Controller
    {
        // Service used to communicate with the API
        private readonly ApiService _apiService;

        // Constructor receives ApiService through dependency injection
        public CategoryController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Display all categories
        public async Task<IActionResult> Index()
        {
            // Create empty category list
            var categories = new List<Category>();

            // Send GET request to API
            var response = await _apiService
                .GetClient()
                .GetAsync("Categories");

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read JSON response
                var json =
                    await response.Content.ReadAsStringAsync();

                // Convert JSON into Category objects
                categories =
                    JsonConvert.DeserializeObject<List<Category>>(json)
                    ?? new List<Category>();
            }

            // Send categories to the view
            return View(categories);
        }
    }
}