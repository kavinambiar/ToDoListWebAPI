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

        [HttpGet]
        public ActionResult<List<ToDo>> GetAll()
        {
            return _context.Todo.ToList();
        }

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
    }
}