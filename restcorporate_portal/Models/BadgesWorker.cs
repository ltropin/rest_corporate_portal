using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class BadgesWorker
    {
        [Key]
        public int BadgeId { get; set; }
        [Key]
        public int WorkerId { get; set; }

        [ForeignKey(nameof(BadgeId))]
        [InverseProperty("BadgesWorkers")]
        public virtual Badge Badge { get; set; }
        [ForeignKey(nameof(WorkerId))]
        [InverseProperty("BadgesWorkers")]
        public virtual Worker Worker { get; set; }
    }
}
