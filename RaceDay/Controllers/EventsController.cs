using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;
using RaceDay.Services;

namespace RaceDay.Controllers
{//start of the namespace

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {//start of the class

        private readonly RaceDayDbContext _context;
        private readonly BlobStorageService _blobStorageService;

        public EventsController(
            RaceDayDbContext context,
            BlobStorageService blobStorageService)
        {//start of the constructor

            _context = context;
            _blobStorageService = blobStorageService;

        }//end of the constructor


        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {//start of the method

            return await _context.Events.ToListAsync();

        }//end of the method


        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {//start of the method

            var raceEvent = await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            return raceEvent;

        }//end of the method


        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event raceEvent)
        {//start of the method

            _context.Events.Add(raceEvent);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEvent),
                new { id = raceEvent.EventId },
                raceEvent);

        }//end of the method


        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(
            int id,
            Event raceEvent)
        {//start of the method

            if (id != raceEvent.EventId)
            {
                return BadRequest(
                    "The Event ID does not match.");
            }

            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound(
                    "The event could not be found.");
            }

            existingEvent.EventName =
                raceEvent.EventName;

            existingEvent.EventDescription =
                raceEvent.EventDescription;

            existingEvent.EventDate =
                raceEvent.EventDate;

            existingEvent.Location =
                raceEvent.Location;

            existingEvent.Distance =
                raceEvent.Distance;

            existingEvent.EventType =
                raceEvent.EventType;

            // Keep the existing image when no new URL is supplied.
            if (!string.IsNullOrWhiteSpace(raceEvent.ImageUrl))
            {
                existingEvent.ImageUrl =
                    raceEvent.ImageUrl;
            }

            await _context.SaveChangesAsync();

            return NoContent();

        }//end of the method


        // POST: api/Events/5/image
        [HttpPost("{id}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadEventImage(
            int id,
            IFormFile image)
        {//start of the method

            var raceEvent =
                await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound(
                    "The event could not be found.");
            }

            if (image == null || image.Length == 0)
            {
                return BadRequest(
                    "Please select an image to upload.");
            }

            // Maximum image size: 5 MB.
            const long maximumFileSize =
                5 * 1024 * 1024;

            if (image.Length > maximumFileSize)
            {
                return BadRequest(
                    "The image must not be larger than 5 MB.");
            }

            string extension =
                Path.GetExtension(image.FileName)
                    .ToLowerInvariant();

            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(
                    "Only JPG, JPEG, PNG and WEBP images are allowed.");
            }

            string[] allowedContentTypes =
            {
                "image/jpeg",
                "image/png",
                "image/webp"
            };

            if (!allowedContentTypes.Contains(
                    image.ContentType.ToLowerInvariant()))
            {
                return BadRequest(
                    "The selected file is not a supported image.");
            }

            await using Stream imageStream =
                image.OpenReadStream();

            string imageUrl =
                await _blobStorageService.UploadImageAsync(
                    imageStream,
                    image.FileName,
                    image.ContentType);

            raceEvent.ImageUrl = imageUrl;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Event image uploaded successfully.",
                eventId = raceEvent.EventId,
                imageUrl = raceEvent.ImageUrl
            });

        }//end of the method


        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {//start of the method

            var raceEvent =
                await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(raceEvent);

            await _context.SaveChangesAsync();

            return NoContent();

        }//end of the method

    }//end of the class

}//end of the namespace