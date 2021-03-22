using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace restcorporate_portal.Models
{
    [Table("Task")]
    public partial class Task
    {
        public Task()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
        public int Reward { get; set; }
        public string AttachedFileUrl { get; set; }
        public int DifficultyId { get; set; }
        public int PriorirtyId { get; set; }
        public int AuthorId { get; set; }
        public int WorkerId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty("TaskAuthors")]
        public virtual Worker Author { get; set; }
        [ForeignKey(nameof(DifficultyId))]
        [InverseProperty("Tasks")]
        public virtual Difficulty Difficulty { get; set; }
        [ForeignKey(nameof(PriorirtyId))]
        [InverseProperty(nameof(Priority.Tasks))]
        public virtual Priority Priorirty { get; set; }
        [ForeignKey(nameof(WorkerId))]
        [InverseProperty("TaskWorkers")]
        public virtual Worker Worker { get; set; }
        [InverseProperty(nameof(Comment.Task))]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
