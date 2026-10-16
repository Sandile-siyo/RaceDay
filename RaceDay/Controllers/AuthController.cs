using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Data;
using RaceDay.DTOs;
using RaceDay.Models;

namespace RaceDay.Controllers
{//start of the namespace
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {//start of the class
        private readonly RaceDayDbContext _context;

        public AuthController(RaceDayDbContext context)
        {//start of the constructor
            _context = context;
        }//end of the constructor

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {//start of the method
            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {//start of the method
            var user = _context.Users
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(dto.Password,
                user.PasswordHash);

            if (!validPassword)
            {
                return BadRequest("Invalid password");
            }

            return Ok("Login Successful");
        }
    }
}