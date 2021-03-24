using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Priority")]
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [InverseProperty(nameof(Task.Priorirty))]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
