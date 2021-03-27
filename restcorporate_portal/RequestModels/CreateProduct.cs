using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restcorporate_portal.RequestModels
{
    public class CreateProduct
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int FileId { get; set; }
        public int Price { get; set; }
    }
}
