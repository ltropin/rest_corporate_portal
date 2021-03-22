using System;
using System.Linq;
using restcorporate_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace restcorporate_portal.Data.Initialize
{
    public static class SampleProducts
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Кофе на выбор в кофейне Встреча",
                        Descriptiom = "Одна чашка объемом 350мл",
                        Price = 35,
                        
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
