using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Repositories
{
    interface ITodoListRepository
    {
	    Task<IEnumerable<ToDoList>> GetToDoLists();
	    Task<ToDoList> GetToDoList(int id);
	    Task AddToDoList(ToDoList model);
	    Task UpdateToDoList(ToDoList model);
	    Task<ToDoList> DeleteToDoList(int id);
    }

	public class TodoListRepository : ITodoListRepository
	{
		private readonly ApplicationDbContext _db;

		public TodoListRepository(ApplicationDbContext db)
		{
			if (db == null)
				throw new ArgumentNullException(nameof(db));

			_db = db;
		}

		public async Task<IEnumerable<ToDoList>> GetToDoLists()
		{
            return await _db.ToDoList.ToListAsync();
		}

		public async Task<ToDoList> GetToDoList(int id)
		{
			return await _db.ToDoList.Include(t => t.ToDoItems).SingleOrDefaultAsync(m => m.Id == id);
		}

		public async Task AddToDoList(ToDoList model)
		{
			_db.Add(model);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateToDoList(ToDoList model)
		{
			_db.Update(model);
			await _db.SaveChangesAsync();
		}

		public async Task<ToDoList> DeleteToDoList(int id)
		{
			var todoList = await GetToDoList(id);
            _db.ToDoList.Remove(todoList);
            await _db.SaveChangesAsync();
			return todoList;
		}
	}
}
