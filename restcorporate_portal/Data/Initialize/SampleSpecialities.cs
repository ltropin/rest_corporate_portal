using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleSpecialities
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Specialities.Any())
            {
                var departments = context.Departments.ToList();
                context.Specialities.AddRange(
                    new Speciality
                    {
                        Name = "Junior Frontend Developer",
                        Department = departments.Single(x => x.Name == "Frontend")
                    },
                    new Speciality
                    {
                        Name = "Middle Frontend Developer",
                        Department = departments.Single(x => x.Name == "Frontend")
                    },
                    new Speciality
                    {
                        Name = "Senior Frontend Developer",
                        Department = departments.Single(x => x.Name == "Frontend")
                    },
                    new Speciality
                    {
                        Name = "Junior Backend Developer",
                        Department = departments.Single(x => x.Name == "Backend")
                    },
                    new Speciality
                    {
                        Name = "Middle Backend Developer",
                        Department = departments.Single(x => x.Name == "Backend")
                    },
                    new Speciality
                    {
                        Name = "Senior Backend Developer",
                        Department = departments.Single(x => x.Name == "Backend")
                    },
                    new Speciality
                    {
                        Name = "Junior Mobile Developer",
                        Department = departments.Single(x => x.Name == "Mobile")
                    },
                    new Speciality
                    {
                        Name = "Middle Mobile Developer",
                        Department = departments.Single(x => x.Name == "Mobile")
                    },
                    new Speciality
                    {
                        Name = "Senior Mobile Developer",
                        Department = departments.Single(x => x.Name == "Mobile")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
