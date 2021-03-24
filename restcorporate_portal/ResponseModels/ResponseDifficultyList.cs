using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseDifficultyList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public static ResponseDifficultyList FromApiDifficulty(Difficulty value) =>
            new ResponseDifficultyList
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
            };
    }
}
