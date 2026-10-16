using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            return raceEvent;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event raceEvent)
        {
            _context.Events.Add(raceEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEvent),
                new { id = raceEvent.EventId },
                raceEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event raceEvent)
        {
            if (id != raceEvent.EventId)
            {
                return BadRequest();
            }

            _context.Entry(raceEvent).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(raceEvent);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}