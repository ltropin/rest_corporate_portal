using System;
using System.ComponentModel.DataAnnotations;

namespace restcorporate_portal.RequestModels
{
    public class CreateTask
    {
        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string ExpiredDate { get; set; }

        public int PriorityId { get; set; }

        [Required]
        public int DifficultyId { get; set; }

        public int FileId { get; set; }

        [Required]
        public int RewardCoins { get; set; }

        [Required]
        public int RewardXp { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; } = "";
    }
}
