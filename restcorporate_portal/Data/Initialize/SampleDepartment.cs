using System;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleDepartment
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department
                    {
                        Name = "Frontend",
                    },
                    new Department
                    {
                        Name = "Backend",
                    },
                    new Department
                    {
                        Name = "Mobile"
                    },
                    new Department
                    {
                        Name = "QA"
                    },
                    new Department
                    {
                        Name = "Managment"
                    },
                    new Department
                    {
                        Name = "Other"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
