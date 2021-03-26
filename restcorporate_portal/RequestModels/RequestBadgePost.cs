using System;
using System.ComponentModel.DataAnnotations;

namespace restcorporate_portal.RequestModels
{
    public class RequestBadgePost
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int BadgeId { get; set; }
    }
}
