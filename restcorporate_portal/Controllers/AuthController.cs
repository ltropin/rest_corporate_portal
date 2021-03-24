using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = restcorporate_portal.Models;
using Swashbuckle.AspNetCore.Annotations;
using restcorporate_portal.Exceptions;
using Microsoft.AspNetCore.Authorization;
using restcorporate_portal.Utils;
using restcorporate_portal.Utils.Validators;
using restcorporate_portal.Utils.JWT;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.ResponseModels;

namespace restcorporate_portal.Controllers
{
    static class AuthErrorsMessages
    {
        public const string UserExist = "USER_EXIST";
        public const string UserNotFound = "USER_NOT_FOUND";
    }

    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly Models.corporateContext _context;

        public AuthController(Models.corporateContext context)
        {
            _context = context;
        }

        // POST api/auth/signin
        [SwaggerOperation(
            Summary = "Войти в систему",
            Tags = new string[] { "Авторизация" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Models.WorkerWithToken))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Ошибка валидации", type: typeof(List<ValidatorException>))]
        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Models.WorkerWithToken>> SignIn([FromBody] Models.AuthModel authModel)
        {

            var authModelValidator = new AuthModelValidator();
            var validationResult = authModelValidator.Validate(authModel);

            if (!validationResult.IsValid)
            {
                return UnprocessableEntity(validationResult.Errors.Select(x => new ValidatorException {
                    Field = x.PropertyName,
                    Message = x.ErrorCode,
                    Description = x.ErrorMessage
                }).ToList());
            }

            var hashPassword = HashPassword.Hash(authModel.Password);
            var existUser = _context.Workers
                .Include(x => x.Speciality)
                    .ThenInclude(x =>  x.Department)
                .SingleOrDefault(x => x.Email == authModel.Email && x.Password == hashPassword);

            if (existUser != null)
            {
                var token = JWTExtension.CreateToken(existUser);
                return Ok(new Models.WorkerWithToken {
                    Token = token,
                    Worker = ResponseWorkerList.FromApiWorker(existUser),
                });
            }
            else
            {
                return NotFound(new ExceptionInfo {
                    Message = AuthErrorsMessages.UserNotFound,
                    Description = "Сотрудник с таким Email и паролем не существует"
                });
            }
        }

        // POST api/auth/signup
        [SwaggerOperation(
            Summary = "Зарегистрироваться в системе",
            Tags = new string[] { "Авторизация" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(Models.WorkerWithToken))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Ошибка валидации", type: typeof(List<ValidatorException>))]
        [Route("signup")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Models.WorkerWithToken>> SignUp([FromBody] Models.RegisterModel registerModel)
        {
            var registerModelValidator = new RegisterModelValidator();
            var validationResult = registerModelValidator.Validate(registerModel);

            if (!validationResult.IsValid)
            {
                return UnprocessableEntity(validationResult.Errors.Select(x => new ValidatorException
                {
                    Field = x.PropertyName,
                    Message = x.ErrorCode,
                    Description = x.ErrorMessage
                }).ToList());
            }

            var hashPassword = HashPassword.Hash(registerModel.Password);
            var existUser = await _context.Workers.SingleOrDefaultAsync(x => x.Email == registerModel.Email && x.Password == hashPassword);

            if (existUser == null)
            {
                var newWorker = new Models.Worker
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email,
                    Password = hashPassword,
                    SpecialityId = registerModel.SpecialityId
                };
                var token = JWTExtension.CreateToken(newWorker);
                _context.Workers.Add(newWorker);
                await _context.SaveChangesAsync();
                var registeredWorker = await _context.Workers
                    .Include(x => x.Speciality)
                        .ThenInclude(x => x.Department)
                    .SingleOrDefaultAsync(x => x.Email == newWorker.Email && x.Password == newWorker.Password);

                return Ok(new Models.WorkerWithToken
                {
                    Token = token,
                    Worker = ResponseWorkerList.FromApiWorker(registeredWorker),
                });
            }
            else
            {
                return Conflict(new ExceptionInfo
                {
                    Message = AuthErrorsMessages.UserExist,
                    Description = "Сотрудник с таким Email и паролем уже существует"
                });
            }
        }
    }
}
