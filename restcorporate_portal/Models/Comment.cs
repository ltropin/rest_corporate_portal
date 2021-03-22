using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Column(TypeName = "date")]
        public DateTime Time { get; set; }
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("Comments")]
        public virtual Task Task { get; set; }
    }
}
