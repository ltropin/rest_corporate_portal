using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseWorkerList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Experience { get; set; }
        public string AvatarUrl { get; set; }
        public int Balance { get; set; }

        public static ResponseWorkerList FromApiWorker(Worker value) =>
            new ResponseWorkerList
            {
                Id = value.Id,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                Password = null,
                Experience = value.Experience,
                Balance = value.Balance,
                AvatarUrl = value.AvatarUrl,
            };
    }
}
