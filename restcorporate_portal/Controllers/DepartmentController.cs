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
using restcorporate_portal.RequestModels;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Controllers
{
    static class DepartmentErrorsMessages
    {
        public const string DepartmentNotFound = "DEPARTMENT_NOT_FOUND";
        //public const string FileNotFoundOrUploadEmptyFile = "FILE_NOT_FOUND_OR_UPLOAD_EMPTY_FILE";
        //public const string QueryParametrsMustBeNotNull = "QUERY_PARAMETRS_MUST_BE_NOT_NULL";
    }

    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly corporateContext _context;

        public DepartmentController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/department
        [SwaggerOperation(
            Summary = "Получение списка всех отделов",
            Tags = new string[] { "Отделы" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<Department>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        // GET: api/department/5
        [SwaggerOperation(
            Summary = "Получение отдела по id",
            Tags = new string[] { "Отделы" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Department))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound(new ExceptionInfo {
                    Message = DepartmentErrorsMessages.DepartmentNotFound,
                    Description = "Отдел не найден по укзанному id"
                });
            }

            return department;
        }

        //// PUT: api/department/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDepartment(int id, Department department)
        //{
        //    if (id != department.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(department).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentExists(id))
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

        // POST: api/department
        [SwaggerOperation(
            Summary = "Записать отдел в базу данных",
            Tags = new string[] { "Отделы" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Department))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(PostDepartment postDepartment)
        {
            if (postDepartment == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = DepartmentErrorsMessages.DepartmentNotFound,
                    Description = "Отдел не найден по укзанному id"
                });
            }

            var newDepartment = new Department { Name = postDepartment.Name };
            _context.Departments.Add(newDepartment);
            await _context.SaveChangesAsync();

            return Ok(newDepartment);
        }

        // DELETE: api/department/5
        [SwaggerOperation(
            Summary = "Удаляет отдел из базы данных",
            Tags = new string[] { "Отделы" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Department))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = DepartmentErrorsMessages.DepartmentNotFound,
                    Description = "Отдел не найден по укзанному id"
                });
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
