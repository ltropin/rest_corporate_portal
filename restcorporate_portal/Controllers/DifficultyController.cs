using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Models;

namespace restcorporate_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly corporateContext _context;

        public DifficultyController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/Difficulty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulties()
        {
            return await _context.Difficulties.ToListAsync();
        }

        // GET: api/Difficulty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return difficulty;
        }

        // PUT: api/Difficulty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDifficulty(int id, Difficulty difficulty)
        {
            if (id != difficulty.Id)
            {
                return BadRequest();
            }

            _context.Entry(difficulty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DifficultyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Difficulty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Difficulty>> PostDifficulty(Difficulty difficulty)
        {
            _context.Difficulties.Add(difficulty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDifficulty", new { id = difficulty.Id }, difficulty);
        }

        // DELETE: api/Difficulty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDifficulty(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            _context.Difficulties.Remove(difficulty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DifficultyExists(int id)
        {
            return _context.Difficulties.Any(e => e.Id == id);
        }
    }
}
