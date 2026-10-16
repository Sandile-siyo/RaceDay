using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{//start of the namespace
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {//start of the class
        private readonly RaceDayDbContext _context;

        public UserController(RaceDayDbContext context)
        {//start of the constructor
            _context = context;
        }//end of constructor

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {//start of the method
            return await _context.Users.ToListAsync();
        }//end of the method

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {//start of the method
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }//end of the method

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {//start of the method
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }//end of the method
    }//end of the class
}//end of the namespace