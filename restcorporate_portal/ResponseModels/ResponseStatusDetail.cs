using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseStatusDetail
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ResponseFileList Icon { get; set; }

        public static ResponseStatusDetail FromApiStatus(Status value, ResponseFileList icon) =>
            value == null ? null :
            new ResponseStatusDetail
            {
                Id = value.Id,
                Name = value.Name,
                Icon = icon,
            };
    }
}
