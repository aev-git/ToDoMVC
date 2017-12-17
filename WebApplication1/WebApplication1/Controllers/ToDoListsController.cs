using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data;
using ToDoListWeb.Models;
using ToDoListWeb.Models.Repositories;

namespace ToDoListWeb.Controllers
{
    public class ToDoListsController : Controller
    {
	    private readonly ITodoListRepository _repository;

        public ToDoListsController(ApplicationDbContext context)
        {
	        _repository = new TodoListRepository(context);
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
			return View(await _repository.GetToDoLists());
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

	        var toDoList = await _repository.GetToDoList(id.Value);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
	            await _repository.AddToDoList(toDoList);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _repository.GetToDoList(id.Value);
            if (toDoList == null)
            {
                return NotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
	                await _repository.UpdateToDoList(toDoList);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ToDoListExistsAsync(toDoList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

	        var toDoList = await _repository.GetToDoList(id.Value);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
	        var todoList = await _repository.DeleteToDoList(id);
	        return Ok(todoList);
        }

        private async Task<bool> ToDoListExistsAsync(int id)
        {
            return (await _repository.GetToDoLists()).Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ToDoList list)
        {
            if (list.Id > 0)
            {
                await _repository.UpdateToDoList(list);
            }
            else
            {
                await _repository.AddToDoList(list);
            }
            return Ok(list.Id);
        }
    }
}
