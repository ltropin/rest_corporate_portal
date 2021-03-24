using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Models = restcorporate_portal.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Configuration;
using restcorporate_portal.Exceptions;
using Microsoft.AspNetCore.Authorization;
using restcorporate_portal.Utils;
using restcorporate_portal.ResponseModels;

namespace restcorporate_portal.Controllers
{
    static class ErrorsMessages
    {
        public const string FileNotFound = "FILE_NOT_FOUND";
        public const string FileNotFoundOrUploadEmptyFile = "FILE_NOT_FOUND_OR_UPLOAD_EMPTY_FILE";
        public const string QueryParametrsMustBeNotNull = "QUERY_PARAMETRS_MUST_BE_NOT_NULL";
    }


    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly IConfiguration _configuration;
        private readonly Models.corporateContext _context;
        private string _basePath { get => _enviroment.ContentRootPath + "/Upload/"; }

        public FilesController(IWebHostEnvironment environment, Models.corporateContext context, IConfiguration configuration)
        {
            _enviroment = environment;
            _context = context;
            _configuration = configuration;
        }

        // GET: api/files
        [SwaggerOperation(
            Summary = "Получает список всех записей о файлах",
            Tags = new string[] { "Файлы" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseFileList>))]
        [HttpGet]
        public async Task<ActionResult<List<ResponseFileList>>> GetFiles()
        {
            var files = await _context.Files.Select(el => ResponseFileList.FromApiFile(el)).ToListAsync();
            
            return Ok(files);
        }

        // GET: api/files/download?id={id}&filename={filename}
        [SwaggerOperation(
            Summary = "Получение файла по названию и расширению или id",
            Tags = new string[] { "Файлы" }
        )]
        [Route("download")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(byte[]))]
        [HttpGet]
        public async Task<ActionResult<byte[]>> GetFileDownload([FromQuery] string filename, [FromQuery] int? id)
        {
            if (filename == null && id == null)
            {
                return NotFound(new ExceptionInfo {
                    Message = ErrorsMessages.QueryParametrsMustBeNotNull,
                    Description = "Параметры не должны быть null"
                });
            }
            //var files = await _context.Files.ToListAsync();
            var file = await _context.Files.SingleOrDefaultAsync(file => filename != null  && file.Name == filename ||
                id.HasValue && file.Id == id.Value); ;

            if (file == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ErrorsMessages.FileNotFound,
                    Description = "Файл не найден"
                });
            }

            var isFileExist = IO.File.Exists(_basePath + file.Name);

            if (isFileExist)
            {
                var filePath = _basePath + file.Name;
                var fileBytes = await IO.File.ReadAllBytesAsync(_basePath + file.Name);
                new FileExtensionContentTypeProvider().TryGetContentType(IO.Path.GetFileName(filePath), out var contentType);
                return File(fileBytes, contentType ?? "application/octet-stream", file.Name);
            }
            else
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ErrorsMessages.FileNotFound,
                    Description = "Файл не найден"
                });
            }
        }

        // GET: api/files/info/?id={id}&filename={filename}
        [SwaggerOperation(
            Summary = "Получает информацию о файле из базы данных по Id",
            Tags = new string[] { "Файлы" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(ResponseFileList))]
        [Route("info")]
        [HttpGet]
        public async Task<ActionResult<Models.File>> GetFileInfoById([FromQuery] int? id, [FromQuery] string filename)
        {
            if (id == null && filename == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ErrorsMessages.QueryParametrsMustBeNotNull,
                    Description = "Параметры не должны быть null"
                });
            }


            var file = await _context.Files.SingleOrDefaultAsync(file => filename != null && file.Name == filename ||
                id.HasValue && file.Id == id.Value);

            if (file == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ErrorsMessages.FileNotFound,
                    Description = "Файл не найден"
                });
            }
            
            return Ok(ResponseFileList.FromApiFile(file));
        }

        // POST: api/files
        [SwaggerOperation(
            Summary = "Загружает картинку на сервер и создает запись в базе данных",
            Tags = new string[] { "Файлы" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Models.File))]
        [HttpPost]
        public async Task<ActionResult<Models.File>> PostFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (!IO.Directory.Exists(_basePath))
                {
                    IO.Directory.CreateDirectory(_basePath);
                }

                var splittedFileName = file.FileName.Split('.');
                var extension = splittedFileName.Count() > 1 ? splittedFileName.Last() : null;

                var fullFileName = IO.Path.GetRandomFileName();
                if (extension != null)
                {
                    fullFileName += $".{extension}";
                }
               
                var tmpFile = new Models.File {
                    Name = fullFileName,
                    Extension = extension,
                };
                _context.Files.Add(tmpFile);
                await _context.SaveChangesAsync();

                using var fileStream = IO.File.Create(_basePath + fullFileName);
                await file.CopyToAsync(fileStream);

                return Ok(ResponseFileList.FromApiFile(tmpFile));
            }
            else
            {
                return NotFound(new ExceptionInfo {
                    Message = ErrorsMessages.FileNotFoundOrUploadEmptyFile,
                    Description = "Файл не найден или загружен пустой файл"
                });
            }
        }

        // DELETE: api/files/5
        [SwaggerOperation(
            Summary = "Удаляет файл по Id",
            Tags = new string[] { "Файлы" }
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Успешно")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ErrorsMessages.FileNotFound,
                    Description = "Файл не найден"
                });
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            IO.File.Delete(file.Name);

            return NoContent();
        }
    }
}
