using System;
using System.Linq;
using restcorporate_portal.Models;

namespace restcorporate_portal.Data.Initialize
{
    public class SampleTasks
    {
        public static void Initialize(corporateContext context)
        {
            if (!context.Tasks.Any())
            {
                var statuses = context.Statuses.ToList();
                var difficulties = context.Difficulties.ToList();
                var priorities = context.Priorities.ToList();
                var workers = context.Workers.ToList();
                context.Tasks.AddRange(
                    new Task
                    {
                        Title = "Экран \"Профиль пользователя\"",
                        Description = "<h2>Назначение</h2><p>Просмотр информации об текущем пользователе</p><p><br></p><h2>Состав экрана</h2><ul><li>Аватар</li><li>Имя</li><li>Логин</li><li>Email</li><li>Баланс пользователя</li></ul><p><br></p><h2>Логика функционирования</h2><p><br></p><p>При успешной авторизации происходит переход на этот экран с прогрузкой данных о пользователе. Во время прогрузки отображается лоадер.</p><p>При нажатии на кнопку с гаечкой пользователь переходит на экран редактирования профиля.</p><p>При нажатии по значку баланса пользователь переходит на экран покупки игровой валюты.</p>",
                        ExpirationDate = DateTime.ParseExact("04.04.2021", "dd.MM.yyyy", null),
                        RewardCoins = 15,
                        RewardXp = 45,
                        DifficultyId = difficulties.Single(x => x.Name == "normal").Id,
                        StatusId = statuses.Single(x => x.Name == "to_do").Id,
                        PriorirtyId = priorities.Single(x => x.Name == "high").Id,
                        AuthorId = workers.Single(x => x.Email == "kirll.manov@list.ru").Id,
                        WorkerId = workers.Single(x => x.Email == "ltropin@mail.ru").Id,
                    },
                    new Task
                    {
                        Title = "Экран \"авторизации и регистрации\"",
                        Description = "<h2>Назначение</h2><p>Просмотр информации об текущем пользователе</p><p><br></p><h2>Состав экрана</h2><ul><li>Аватар</li><li>Имя</li><li>Логин</li><li>Email</li><li>Баланс пользователя</li></ul><p><br></p><h2>Логика функционирования</h2><p><br></p><p>При успешной авторизации происходит переход на этот экран с прогрузкой данных о пользователе. Во время прогрузки отображается лоадер.</p><p>При нажатии на кнопку с гаечкой пользователь переходит на экран редактирования профиля.</p><p>При нажатии по значку баланса пользователь переходит на экран покупки игровой валюты.</p>",
                        ExpirationDate = DateTime.ParseExact("17.03.2021", "dd.MM.yyyy", null),
                        RewardCoins = 30,
                        RewardXp = 100,
                        DifficultyId = difficulties.Single(x => x.Name == "hard").Id,
                        StatusId = statuses.Single(x => x.Name == "in_progress").Id,
                        PriorirtyId = priorities.Single(x => x.Name == "high").Id,
                        AuthorId = workers.Single(x => x.Email == "kirll.manov@list.ru").Id,
                        WorkerId = workers.Single(x => x.Email == "ltropin@mail.ru").Id,
                    },
                    new Task
                    {
                        Title = "Инициализация проекта + подключение сторонних библиотек",
                        Description = "<h2>Назначение</h2><p>Просмотр информации об текущем пользователе</p><p><br></p><h2>Состав экрана</h2><ul><li>Аватар</li><li>Имя</li><li>Логин</li><li>Email</li><li>Баланс пользователя</li></ul><p><br></p><h2>Логика функционирования</h2><p><br></p><p>При успешной авторизации происходит переход на этот экран с прогрузкой данных о пользователе. Во время прогрузки отображается лоадер.</p><p>При нажатии на кнопку с гаечкой пользователь переходит на экран редактирования профиля.</p><p>При нажатии по значку баланса пользователь переходит на экран покупки игровой валюты.</p>",
                        ExpirationDate = DateTime.ParseExact("17.05.2021", "dd.MM.yyyy", null),
                        RewardCoins = 30,
                        RewardXp = 100,
                        DifficultyId = difficulties.Single(x => x.Name == "hard").Id,
                        StatusId = statuses.Single(x => x.Name == "bugs").Id,
                        PriorirtyId = priorities.Single(x => x.Name == "low").Id,
                        WorkerId = workers.Single(x => x.Email == "kirll.manov@list.ru").Id,
                        AuthorId = workers.Single(x => x.Email == "ltropin@mail.ru").Id,
                    },
                    new Task
                    {
                        Title = "Экран \"Редактирование профиля + загрузка фото с камеры и галереи\"",
                        Description = "<h2>Назначение</h2><p>Просмотр информации об текущем пользователе</p><p><br></p><h2>Состав экрана</h2><ul><li>Аватар</li><li>Имя</li><li>Логин</li><li>Email</li><li>Баланс пользователя</li></ul><p><br></p><h2>Логика функционирования</h2><p><br></p><p>При успешной авторизации происходит переход на этот экран с прогрузкой данных о пользователе. Во время прогрузки отображается лоадер.</p><p>При нажатии на кнопку с гаечкой пользователь переходит на экран редактирования профиля.</p><p>При нажатии по значку баланса пользователь переходит на экран покупки игровой валюты.</p>",
                        ExpirationDate = DateTime.ParseExact("03.03.2021", "dd.MM.yyyy", null),
                        RewardCoins = 60,
                        RewardXp = 130,
                        DifficultyId = difficulties.Single(x => x.Name == "nightmare").Id,
                        StatusId = statuses.Single(x => x.Name == "complete").Id,
                        PriorirtyId = priorities.Single(x => x.Name == "medium").Id,
                        WorkerId = workers.Single(x => x.Email == "kirll.manov@list.ru").Id,
                        AuthorId = workers.Single(x => x.Email == "ltropin@mail.ru").Id,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
