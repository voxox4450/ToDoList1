using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Status(int id)
    {
        public int id { get; set; } = id;
        public string name { get; set; }
    }
}