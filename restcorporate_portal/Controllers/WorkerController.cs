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
    [Route("api/workers")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly corporateContext _context;

        public WorkerController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/workers
        [SwaggerOperation(
            Summary = "Получение списка работяг",
            Tags = new string[] { "Работяги" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseWorkerList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseWorkerList>>> GetWorkers()
        {
            var workers = await _context.Workers
                .Include(x => x.Speciality)
                .ToListAsync();


            return Ok(workers.Select(x => ResponseWorkerList.FromApiWorker(x)).ToList());
        }

        // GET: api/workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // PUT: api/Worker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, Worker worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Worker
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
        }

        // DELETE: api/Worker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}
