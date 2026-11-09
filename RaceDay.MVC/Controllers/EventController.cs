using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;
using System.Text;

namespace RaceDay.MVC.Controllers
{
    // Controller responsible for Event Management
    public class EventController : Controller
    {
        // Service used to communicate with the API
        private readonly ApiService _apiService;

        // Constructor receives ApiService through dependency injection
        public EventController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Display all events retrieved from the API
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = new List<Event>();

            var response = await _apiService
                .GetClient()
                .GetAsync("Events");

            if (response.IsSuccessStatusCode)
            {
                var json =
                    await response.Content.ReadAsStringAsync();

                events =
                    JsonConvert.DeserializeObject<List<Event>>(json)
                    ?? new List<Event>();
            }

            return View(events);
        }

        // Display Create Event page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle Create Event form submission
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            // Convert event details to JSON
            var json = JsonConvert.SerializeObject(model);

            // Prepare JSON content
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // Send event to API
            var response = await _apiService
                .GetClient()
                .PostAsync("Events", content);

            // Redirect to Event list if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Display Edit Event page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Request event from API
            var response = await _apiService
                .GetClient()
                .GetAsync($"Events/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into Event object
            var raceEvent =
                JsonConvert.DeserializeObject<Event>(json);

            if (raceEvent == null)
            {
                return RedirectToAction("Index");
            }

            // Populate Edit ViewModel
            var model = new EditEventViewModel
            {
                EventId = raceEvent.EventId,
                EventName = raceEvent.EventName,
                EventDescription = raceEvent.EventDescription,
                EventDate = raceEvent.EventDate,
                Location = raceEvent.Location,
                Distance = raceEvent.Distance,
                EventType = raceEvent.EventType
            };

            return View(model);
        }

        // Handle Edit Event form submission
        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            // Convert updated event details into JSON
            var json = JsonConvert.SerializeObject(model);

            // Prepare JSON content
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // Send update request to API
            var response = await _apiService
                .GetClient()
                .PutAsync(
                    $"Events/{model.EventId}",
                    content);

            // Redirect back to Event list if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Delete Event
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Send delete request to API
            var response = await _apiService
                .GetClient()
                .DeleteAsync($"Events/{id}");

            // Redirect back to Event Management page
            return RedirectToAction("Index");
        }
    }
}