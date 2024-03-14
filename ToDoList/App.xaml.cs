using System;
using System.Configuration;
using System.Data;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            var priorities = new List<Priority>()
            {
                new(){ Id = 0, Name = "Wysoki" },
                new(){ Id = 1, Name = "Średni" },
                new(){ Id = 2, Name = "Niski" }
            };
            if (!dbContext.Priorities.Any())
            {
                dbContext.AddRange(priorities);
                dbContext.SaveChanges();
            }

            var statuses = new List<Status>()
            {
                new(){ Id = 0, Name = "Ukończono" },
                new(){ Id = 1, Name = "Rozpoczęto" },
                new(){ Id = 2, Name = "Dodano" },
            };
            if (!dbContext.Statuses.Any())
            {
                dbContext.AddRange(statuses);
                dbContext.SaveChanges();
            }
        }
    }
}