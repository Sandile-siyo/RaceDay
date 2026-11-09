using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;
using System.Net.Http.Headers;
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


        // =========================================================
        // INDEX
        // =========================================================

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
            else
            {
                TempData["ErrorMessage"] =
                    "The events could not be loaded.";
            }

            return View(events);
        }


        // =========================================================
        // CREATE EVENT
        // =========================================================

        // Display Create Event page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // Receive and process the Create Event form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateEventViewModel model)
        {
            // Return the form if model validation failed
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // Validate the image if one was selected
            if (model.ImageFile != null)
            {
                bool imageIsValid =
                    ValidateImage(model.ImageFile);

                if (!imageIsValid)
                {
                    return View(model);
                }
            }


            // Create an object containing only event details
            var eventData = new
            {
                model.EventName,
                model.EventDescription,
                model.EventDate,
                model.Location,
                model.Distance,
                model.EventType,

                // Image URL will be added after Azure upload
                ImageUrl = string.Empty
            };


            // Convert event details into JSON
            string eventJson =
                JsonConvert.SerializeObject(eventData);

            var eventContent = new StringContent(
                eventJson,
                Encoding.UTF8,
                "application/json");


            // Send the new event to the API
            var createResponse = await _apiService
                .GetClient()
                .PostAsync(
                    "Events",
                    eventContent);


            // Check whether the event was created
            if (!createResponse.IsSuccessStatusCode)
            {
                string apiError =
                    await createResponse.Content
                        .ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    "The event could not be created. " +
                    apiError);

                return View(model);
            }


            // Read the event returned by the API
            string createdEventJson =
                await createResponse.Content
                    .ReadAsStringAsync();

            var createdEvent =
                JsonConvert.DeserializeObject<Event>(
                    createdEventJson);


            // Make sure the API returned the new Event Id
            if (createdEvent == null ||
                createdEvent.EventId <= 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "The event was created, but its ID " +
                    "was not returned.");

                return View(model);
            }


            // Upload the image after creating the event
            if (model.ImageFile != null)
            {
                bool imageUploadSuccessful =
                    await UploadEventImageAsync(
                        createdEvent.EventId,
                        model.ImageFile);

                if (!imageUploadSuccessful)
                {
                    TempData["WarningMessage"] =
                        "The event was created, but the image " +
                        "could not be uploaded.";

                    return RedirectToAction(
                        nameof(Index));
                }
            }


            TempData["SuccessMessage"] =
                "Event created successfully.";

            return RedirectToAction(
                nameof(Index));
        }


        // =========================================================
        // EDIT EVENT
        // =========================================================

        // Display Edit Event page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the selected event from the API
            var response = await _apiService
                .GetClient()
                .GetAsync($"Events/{id}");


            // Return 404 if the event does not exist
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }


            // Read the event information
            string json =
                await response.Content
                    .ReadAsStringAsync();

            var raceEvent =
                JsonConvert.DeserializeObject<Event>(
                    json);


            if (raceEvent == null)
            {
                return NotFound();
            }


            // Convert Event into EditEventViewModel
            var model = new EditEventViewModel
            {
                EventId =
                    raceEvent.EventId,

                EventName =
                    raceEvent.EventName,

                EventDescription =
                    raceEvent.EventDescription,

                EventDate =
                    raceEvent.EventDate,

                Location =
                    raceEvent.Location,

                Distance =
                    raceEvent.Distance,

                EventType =
                    raceEvent.EventType,

                ImageUrl =
                    raceEvent.ImageUrl
            };


            return View(model);
        }


        // Receive and process the Edit Event form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            EditEventViewModel model)
        {
            // Return the form if model validation failed
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // Validate the new image if one was selected
            if (model.ImageFile != null)
            {
                bool imageIsValid =
                    ValidateImage(model.ImageFile);

                if (!imageIsValid)
                {
                    return View(model);
                }
            }


            // Create the event object sent to the API
            var updatedEvent = new
            {
                model.EventId,
                model.EventName,
                model.EventDescription,
                model.EventDate,
                model.Location,
                model.Distance,
                model.EventType,

                // Keep the current image URL
                model.ImageUrl
            };


            // Convert the updated event into JSON
            string json =
                JsonConvert.SerializeObject(
                    updatedEvent);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");


            // Send the updated event to the API
            var updateResponse = await _apiService
                .GetClient()
                .PutAsync(
                    $"Events/{model.EventId}",
                    content);


            // Check whether the update succeeded
            if (!updateResponse.IsSuccessStatusCode)
            {
                string apiError =
                    await updateResponse.Content
                        .ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    "The event could not be updated. " +
                    apiError);

                return View(model);
            }


            // Upload a replacement image if selected
            if (model.ImageFile != null)
            {
                bool imageUploadSuccessful =
                    await UploadEventImageAsync(
                        model.EventId,
                        model.ImageFile);

                if (!imageUploadSuccessful)
                {
                    TempData["WarningMessage"] =
                        "The event details were updated, but " +
                        "the new image could not be uploaded.";

                    return RedirectToAction(
                        nameof(Index));
                }
            }


            TempData["SuccessMessage"] =
                "Event updated successfully.";

            return RedirectToAction(
                nameof(Index));
        }


        // =========================================================
        // IMAGE VALIDATION HELPER
        // =========================================================

        // Validate image size, extension and content type
        private bool ValidateImage(
            IFormFile imageFile)
        {
            // Maximum image size is 5 MB
            const long maximumFileSize =
                5 * 1024 * 1024;


            // Check whether the image is empty
            if (imageFile.Length == 0)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "The selected image is empty.");

                return false;
            }


            // Check the image size
            if (imageFile.Length > maximumFileSize)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "The image must not be larger than 5 MB.");

                return false;
            }


            // Get the image extension
            string extension =
                Path.GetExtension(
                    imageFile.FileName)
                    .ToLowerInvariant();


            // Allowed image extensions
            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };


            // Check the image extension
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Only JPG, JPEG, PNG and WEBP " +
                    "images are allowed.");

                return false;
            }


            // Allowed content types
            string[] allowedContentTypes =
            {
                "image/jpeg",
                "image/png",
                "image/webp"
            };


            // Read the content type safely
            string contentType =
                imageFile.ContentType
                    .ToLowerInvariant();


            // Check the image content type
            if (!allowedContentTypes.Contains(
                    contentType))
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "The selected file is not a " +
                    "supported image.");

                return false;
            }


            return true;
        }


        // =========================================================
        // AZURE IMAGE UPLOAD HELPER
        // =========================================================

        // Send the selected image to the RaceDay API
        private async Task<bool> UploadEventImageAsync(
            int eventId,
            IFormFile imageFile)
        {
            // Create multipart form content
            using var multipartContent =
                new MultipartFormDataContent();


            // Open the selected image
            await using var imageStream =
                imageFile.OpenReadStream();


            // Add the image stream to the HTTP request
            using var imageContent =
                new StreamContent(imageStream);


            // Use the image content type
            string contentType =
                string.IsNullOrWhiteSpace(
                    imageFile.ContentType)
                    ? "application/octet-stream"
                    : imageFile.ContentType;


            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse(
                    contentType);


            // The name "image" must match:
            // IFormFile image
            // in the API EventsController
            multipartContent.Add(
                imageContent,
                "image",
                Path.GetFileName(
                    imageFile.FileName));


            // Send the image to the API
            var response = await _apiService
                .GetClient()
                .PostAsync(
                    $"Events/{eventId}/image",
                    multipartContent);


            return response.IsSuccessStatusCode;
        }
    }
}