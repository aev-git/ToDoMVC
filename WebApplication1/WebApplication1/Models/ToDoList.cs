using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public int ToDoListId { get; set; }
        public string Memo { get; set; }
        public bool IsDone { get; set; }
    }
}
