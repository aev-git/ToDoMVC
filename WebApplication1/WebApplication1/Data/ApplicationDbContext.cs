using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Models;

namespace ToDoListWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoList> ToDoList { get; set; }

        public DbSet<ToDoItem> ToDoItem { get; set; }
    }
}
