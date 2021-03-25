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
                    },
                    new File
                    {
                        Name = "b_launch.jpg",
                        Extension = "jpg",
                    },
                    new File
                    {
                        Name = "coffe.jpg",
                        Extension = "jpg",
                    },
                    new File
                    {
                        Name = "spa.jpg",
                        Extension = "jpg",
                    },
                    new File
                    {
                        Name = "leave_early.jpg",
                        Extension = "jpg",
                    },
                    new File
                    {
                        Name = "tshirt_logo.jpg",
                        Extension = "jpg",
                    },
                    new File
                    {
                        Name = "weekend .jpg",
                        Extension = "jpg",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
