using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RegistrationController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        [HttpPost]
        public async Task<ActionResult<Registration>> CreateRegistration(Registration registration)
        {
            _context.Registrations.Add(registration);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRegistration),
                new { id = registration.RegistrationId },
                registration);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}