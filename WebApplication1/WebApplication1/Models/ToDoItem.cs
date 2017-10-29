namespace WebApplication1.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int ToDoListId { get; set; }
        public string Memo { get; set; }
        public bool IsDone { get; set; }
        public ToDoList ToDoList { get; set; }
    }
}