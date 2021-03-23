using System;
using System.ComponentModel.DataAnnotations;

namespace restcorporate_portal.Exceptions
{
    public class ValidatorException
    {
        /// <summary>
        /// Поле
        /// </summary>
        /// <example>
        /// password
        /// </example>
        [Required]
        public string Field { get; set; }
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
