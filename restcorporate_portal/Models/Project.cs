using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Project
    {
        public Project()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
