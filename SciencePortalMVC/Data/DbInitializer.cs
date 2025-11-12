using SciencePortalMVC.Models;
using System;
using System.Linq;

namespace SciencePortalMVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SciencePortalDbContext context)
        {
            // Убеждаемся, что база данных создана миграциями.
            context.Database.EnsureCreated();

            // Если данные уже есть, ничего не делаем.
            if (context.Departments.Any())
            {
                return;
            }

            // --- 1. Создание кафедр ---
            var departments = new Department[]
            {
                new Department { Name = "Информационные технологии", Profile = "ИТ" },
                new Department { Name = "Автоматизированный электропривод", Profile = "АЭП" },
                new Department { Name = "Промышленная электроника", Profile = "ПЭ" }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

            // --- 2. Создание преподавателей ---
            var teachers = new Teacher[]
            {
                new Teacher { FullName = "Асенчик О.Д.", Position = "Доцент", Degree = "к.т.н.", DepartmentId = 1 },
                new Teacher { FullName = "Иванов И.И.", Position = "Профессор", Degree = "д.т.н.", DepartmentId = 2 },
                new Teacher { FullName = "Петров П.П.", Position = "Ассистент", Degree = "", DepartmentId = 1 },
                new Teacher { FullName = "Сидоров С.С.", Position = "Ст. преподаватель", Degree = "к.ф.-м.н", DepartmentId = 3 },
                new Teacher { FullName = "Кузнецов К.К.", Position = "Доцент", Degree = "к.т.н.", DepartmentId = 2 }
            };
            context.Teachers.AddRange(teachers);
            context.SaveChanges();

            // --- 3. Создание проектов ---
            var projects = new Project[]
            {
                new Project { Name = "Разработка ИИ-ассистента", Number = "AI-01", FundingOrg = "Грант БРФФИ", StartDate = new DateTime(2023, 9, 1), LeaderId = 1 },
                new Project { Name = "Оптимизация электропривода", Number = "DRV-05", FundingOrg = "Госпрограмма", StartDate = new DateTime(2024, 2, 15), LeaderId = 2 },
                new Project { Name = "Исследование наноэлектроники", Number = "NANO-03", FundingOrg = "Международный контракт", StartDate = new DateTime(2024, 6, 20), LeaderId = 4 }
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();

            // --- 4. Создание публикаций и связей с авторами ---
            var publications = new Publication[]
            {
                new Publication { Title = "Нейронные сети в анализе данных", Type = "Статья", Year = 2023 },
                new Publication { Title = "Современные системы управления", Type = "Монография", Year = 2024 },
                new Publication { Title = "Квантовые вычисления: Перспективы", Type = "Тезисы", Year = 2025 }
            };

            // Добавляем публикации и сразу связываем их с авторами
            publications[0].Teachers.Add(teachers[0]); // Асенчик - автор первой
            publications[0].Teachers.Add(teachers[2]); // Петров - соавтор первой
            publications[1].Teachers.Add(teachers[1]); // Иванов - автор второй
            publications[2].Teachers.Add(teachers[0]); // Асенчик - автор третьей
            publications[2].Teachers.Add(teachers[3]); // Сидоров - соавтор третьей

            context.Publications.AddRange(publications);
            context.SaveChanges();
        }
    }
}