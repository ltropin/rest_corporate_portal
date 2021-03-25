using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseProductList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ResponseFileList Image { get; set; }
        public int Price { get; set; }

        public bool IsFavorite { get; set; }

        public static ResponseProductList FromApiProduct(Product value, File image, bool isFavorite) =>
            value == null ? null :
            new ResponseProductList
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Descriptiom,
                Price = value.Price,
                Image = ResponseFileList.FromApiFile(image),
                IsFavorite = isFavorite,
            };
    }
}
