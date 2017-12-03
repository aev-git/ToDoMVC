using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListWeb.Models.Repositories
{
    interface ITodoListRepository
    {
	    Task<IEnumerable<ToDoList>> GetToDoLists();
	    Task<ToDoList> GetToDoList(int id);
	    Task AddToDoList(ToDoList model);
	    Task UpdateToDoList(ToDoList model);
	    Task<ToDoList> DeleteToDoList(int id);
    }
}
