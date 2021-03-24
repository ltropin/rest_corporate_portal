using System;
using System.Linq;
using restcorporate_portal.Models;
using restcorporate_portal.Utils;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleFiles
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Files.Any())
            {
                context.Files.AddRange(
                    new File
                    {
                        Name = "Draft.png",
                        Extension = "png",
                    },
                    new File
                    {
                        Name = "Complete.png",
                        Extension = "png",
                    },
                    new File
                    {
                        Name = "InProgress.png",
                        Extension = "png",
                    },
                    new File
                    {
                        Name = "Expired.png",
                        Extension = "png",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
