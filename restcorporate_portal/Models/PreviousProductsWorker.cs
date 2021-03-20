using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class PreviousProductsWorker
    {
        public int Id { get; set; }
        public int PreviousProductId { get; set; }
        public int WorkerId { get; set; }

        public virtual Product PreviousProduct { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
