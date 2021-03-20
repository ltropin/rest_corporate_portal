using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class FavoriteProductsWorker
    {
        public int Id { get; set; }
        public int FavoriteProductId { get; set; }
        public int WorkerId { get; set; }

        public virtual Product FavoriteProduct { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
