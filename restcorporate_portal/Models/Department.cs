using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Department
    {
        public Department()
        {
            Specialities = new HashSet<Speciality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}
