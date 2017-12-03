using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data;

namespace ToDoListWeb.Models.Repositories
{
	public class TodoItemRepository : ITodoItemRepository
	{
		private readonly ApplicationDbContext _db;

		public TodoItemRepository(ApplicationDbContext db)
		{
			if (db == null)
				throw new ArgumentNullException(nameof(db));

			_db = db;
		}

		public async Task<IEnumerable<ToDoItem>> GetToDoItems(int todoListId)
		{
			return await Queryable.Where<ToDoItem>(_db.ToDoItem, t => t.ToDoListId == todoListId).ToListAsync();
		}

		public async Task<ToDoItem> GetToDoItem(int id)
		{
			return await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<ToDoItem>(_db.ToDoItem, m => m.Id == id);
		}

		public async Task AddToDoItem(ToDoItem model)
		{
			_db.Add(model);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateToDoItem(ToDoItem model)
		{
			_db.Update(model);
			await _db.SaveChangesAsync();
		}

		public async Task<ToDoItem> DeleteToDoItem(int id)
		{
			var todoItem = await GetToDoItem(id);
			_db.ToDoItem.Remove(todoItem);

			await _db.SaveChangesAsync();
			return todoItem;
		}
	}
}