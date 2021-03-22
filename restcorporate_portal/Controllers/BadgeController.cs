using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Controllers
{
    [Route("api/badges")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly corporateContext _context;

        public BadgeController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/Badge
        [SwaggerOperation(
            Summary = "",
            Tags = new string []{ "Награды" }
        )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges()
        {
            return await _context.Badges.ToListAsync();
        }

        // GET: api/Badge/5
        [SwaggerOperation(
            Summary = "",
            Tags = new string[] { "Награды" }
        )]
        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(int id)
        {
            var badge = await _context.Badges.FindAsync(id);

            if (badge == null)
            {
                return NotFound();
            }

            return badge;
        }

        // PUT: api/Badge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [SwaggerOperation(
            Summary = "",
            Tags = new string[] { "Награды" }
        )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBadge(int id, Badge badge)
        {
            if (id != badge.Id)
            {
                return BadRequest();
            }

            _context.Entry(badge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(id))
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

        // POST: api/Badge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [SwaggerOperation(
            Summary = "",
            Tags = new string[] { "Награды" }
        )]
        [HttpPost]
        public async Task<ActionResult<Badge>> PostBadge(Badge badge)
        {
            _context.Badges.Add(badge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBadge", new { id = badge.Id }, badge);
        }

        // DELETE: api/Badge/5
        [SwaggerOperation(
            Summary = "",
            Tags = new string[] { "Награды" }
        )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }

            _context.Badges.Remove(badge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BadgeExists(int id)
        {
            return _context.Badges.Any(e => e.Id == id);
        }
    }
}
