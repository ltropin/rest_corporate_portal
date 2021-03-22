using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Project")]
    public partial class Project
    {
        public Project()
        {
            Workers = new HashSet<Worker>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [InverseProperty(nameof(Worker.Project))]
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
