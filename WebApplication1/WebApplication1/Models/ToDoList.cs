using System.Collections.Generic;

namespace ToDoListWeb.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
