using System;
using System.Collections.Generic;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class Speciality
    {
        public Speciality()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
