using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
