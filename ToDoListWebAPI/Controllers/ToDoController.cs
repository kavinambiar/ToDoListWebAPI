using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.Controllers
{
    [Route("api/ToDo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public TodoController(MyDatabaseContext context)
        {
            _context = context;

        }

        // Get list of all items
        [HttpGet]
        public ActionResult<List<ToDo>> GetAll()
        {
            return _context.Todo.ToList();
        }

        // Get item with ID parameter
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<ToDo> GetById(int id)
        {
            var item = _context.Todo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // Create new item
        [HttpPost]
        public IActionResult Create(ToDo item)
        {
            _context.Todo.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.ID }, item);
        }

        // Update item
        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDo item)
        {
            var todo = _context.Todo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            //todo.IsComplete = item.IsComplete;
            //todo.Name = item.Name;

            todo.Description = item.Description;
            todo.CreatedDate = item.CreatedDate;

            _context.Todo.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Todo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}