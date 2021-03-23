using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Badge")]
    public partial class Badge
    {
        public Badge()
        {
            BadgesWorkers = new HashSet<BadgesWorker>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IconUrl { get; set; }

        [InverseProperty(nameof(BadgesWorker.Badge))]
        public virtual ICollection<BadgesWorker> BadgesWorkers { get; set; }
    }
}
