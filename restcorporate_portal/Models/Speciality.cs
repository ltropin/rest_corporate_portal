using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Speciality")]
    public partial class Speciality
    {
        public Speciality()
        {
            Workers = new HashSet<Worker>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Specialities")]
        public virtual Department Department { get; set; }
        [InverseProperty(nameof(Worker.Speciality))]
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
