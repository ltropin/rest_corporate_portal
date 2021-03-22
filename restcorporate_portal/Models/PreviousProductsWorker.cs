using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class PreviousProductsWorker
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int PreviousProductId { get; set; }
        public int WorkerId { get; set; }

        [ForeignKey(nameof(PreviousProductId))]
        [InverseProperty(nameof(Product.PreviousProductsWorkers))]
        public virtual Product PreviousProduct { get; set; }
        [ForeignKey(nameof(WorkerId))]
        [InverseProperty("PreviousProductsWorkers")]
        public virtual Worker Worker { get; set; }
    }
}
