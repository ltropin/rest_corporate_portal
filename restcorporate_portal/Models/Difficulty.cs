using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Difficulty")]
    public partial class Difficulty
    {
        public Difficulty()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string IconUrl { get; set; }

        [InverseProperty(nameof(Task.Difficulty))]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
