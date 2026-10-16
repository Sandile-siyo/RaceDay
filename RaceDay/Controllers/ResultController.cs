using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Data;
using RaceDay.Models;

namespace RaceDay.Controllers
{//start of the namespace
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {//start of the class
        private readonly RaceDayDbContext _context;

        public ResultController(RaceDayDbContext context)
        {//start of the constructor
            _context = context;
        }//end of the constructor

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {//start of the method
            return await _context.Results.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {//start of the method
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }//end of the method

        [HttpPost]
        public async Task<ActionResult<Result>> CreateResult(Result result)
        {//start of the method
            _context.Results.Add(result);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetResult),
                new { id = result.ResultId },
                result);
        }//end of the method

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, Result result)
        {//start of the method
            if (id != result.ResultId)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }//end of the method

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {//start of the method
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);

            await _context.SaveChangesAsync();

            return NoContent();
        }//end of the method
    }//end of the class
}//end of the namespace