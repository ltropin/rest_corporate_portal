using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Worker
    {
        public Worker()
        {
            BadgesWorkers = new HashSet<BadgesWorker>();
            FavoriteProductsWorkers = new HashSet<FavoriteProductsWorker>();
            PreviousProductsWorkers = new HashSet<PreviousProductsWorker>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Experience { get; set; }
        public byte[] Avatar { get; set; }
        public int Balance { get; set; }
        public int SpecialityId { get; set; }
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Speciality Speciality { get; set; }
        public virtual ICollection<BadgesWorker> BadgesWorkers { get; set; }
        public virtual ICollection<FavoriteProductsWorker> FavoriteProductsWorkers { get; set; }
        public virtual ICollection<PreviousProductsWorker> PreviousProductsWorkers { get; set; }
    }
}
