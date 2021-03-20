using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Product
    {
        public Product()
        {
            FavoriteProductsWorkers = new HashSet<FavoriteProductsWorker>();
            PreviousProductsWorkers = new HashSet<PreviousProductsWorker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptiom { get; set; }
        public byte[] Image { get; set; }
        public int Price { get; set; }

        public virtual ICollection<FavoriteProductsWorker> FavoriteProductsWorkers { get; set; }
        public virtual ICollection<PreviousProductsWorker> PreviousProductsWorkers { get; set; }
    }
}
