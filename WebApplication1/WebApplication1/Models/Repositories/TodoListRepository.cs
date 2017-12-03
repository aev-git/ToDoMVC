using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data;

namespace ToDoListWeb.Models.Repositories
{
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
			return await EntityFrameworkQueryableExtensions.ToListAsync<ToDoList>(_db.ToDoList);
		}

		public async Task<ToDoList> GetToDoList(int id)
		{
			return await EntityFrameworkQueryableExtensions.Include<ToDoList, ICollection<ToDoItem>>(_db.ToDoList, t => t.ToDoItems).SingleOrDefaultAsync(m => m.Id == id);
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