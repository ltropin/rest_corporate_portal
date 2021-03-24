using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseBadgeList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IconUrl { get; set; }

        public static ResponseBadgeList FromApiBadge(Badge value) =>
            value == null ? null :
            new ResponseBadgeList
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                IconUrl = value.IconUrl,
            };
    }
}
