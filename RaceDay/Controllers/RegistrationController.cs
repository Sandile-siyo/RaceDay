using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{//start of the namespace
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {//start of the class
        private readonly RaceDayDbContext _context;

        public RegistrationController(RaceDayDbContext context)
        {//start of the constructor
            _context = context;
        }//end of the constructor

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {//start of the method
            return await _context.Registrations.ToListAsync();
        }//end of the method

        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {//start of the method
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }//end of the method

        [HttpPost]
        public async Task<ActionResult<Registration>> CreateRegistration(Registration registration)
        {//start of the method
            _context.Registrations.Add(registration);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRegistration),
                new { id = registration.RegistrationId },
                registration);
        }//end of the method

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {//start of the method
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);

            await _context.SaveChangesAsync();

            return NoContent();
        }//end of the method
    }//end of the class
}//end of the namespace