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
        public const string TaskOrStatusNotFound = "TASK_OR_STATUS_NOT_FOUND";
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
        public async Task<ActionResult<IEnumerable<ResponseTaskList>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(x => x.Status)
                .Include(x => x.Difficulty)
                .Include(x => x.Priorirty)
                .Include(x => x.Worker)
                    .ThenInclude(x => x.Speciality)
                .ToListAsync();

            return Ok(tasks.Select(x =>
            {
                var isExpired = DateTime.Now > x.ExpirationDate;
                var iconName = x.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                var icon = isExpired ?
                    _context.Files.SingleOrDefault(y => y.Name == "Expired.png") :
                    _context.Files.SingleOrDefault(y => y.Name == iconName);
                var author = _context.Workers
                    .Include(x => x.Speciality)
                    .SingleOrDefault(y => y.Id == x.AuthorId);


                return ResponseTaskList.FromApiTask(x,
                    author: author,
                    icon: icon,
                    avatarWorker: _getAvatar(x.Worker.AvatarUrl),
                    avatarAuthor: _getAvatar(author.AvatarUrl)
                );
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
                    .ThenInclude(x => x.Speciality)
                .Include(x => x.Priorirty)
                .Include(x => x.Difficulty)
                .Include(x => x.Status)
                .SingleOrDefaultAsync(x => x.Id == id);


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
            var author = _context.Workers
                .Include(x => x.Speciality)
                .SingleOrDefault(x => x.Id == task.AuthorId);

            return Ok(ResponseTaskDetail.FromApiTask(task,
                author: author,
                icon: icon,
                file: file,
                avatarWorker: _getAvatar(task.Worker.AvatarUrl),
                avatarAuthor: _getAvatar(author.AvatarUrl)
            ));
        }

        // GET: api/tasks/me/5
        [SwaggerOperation(
            Summary = "Просмотр моей задачи",
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
                    .ThenInclude(x => x.Speciality)
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
            var author = _context.Workers
                .Include(x => x.Speciality)
                .SingleOrDefault(x => x.Id == task.AuthorId);
            return Ok(ResponseTaskDetail.FromApiTask(task,
                author: author,
                icon: icon,
                file: file,
                avatarWorker: _getAvatar(task.Worker.AvatarUrl),
                avatarAuthor: _getAvatar(author.AvatarUrl)
            ));
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
        public async Task<ActionResult<IEnumerable<ResponseTaskList>>> GetMyTasks()
        {
            var email = User.Identity.Name;
            var tasks = await _context.Tasks
                .Include(x => x.Status)
                .Include(x => x.Difficulty)
                .Include(x => x.Priorirty)
                .Include(x => x.Worker)
                    .ThenInclude(x => x.Speciality)
                .Where(x => x.Worker.Email == email)
                .ToListAsync();

            return Ok(tasks.Select(x =>
            {
                var isExpired = DateTime.Now > x.ExpirationDate;
                var iconName = x.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                var icon = isExpired ?
                    _context.Files.SingleOrDefault(y => y.Name == "Expired.png") :
                    _context.Files.SingleOrDefault(y => y.Name == iconName);
                var author = _context.Workers
                    .Include(x => x.Speciality)
                    .SingleOrDefault(y => y.Id == x.AuthorId);
                return ResponseTaskList.FromApiTask(x,
                    author: author,
                    icon: icon,
                    avatarWorker: _getAvatar(x.Worker.AvatarUrl),
                    avatarAuthor: _getAvatar(author.AvatarUrl)
                );
            }).ToList());
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
        [HttpGet("me/filter")]
        public async Task<ActionResult<IEnumerable<ResponseTaskList>>> GetMyTasksStatus([FromQuery] string status)
        {
            var email = User.Identity.Name;
            var tasks = await _context.Tasks
                .Include(x => x.Status)
                .Include(x => x.Difficulty)
                .Include(x => x.Priorirty)
                .Include(x => x.Worker)
                    .ThenInclude(x => x.Speciality)
                .Where(x => x.Worker.Email == email && x.Status.Name == status && x.ExpirationDate < DateTime.Now)
                .ToListAsync();

            return Ok(tasks.Select(x =>
            {
                var iconName = x.Status.IconUrl.Replace(Constans.ApiUrl + Constans.FileDownloadPart, string.Empty);
                var icon = _context.Files.SingleOrDefault(y => y.Name == iconName);
                var author = _context.Workers
                    .Include(x => x.Speciality)
                    .SingleOrDefault(y => y.Id == x.AuthorId);
                return ResponseTaskList.FromApiTask(x,
                    author: author,
                    icon: icon,
                    avatarWorker: _getAvatar(x.Worker.AvatarUrl),
                    avatarAuthor: _getAvatar(author.AvatarUrl)
                );
            }).ToList());
        }

        //PUT: api/tasks/5
        [SwaggerOperation(
            Summary = "Изменение статуса задачи",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseTaskList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("status/")]
        public async Task<IActionResult> PutTask(RequestTaskPut requestTaskPut)
        {
            var currentStatus = await _context.Statuses.SingleOrDefaultAsync(x => x.Id == requestTaskPut.NewStatusId);
            var currentTask = await _context.Tasks.SingleOrDefaultAsync(x => x.Id == requestTaskPut.TaskId);
            var email = User.Identity.Name;
            var currentUser = await _context.Workers.SingleAsync(x => x.Email == email);

            if (currentTask == null || currentStatus == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = TasksErrorsMessages.TaskOrStatusNotFound,
                    Description = "Невозможно изменить статус для этой задачи. Обновите страницу."
                });
            }

            if(requestTaskPut.NewStatusId == 1)
            {
                if (DateTime.Now > currentTask.ExpirationDate)
                {
                    currentTask.RewardCoins = (int)Math.Ceiling(currentTask.RewardCoins * 0.7);
                    currentTask.RewardXp = (int)Math.Ceiling(currentTask.RewardXp * 0.7);
                   
                }

                currentUser.Balance += currentTask.RewardCoins;
                currentUser.Experience += currentTask.RewardXp;
                _context.Workers.Update(currentUser);
                await _context.SaveChangesAsync();
            }

            

            currentTask.StatusId = requestTaskPut.NewStatusId;

            _context.Tasks.Update(currentTask);

            await _context.SaveChangesAsync();

            return Ok();
        }

        //PUT: api/file/5
        [SwaggerOperation(
            Summary = "Изменение приложенного файла задачи",
            Tags = new string[] { "Задачи" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseTaskList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("file/")]
        public async Task<IActionResult> PutFile(RequestFilePut requestFilePut)
        {
            var currentFile = await _context.Files.SingleOrDefaultAsync(x => Constans.ApiUrl + Constans.FileDownloadPart + x.Name == requestFilePut.NewFileUrl);
            var currentTask = await _context.Tasks.SingleOrDefaultAsync(x => x.Id == requestFilePut.TaskId);
            var email = User.Identity.Name;
            var currentUser = await _context.Workers.SingleAsync(x => x.Email == email);

            if (currentTask == null || currentFile == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = TasksErrorsMessages.TaskOrStatusNotFound,
                    Description = "Невозможно изменить приложенный файл для этой задачи. Обновите страницу."
                });
            }

            currentTask.AttachedFileUrl = requestFilePut.NewFileUrl;

            _context.Tasks.Update(currentTask);

            await _context.SaveChangesAsync();

            return Ok();
        }


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
                AttachedFileUrl = Constans.ApiUrl + Constans.FileDownloadPart + file.Name,
                Status = await _context.Statuses.SingleAsync(x => x.Name == "to_do"),
                WorkerId = createTask.WorkerId,
                AuthorId = author.Id
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            var createdTask = await _context.Tasks
                .Include(x => x.Worker)
                    .ThenInclude(x => x.Speciality)
                .Include(x => x.Status)
                .Include(x => x.Priorirty)
                .Include(x => x.Difficulty)
                .SingleAsync(x => x.Id == newTask.Id);
            var icon = await _context.Files.SingleOrDefaultAsync(x => x.Name == "Draft.png");

            return Ok(ResponseTaskDetail.FromApiTask(createdTask,
                icon: icon,
                author: author,
                file: file,
                avatarWorker: _getAvatar(createdTask.Worker.AvatarUrl),
                avatarAuthor: _getAvatar(author.AvatarUrl)
            ));
        }

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
