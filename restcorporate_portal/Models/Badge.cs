using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Badge
    {
        public Badge()
        {
            BadgesWorkers = new HashSet<BadgesWorker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Icon { get; set; }

        public virtual ICollection<BadgesWorker> BadgesWorkers { get; set; }
    }
}
