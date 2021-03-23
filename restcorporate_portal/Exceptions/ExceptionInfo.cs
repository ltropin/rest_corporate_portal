using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Exceptions
{
    public class ExceptionInfo
    {
        /// <summary>
        /// Уникальное название ошибки (ключ)
        /// </summary>
        /// <example>
        /// AUTHORIZATION_ERROR
        /// </example>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Понятное описание ошибки
        /// </summary>
        /// <example>
        /// Ошибка авторизации
        /// </example>
        [Required]
        public string Description { get; set; }
    }
}
