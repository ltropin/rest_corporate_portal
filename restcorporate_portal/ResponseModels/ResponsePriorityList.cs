using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponsePriorityList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public static ResponsePriorityList FromApiPriority(Priority value) =>
            value == null ? null :
            new ResponsePriorityList
            {
                Id = value.Id,
                Name = value.Name,
            };
    }
}
