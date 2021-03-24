using System;
using System.Linq;
using restcorporate_portal.Models;
using restcorporate_portal.Utils;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleWorkers
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Workers.Any())
            {
                var specialities = context.Specialities.ToList();
                context.Workers.AddRange(
                    new Worker
                    {
                        FirstName = "Kirll",
                        LastName = "Manov",
                        Email = "kirll.manov@list.ru",
                        Password = "8a82eadae2f46c2cf68daaa730d018d93ababe1f",
                        SpecialityId = specialities.Single(x => x.Name == "Senior Frontend Developer").Id,
                    },
                    new Worker
                    {
                        FirstName = "Алексей",
                        LastName = "Тропин",
                        Email = "ltropin@mail.ru",
                        Password = "5e6268a55f798cbb4626be245b8cc13a4d856fc9",
                        SpecialityId = specialities.Single(x => x.Name == "Senior Backend Developer").Id,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
