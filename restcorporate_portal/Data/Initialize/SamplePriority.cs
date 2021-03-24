using System;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SamplePriority
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(
                    new Priority
                    {
                        Name = "low",
                        Description = "Low"
                    },
                    new Priority
                    {
                        Name = "medium",
                        Description = "Medium"
                    },
                    new Priority
                    {
                        Name = "high",
                        Description = "High"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
