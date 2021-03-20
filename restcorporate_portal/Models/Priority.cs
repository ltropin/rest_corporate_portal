using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
