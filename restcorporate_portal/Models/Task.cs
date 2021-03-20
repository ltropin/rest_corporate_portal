using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Task
    {
        public Task()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Reward { get; set; }
        public byte[] AttachedFile { get; set; }
        public int DifficultyId { get; set; }
        public int PriorirtyId { get; set; }
        public int AuthorId { get; set; }
        public int WorkerId { get; set; }

        public virtual Difficulty Difficulty { get; set; }
        public virtual Priority Priorirty { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
