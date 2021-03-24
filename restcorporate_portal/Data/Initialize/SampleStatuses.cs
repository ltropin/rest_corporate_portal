using System;
using System.Linq;
using restcorporate_portal.Models;
using restcorporate_portal.Utils;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleStatuses
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new Status
                    {
                        Name = "to_do",
                        Description = "Новая",
                        IconUrl = Constans.ApiUrl + Constans.FileDownloadPart + "Draft.png",

                    },
                    new Status
                    {
                        Name = "in_progress",
                        Description = "В работе",
                        IconUrl = Constans.ApiUrl + Constans.FileDownloadPart + "InProgress.png",

                    },
                    new Status
                    {
                        Name = "bugs",
                        Description = "В отладке",
                        IconUrl = Constans.ApiUrl + Constans.FileDownloadPart + "InProgress.png",

                    },
                    new Status
                    {
                        Name = "complete",
                        Description = "Новая",
                        IconUrl = Constans.ApiUrl + Constans.FileDownloadPart + "Complete.png",

                    }
                );
                context.SaveChanges();
            }
        }
    }
}
