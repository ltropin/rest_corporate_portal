using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Exceptions;
using restcorporate_portal.RequestModels;
using restcorporate_portal.ResponseModels;
using restcorporate_portal.Utils;
using Swashbuckle.AspNetCore.Annotations;
using Models = restcorporate_portal.Models;

namespace restcorporate_portal.Controllers
{
    static class TasksErrorsMessages
    {
        public const string TaskNotFound = "TASK_NOT_FOUND";
    }

    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly Models.corporateContext _context;

        public TaskController(Models.corporateContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [SwaggerOperation(
            Summary = "Получение списка задач с статусами, сложностью, приорететом",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseTaskList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(x => x.Status)
                .Include(x => x.Difficulty)
                .Include(x => x.Priorirty)
                .Include(x => x.Worker)
                .ToListAsync();

            return Ok(tasks.Select(x => {
                var isExpired = DateTime.Now > x.ExpirationDate;
                var iconName = x.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                var icon = isExpired ?
                    _context.Files.SingleOrDefault(y => y.Name == "Expired.png") :
                    _context.Files.SingleOrDefault(y => y.Name == iconName);
                var author = _context.Workers.SingleOrDefault(y => y.Id == x.AuthorId);
                return ResponseTaskList.FromApiTask(x, author: author, icon: icon);
            }).ToList());
        }

        // GET: api/tasks/5
        [SwaggerOperation(
            Summary = "Просмотр задачи",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseTaskDetail))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseTaskDetail>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(x => x.Worker)
                .Include(x => x.Priorirty)
                .Include(x => x.Difficulty)
                .Include(x => x.Status)
                .SingleOrDefaultAsync(x => x.Id == id);


            if (task == null)
            {
                return NotFound(new ExceptionInfo {
                    Message = TasksErrorsMessages.TaskNotFound,
                    Description = "Задача не найдена",
                });
            }

            var fileName = task?.AttachedFileUrl?.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);

            var file = fileName != null ? await _context.Files.SingleOrDefaultAsync(x => x.Name == fileName) : null;
            var isExpired = DateTime.Now > task.ExpirationDate;
            var iconName = task.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
            var icon = isExpired ?
                await _context.Files.SingleOrDefaultAsync(x => x.Name == "Expired.png") :
                await _context.Files.SingleOrDefaultAsync(x => x.Name == iconName);
            var author = _context.Workers.SingleOrDefault(x => x.Id == task.AuthorId);
            return Ok(ResponseTaskDetail.FromApiTask(task, author: author, icon: icon, file: file));
        }

        // GET: api/tasks/me/5
        [SwaggerOperation(
            Summary = "Просмотр задачи",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseTaskDetail))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        //[Route("me")]
        [HttpGet("me/{id}")]
        public async Task<ActionResult<ResponseTaskDetail>> GetMyTask(int id)
        {
            var email = User.Identity.Name;
            var task = await _context.Tasks
                .Include(x => x.Worker)
                .Include(x => x.Priorirty)
                .Include(x => x.Difficulty)
                .Include(x => x.Status)
                .SingleOrDefaultAsync(x => x.Id == id && x.Worker.Email == email);


            if (task == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = TasksErrorsMessages.TaskNotFound,
                    Description = "Задача не найдена",
                });
            }

            var fileName = task?.AttachedFileUrl?.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);

            var file = fileName != null ? await _context.Files.SingleOrDefaultAsync(x => x.Name == fileName) : null;
            var isExpired = DateTime.Now > task.ExpirationDate;
            var iconName = task.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
            var icon = isExpired ?
                await _context.Files.SingleOrDefaultAsync(x => x.Name == "Expired.png") :
                await _context.Files.SingleOrDefaultAsync(x => x.Name == iconName);
            var author = _context.Workers.SingleOrDefault(x => x.Id == task.AuthorId);
            return Ok(ResponseTaskDetail.FromApiTask(task, author: author, icon: icon, file: file));
        }

        // GET: api/tasks/me
        [SwaggerOperation(
            Summary = "Получение моего списка задач с статусами, сложностью, приорететом",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseTaskList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        //[Route("me")]
        [HttpGet("me/")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetMyTasks()
        {
            var email = User.Identity.Name;
            var tasks = await _context.Tasks
                .Include(x => x.Status)
                .Include(x => x.Difficulty)
                .Include(x => x.Priorirty)
                .Include(x => x.Worker)
                .Where(x => x.Worker.Email == email)
                .ToListAsync();

            return Ok(tasks.Select(x => {
                var isExpired = DateTime.Now > x.ExpirationDate;
                var iconName = x.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                var icon = isExpired ?
                    _context.Files.SingleOrDefault(y => y.Name == "Expired.png") :
                    _context.Files.SingleOrDefault(y => y.Name == iconName);
                var author = _context.Workers.SingleOrDefault(y => y.Id == x.AuthorId);
                return ResponseTaskList.FromApiTask(x, author: author, icon: icon);
            }).ToList());
        }

        // PUT: api/Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTask(int id, Models.Task task)
        //{
        //    if (id != task.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(task).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TaskExists(id))
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

        // POST: api/tasks/me/
        [SwaggerOperation(
            Summary = "Назначение записи",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseTaskDetail))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Ошибка валидации", type: typeof(List<ValidatorException>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("me/")]
        public async Task<ActionResult<ResponseTaskDetail>> PostTask([FromBody] CreateTask createTask)
        {
            if (string.IsNullOrEmpty(createTask.Title))
            {
                return UnprocessableEntity(new List<ValidatorException> {
                    new ValidatorException
                    {
                        Field = "Title",
                        Message = "TITLE_IS_REQUIRED",
                        Description = "Поле Название обязательное для заполнения"
                    }
                });
            }


            var email = User.Identity.Name;
            var author = await _context.Workers.SingleOrDefaultAsync(x => x.Email == email);
            var file = await _context.Files.SingleOrDefaultAsync(x => x.Id == createTask.FileId);

            var newTask = new Models.Task
            {
                Title = createTask.Title,
                Description = createTask.Description,
                ExpirationDate = DateTime.ParseExact(createTask.ExpiredDate, "dd.MM.yyyy", null),
                RewardCoins = createTask.RewardCoins,
                RewardXp = createTask.RewardXp,
                DifficultyId = createTask.DifficultyId,
                PriorirtyId = createTask.PriorityId,
                Status = await _context.Statuses.SingleAsync(x => x.Name == "to_do"),
                WorkerId = createTask.WorkerId
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            var createdTask = await _context.Tasks.SingleAsync(x => x.Id == newTask.Id);
            var icon = await _context.Files.SingleOrDefaultAsync(x => x.Name == "Draft.png");

            return Ok(ResponseTaskDetail.FromApiTask(createdTask, icon: icon, author: author, file: file));
        }

        //// DELETE: api/Task/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTask(int id)
        //{
        //    var task = await _context.Tasks.FindAsync(id);
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tasks.Remove(task);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TaskExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.Id == id);
        //}
    }
}
