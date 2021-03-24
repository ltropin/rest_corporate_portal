using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseDepartmentList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public static ResponseDepartmentList FromApiDepartment(Department value) =>
            value == null ? null :
            new ResponseDepartmentList
            {
                Id = value.Id,
                Name = value.Name,
            };
    }
}
