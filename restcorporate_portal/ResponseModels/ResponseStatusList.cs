using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseStatusList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IconUrl { get; set; }

        public static ResponseStatusList FromApiStatus(Status value) =>
            new ResponseStatusList
            {
                Id = value.Id,
                Name = value.Name,
                IconUrl = value.IconUrl,
            };
    }
}
