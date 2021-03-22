using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Specialities = new HashSet<Speciality>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [InverseProperty(nameof(Speciality.Department))]
        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}
