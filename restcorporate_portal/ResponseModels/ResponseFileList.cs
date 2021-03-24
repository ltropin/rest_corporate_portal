using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;
using restcorporate_portal.Utils;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseFileList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Url { get => Constans.ApiUrl + Constans.FileDownloadPart + Name; }

        public static ResponseFileList FromApiFile(File value) =>
            value == null ? null :
            new ResponseFileList
            {
                Id = value.Id,
                Name = value.Name,
                Extension = value.Extension,
            };
    }
}
