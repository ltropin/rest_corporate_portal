using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Worker")]
    public partial class Worker
    {
        public Worker()
        {
            BadgesWorkers = new HashSet<BadgesWorker>();
            FavoriteProductsWorkers = new HashSet<FavoriteProductsWorker>();
            PreviousProductsWorkers = new HashSet<PreviousProductsWorker>();
            Tasks = new HashSet<Task>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Experience { get; set; }
        public string AvatarUrl { get; set; }
        public int Balance { get; set; }
        public int SpecialityId { get; set; }
        public int? ProjectId { get; set; }

        //[ForeignKey(nameof(ProjectId))]
        //[InverseProperty("Workers")]
        public virtual Project Project { get; set; }
        //[ForeignKey(nameof(SpecialityId))]
        //[InverseProperty("Workers")]
        public virtual Speciality Speciality { get; set; }
        //[InverseProperty(nameof(BadgesWorker.Worker))]
        public virtual ICollection<BadgesWorker> BadgesWorkers { get; set; }
        //[InverseProperty(nameof(FavoriteProductsWorker.Worker))]
        public virtual ICollection<FavoriteProductsWorker> FavoriteProductsWorkers { get; set; }
        //[InverseProperty(nameof(PreviousProductsWorker.Worker))]
        public virtual ICollection<PreviousProductsWorker> PreviousProductsWorkers { get; set; }
        //[InverseProperty(nameof(Task.Worker))]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
