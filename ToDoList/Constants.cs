using Serilog;

namespace ToDoList
{
    public class Constants
    {
        public static List<Priority> GetPriorities()
        {
            using (var context = new Database())
            {
                return context.Priorities.ToList();
            }
        }

        public static Priority GetPriority(int id)
        {
            using (var context = new Database())
            {
                return context.Priorities.First(x => x.Id == id);
            }
        }

        public static List<Status> GetStatuses()
        {
            using (var context = new Database())
            {
                return context.Statuses.ToList();
            }
        }

        public static Status GetStatus(int id)
        {
            using (var context = new Database())
            {
                return context.Statuses.First(x => x.Id == id);
            }
        }
    }
}