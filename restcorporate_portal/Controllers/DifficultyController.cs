using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Exceptions;
using restcorporate_portal.Models;
using restcorporate_portal.ResponseModels;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Controllers
{
    static class DifficultyErrorsMessages
    {
        public const string DifficultyNotFound = "DIFFICULTY_NOT_FOUND";
    }

    [Route("api/difficulties")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly corporateContext _context;

        public DifficultyController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/difficulties
        [SwaggerOperation(
            Summary = "Просмотр задачи",
            Tags = new string[] { "Сложность задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseDifficultyList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDifficultyList>>> GetDifficulties()
        {
            var difficulties = await _context.Difficulties.ToListAsync();
            return Ok(difficulties.Select(x => ResponseDifficultyList.FromApiDifficulty(x)).ToList());
        }

        // GET: api/difficulties/5
        [SwaggerOperation(
            Summary = "Детальный просмотр сложности",
            Tags = new string[] { "Сложность задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseDifficultyList))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);

            if (difficulty == null)
            {
                return NotFound(new ExceptionInfo {
                    Message = DifficultyErrorsMessages.DifficultyNotFound,
                    Description = "Сложность не найдена"
                });
            }

            return Ok(ResponseDifficultyList.FromApiDifficulty(difficulty));
        }

        // PUT: api/Difficulty/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDifficulty(int id, Difficulty difficulty)
        //{
        //    if (id != difficulty.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(difficulty).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DifficultyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Difficulty
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Difficulty>> PostDifficulty(Difficulty difficulty)
        //{
        //    _context.Difficulties.Add(difficulty);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDifficulty", new { id = difficulty.Id }, difficulty);
        //}

        //// DELETE: api/Difficulty/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDifficulty(int id)
        //{
        //    var difficulty = await _context.Difficulties.FindAsync(id);
        //    if (difficulty == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Difficulties.Remove(difficulty);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DifficultyExists(int id)
        //{
        //    return _context.Difficulties.Any(e => e.Id == id);
        //}
    }
}
