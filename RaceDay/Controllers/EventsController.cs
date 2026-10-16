using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{//start of the namespace
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {//start of the class
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {//start of the constructor
            _context = context;
        }//end of the constructor

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {//start of the method
            return await _context.Events.ToListAsync();
        }//end of the method

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

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event raceEvent)
        {//start of the method
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
        }//end of the method
    }//end of the class
}//end of the namespace