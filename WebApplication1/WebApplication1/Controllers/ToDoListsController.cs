using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Controllers
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
	        var toDoList = await _repository.DeleteToDoList(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ToDoListExistsAsync(int id)
        {
            return (await _repository.GetToDoLists()).Any(e => e.Id == id);
        }

/*
        private bool ToDoListExists(int id)
        {
            return Task.Run(async () => await _repository.GetToDoLists()).Result.Any(e => e.Id == id);
        }
*/
    }
}
