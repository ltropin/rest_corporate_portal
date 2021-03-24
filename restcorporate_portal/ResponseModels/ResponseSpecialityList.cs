using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseSpecialityList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ResponseDepartmentList Department { get; set; }

        public static ResponseSpecialityList FromApiSpeciality(Speciality value) =>
            value == null ? null :
            new ResponseSpecialityList
            {
                Id = value.Id,
                Name = value.Name,
                Department = ResponseDepartmentList.FromApiDepartment(value.Department),

            };
    }
}
