using Serilog;

namespace ToDoList
{
    public static class Constants

    {
        private static List<Priority> _priorities;
        private static List<Status> _status;

        public static void InitializePriorities()
        {
            _priorities = new List<Priority>
            {
                new Priority { Id = 0, Name = "Wysoki" },
                new Priority { Id = 1, Name = "Średni" },
                new Priority { Id = 2, Name = "Niski" }
            };
        }

        public static void InitializeStatuses()
        {
            _status = new List<Status>
            {
                new Status { Id = 0, Name = "Ukońoczono" },
                new Status { Id = 1, Name = "Rozpoczęto" },
                new Status { Id = 2, Name = "Dodano" }
            };
        }

        //private static readonly List<Priority> _priorities =
        //[
        //    new(){ Id = 0, Name = "Wysoki" },
        //    new(){ Id = 1, Name = "Średni" },
        //    new(){ Id = 2, Name = "Niski" }
        //];

        //private static readonly List<Status> _status =
        //[
        //    new(){ Id = 0, Name = "Ukońoczono" },
        //    new(){ Id = 1, Name = "Rozpoczęto" },
        //    new(){ Id = 2, Name = "Dodano" },

        //];

        public static Priority GetPriority(int id)
        {
            return _priorities.First(x => x.Id == id);
        }

        public static IEnumerable<Priority> GetPriorities()
        {
            return _priorities;
        }

        public static Status GetStatus(int id)
        {
            return _status.First(x => x.Id == id);
        }

        public static IEnumerable<Status> GetStatus()
        {
            return _status;
        }
    }
}