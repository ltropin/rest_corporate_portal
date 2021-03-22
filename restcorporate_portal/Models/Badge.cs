using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

#nullable disable

namespace restcorporate_portal.Models
{
    [DataContract(Name = "OVERRIDECLASSNAME")]
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
