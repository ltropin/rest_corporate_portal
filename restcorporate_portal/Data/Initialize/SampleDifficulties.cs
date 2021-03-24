using System;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleDifficulties
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Difficulties.Any())
            {
                context.Difficulties.AddRange(
                    new Difficulty
                    {
                        Name = "easy",
                        Description = "Простая"
                    },
                    new Difficulty
                    {
                        Name = "normal",
                        Description = "Нормальная"
                    },
                    new Difficulty
                    {
                        Name = "hard",
                        Description = "Сложная"
                    },
                    new Difficulty
                    {
                        Name = "nightmare",
                        Description = "Вспотеешь"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
