using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListWeb.Models.Repositories
{
	interface ITodoItemRepository
	{
		Task<IEnumerable<ToDoItem>> GetToDoItems(int todoListId);
		Task<ToDoItem> GetToDoItem(int id);
		Task AddToDoItem(ToDoItem model);
		Task UpdateToDoItem(ToDoItem model);
		Task<ToDoItem> DeleteToDoItem(int id);
	}
}