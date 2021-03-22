using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            FavoriteProductsWorkers = new HashSet<FavoriteProductsWorker>();
            PreviousProductsWorkers = new HashSet<PreviousProductsWorker>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriptiom { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int Price { get; set; }

        [InverseProperty(nameof(FavoriteProductsWorker.FavoriteProduct))]
        public virtual ICollection<FavoriteProductsWorker> FavoriteProductsWorkers { get; set; }
        [InverseProperty(nameof(PreviousProductsWorker.PreviousProduct))]
        public virtual ICollection<PreviousProductsWorker> PreviousProductsWorkers { get; set; }
    }
}
