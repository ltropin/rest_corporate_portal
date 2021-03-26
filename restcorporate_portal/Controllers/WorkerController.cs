using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Exceptions;
using Models = restcorporate_portal.Models;
using restcorporate_portal.ResponseModels;
using restcorporate_portal.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Controllers
{
    static class WorkersErrorsMessages
    {
        public const string WorkerNotFound = "WORKER_NOT_FOUND";
    }

    [Route("api/workers")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly Models.corporateContext _context;

        public WorkerController(Models.corporateContext context)
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
                    .ThenInclude(x => x.Department)
                .ToListAsync();


            return Ok(workers.Select(x => ResponseWorkerList.FromApiWorker(x, avatar: _getAvatar(x.AvatarUrl))).ToList());
        }

        // GET: api/workers/5
        [SwaggerOperation(
            Summary = "Детализация по работяге",
            Tags = new string[] { "Работяги" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseWorkerList))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWorkerList>> GetWorker(int id)
        {
            var worker = await _context.Workers
                .Include(x => x.Speciality)
                    .ThenInclude(x => x.Department)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (worker == null)
            {
                return NotFound(new ExceptionInfo {
                    Message = WorkersErrorsMessages.WorkerNotFound,
                    Description = "Работяга не найден"
                });
            }

            return Ok(ResponseWorkerList.FromApiWorker(worker, avatar: _getAvatar(worker.AvatarUrl)));
        }

        // GET: api/workers/profile/
        [SwaggerOperation(
            Summary = "Получить профиль работяги",
            Tags = new string[] { "Работяги" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseWorkerList))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("profile/")]
        public async Task<ActionResult<ResponseWorkerList>> GetMe()
        {
            var email = User.Identity.Name;
            var worker = await _context.Workers
                .Include(x => x.Speciality)
                    .ThenInclude(x => x.Department)
                .Include(x => x.PreviousProductsWorkers)
                    .ThenInclude(x => x.PreviousProduct)
                .Include(x => x.FavoriteProductsWorkers)
                    .ThenInclude(x => x.FavoriteProduct)
                .SingleAsync(x => x.Email == email);

            return Ok(ResponseProfile.FromApiProfile(worker,
                context: _context,
                avatar: _getAvatar(worker.AvatarUrl)
            ));
        }

        //PUT: api/Worker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWorker(int id, Models.Worker worker)
        //{
        //    if (id != worker.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(worker).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkerExists(id))
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

        //// POST: api/Worker
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        //{
        //    _context.Workers.Add(worker);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
        //}

        //// DELETE: api/Worker/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWorker(int id)
        //{
        //    var worker = await _context.Workers.FindAsync(id);
        //    if (worker == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Workers.Remove(worker);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool WorkerExists(int id)
        //{
        //    return _context.Workers.Any(e => e.Id == id);
        //}

        Models.File _getAvatar(string avatarUrl)
        {
            if (avatarUrl != null)
            {
                var fullFileName = avatarUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                return _context.Files.SingleOrDefault(x => x.Name == fullFileName);
            }
            else
            {
                return null;
            }
        }
    }
}
