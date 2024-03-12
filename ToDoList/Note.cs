using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Note(string contentText, DateTime endDate, DateTime startDate, string priority, string status)
    {
        public string ContentText { get; set; } = contentText;
        public DateTime EndDate { get; set; } = endDate;
        public DateTime StartDate { get; set; } = startDate;
        public string Priority { get; set; } = priority;
        public string Status { get; set; } = status;

        public string Show()
        {
            return contentText + " | " + startDate.ToString("dd.MM.yyyy") + " | " + endDate.ToString("dd.MM.yyyy") + " | " + priority + " | " + status;
        }
    }
}