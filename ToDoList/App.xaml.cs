using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using var dbContext = new Database();

            var statuses = new List<Status>()
            {
                new(){ Id = 0, Name = "Wysoki" },
                new(){ Id = 1, Name = "Średni" },
                new(){ Id = 2, Name = "Niski" }
            };
            if (!dbContext.Priorities.Any())
            {
                dbContext.AddRange(statuses);
                dbContext.SaveChanges();
            }

            var priorities = new List<Priority>()
            {
                new(){ Id = 0, Name = "Ukońoczono" },
                new(){ Id = 1, Name = "Rozpoczęto" },
                new(){ Id = 2, Name = "Dodano" },
            };
            if (!dbContext.Priorities.Any())
            {
                dbContext.AddRange(priorities);
                dbContext.SaveChanges();
            }
        }
    }
}