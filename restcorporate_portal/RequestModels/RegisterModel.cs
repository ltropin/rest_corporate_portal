using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Models
{
    public class RegisterModel
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Email сотрудника
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль сотрудника
        /// </summary>
        [Required]
        public string Password { get; set; }

        public int SpecialityId { get; set; }

        ///// <summary>
        ///// Должность сотрудника
        ///// </summary>
        //[ForeignKey(nameof(SpecialityId))]
        //[InverseProperty("Workers")]
        //public virtual Speciality Speciality { get; set; }
    }
}