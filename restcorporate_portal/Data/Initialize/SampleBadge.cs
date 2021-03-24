using System;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleBadge
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Badges.Any())
            {
                context.Badges.AddRange(
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    },
                    new Badge
                    {
                        Name = "",
                        Description = "",
                        IconUrl = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
