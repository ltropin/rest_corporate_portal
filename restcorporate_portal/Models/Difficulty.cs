using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Difficulty
    {
        public Difficulty()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
