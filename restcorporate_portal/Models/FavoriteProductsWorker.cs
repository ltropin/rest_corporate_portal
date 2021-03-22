using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class FavoriteProductsWorker
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int FavoriteProductId { get; set; }
        public int WorkerId { get; set; }

        [ForeignKey(nameof(FavoriteProductId))]
        [InverseProperty(nameof(Product.FavoriteProductsWorkers))]
        public virtual Product FavoriteProduct { get; set; }
        [ForeignKey(nameof(WorkerId))]
        [InverseProperty("FavoriteProductsWorkers")]
        public virtual Worker Worker { get; set; }
    }
}
