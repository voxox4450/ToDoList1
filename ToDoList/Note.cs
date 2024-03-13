using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Note
    {
        public int Id;
        public string ContentText { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }

        public void GetPriority()
        {
            Priority = Constants.GetPriority(PriorityId);
        }

        public void GetStatus()
        {
            Status = Constants.GetStatus(StatusId);
        }
    }
}