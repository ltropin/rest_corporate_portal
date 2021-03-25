using System;
using System.Linq;
using restcorporate_portal.Models;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Utils;

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
                        Name = "Кофе на выбор в кофейне Встреча",
                        Descriptiom = "Одна чашка объемом 350мл",
                        Price = 35,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "coffe.jpg",
                    },
                    new Product
                    {
                        Name = "Бизнес ланч в Шашлыкофф",
                        Descriptiom = "Комплексный обед на сумму 269р с доставкой в офис",
                        Price = 55,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "b_lanch.jpg",
                    },
                    new Product
                    {
                        Name = "Выходной",
                        Descriptiom = "Целый день кайфуй - завтра вернуться не забудь",
                        Price = 250,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "weekend.jpg",
                    },
                    new Product
                    {
                        Name = "Уйти пораньше на час",
                        Descriptiom = "На пару часиков пораньше в пятницу - благое дело",
                        Price = 20,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "leave_early.jpg",
                    },
                    new Product
                    {
                        Name = "SPA процедуры",
                        Descriptiom = "Релакс, балдеж и пилинг - позволь себе стать красивее",
                        Price = 600,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "spa.jpg",
                    },
                    new Product
                    {
                        Name = "Футболка с лого фирмы",
                        Descriptiom = "Покажи всем в какой крутой конторе ты работаешь",
                        Price = 200,
                        ImageUrl = Constans.ApiUrl + Constans.FileDownloadPart + "tshirt_logo.jpg",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
