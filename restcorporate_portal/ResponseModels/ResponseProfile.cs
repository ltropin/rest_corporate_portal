using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseProfile
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Experience { get; set; }
        public ResponseFileList Avatar { get; set; }
        public int Balance { get; set; }
        public virtual ResponseSpecialityList Speciality { get; set; }
        public virtual List<ResponseProductList> PreviousProducts { get; set; }
        public virtual List<ResponseProductList> FavoriteProducts { get; set; }

        public static ResponseProfile FromApiProfile(Worker value, corporateContext context, File avatar = null)
        {
            if (value == null)
            {
                return null;
            }

            return new ResponseProfile
            {
                Id = value.Id,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                Password = null,
                Experience = value.Experience,
                Balance = value.Balance,
                Avatar = ResponseFileList.FromApiFile(avatar),
                Speciality = ResponseSpecialityList.FromApiSpeciality(value.Speciality),
                PreviousProducts = value.PreviousProductsWorkers
                    .Select(x => {
                        var image = context.Files.SingleOrDefault(y => x.PreviousProduct.ImageUrl.Contains(y.Name));
                        var isFavorite = x.PreviousProduct.FavoriteProductsWorkers.Any(y => y.WorkerId == value.Id && y.FavoriteProductId == x.Id);
                        return ResponseProductList.FromApiProduct(x.PreviousProduct,
                            image: image,
                            isFavorite: isFavorite,
                            isCanBuy: value.Balance >= x.PreviousProduct.Price);
                    }).ToList(),
                FavoriteProducts = value.FavoriteProductsWorkers
                    .Select(x => {
                        var image = context.Files.SingleOrDefault(y => x.FavoriteProduct.ImageUrl.Contains(y.Name));
                        return ResponseProductList.FromApiProduct(x.FavoriteProduct,
                            image: image,
                            isFavorite: true,
                            isCanBuy: value.Balance >= x.FavoriteProduct.Price);
                    }).ToList()
            };
        }
    }
}
