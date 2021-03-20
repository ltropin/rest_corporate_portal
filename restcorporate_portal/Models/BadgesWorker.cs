using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class BadgesWorker
    {
        public int BadgeId { get; set; }
        public int WorkerId { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
