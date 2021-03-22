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

namespace restcorporate_portal.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<List<Models.File>>> GetFiles()
        {
            var files = await _context.Files.Select(el => new Models.File
            {
                Id = el.Id,
                Name = el.Name,
                Extension = el.Extension,
                Url = _configuration.GetValue<string>("Url") + "/api/files/" + el.Name,
            }).ToListAsync();
            
            return Ok(files);
        }

        // GET: api/files/download/{filename}
        [SwaggerOperation(
            Summary = "Получение файла по названию и расширению",
            Tags = new string[] { "Файлы" }
        )]
        [Route("download/{filename}"), HttpGet]
        public async Task<ActionResult<byte[]>> GetFileDownloadByName(string filename)
        {
            var file = await _context.Files.SingleOrDefaultAsync(file => file.Name == filename);

            if (file == null)
            {
                return NotFound();
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
                return NotFound();
            }
        }

        // GET: api/files/download/{id}
        [SwaggerOperation(
            Summary = "Получение файла по Id",
            Tags = new string[] { "Файлы" }
        )]
        [Route("download/{id}"), HttpGet]
        public async Task<ActionResult<byte[]>> GetFileDownloadById(int id)
        {
            var file = await _context.Files.SingleOrDefaultAsync(file => file.Id == id);

            if (file == null)
            {
                return NotFound();
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
                return NotFound();
            }
        }

        // GET: api/files/info/{id}
        [SwaggerOperation(
            Summary = "Получает информацию о файле из базы данных по Id",
            Tags = new string[] { "Файлы" }
        )]
        [Route("info/{id}"), HttpGet]
        public async Task<ActionResult<byte[]>> GetFileInfoById(int id)
        {
            var file = await _context.Files.SingleOrDefaultAsync(file => file.Id == id);

            if (file == null)
            {
                return NotFound();
            }
            file.Url = _configuration.GetValue<string>("Url") + "/api/files/" + file.Name;
            return Ok();
        }

        // GET: api/files/info/{filename}
        [SwaggerOperation(
            Summary = "Получает информацию о файле из базы данных по названию файла и расширению",
            Tags = new string[] { "Файлы" }
        )]
        [Route("info/{filename}"), HttpGet]
        //[HttpGet("{id}")]
        public async Task<ActionResult<byte[]>> GetFileInfoByFileName(string filename)
        {
            var file = await _context.Files.SingleOrDefaultAsync(file => file.Name == filename);

            if (file == null)
            {
                return NotFound();
            }
            file.Url = _configuration.GetValue<string>("Url") + "/api/files/" + file.Name;

            return Ok(file);
        }

        // POST: api/files
        [SwaggerOperation(
            Summary = "Загружает картинку на сервер и создает запись в базе данных",
            Tags = new string[] { "Файлы" }
        )]
        [HttpPost]
        public async Task<ActionResult<Models.File>> PostFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (!IO.Directory.Exists(_enviroment.ContentRootPath + "/Upload/"))
                {
                    IO.Directory.CreateDirectory(_enviroment.ContentRootPath + "/Upload/");
                }

                var fullFileName = IO.Path.GetRandomFileName() + ".jpg";
                var tmpFile = new Models.File {
                    Name = fullFileName
                };
                _context.Files.Add(tmpFile);
                await _context.SaveChangesAsync();

                using var fileStream = IO.File.Create(_enviroment.ContentRootPath + "/Upload/" + fullFileName);
                await file.CopyToAsync(fileStream);

                return Ok(tmpFile);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/files/5
        [SwaggerOperation(
            Summary = "Удаляет файл по Id",
            Tags = new string[] { "Файлы" }
        )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            IO.File.Delete(file.Name);

            return NoContent();
        }
    }
}
