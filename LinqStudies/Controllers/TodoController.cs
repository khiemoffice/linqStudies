using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LinqStudies.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            _context.Database.EnsureCreatedAsync();
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.SaveChanges();
            }
            if (_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee { Name = "Item1" });
                _context.Employees.Add(new Employee { Name = "Item2" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<TodoItem> getAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("back")]
        public IActionResult getE()
        {
            var cc = new string[] { "haha","hoho"};
            return Content("helo bakon");
        }


        [HttpGet("haha")]
        public IActionResult getII()
        {
            var item = _context.TodoItems.Where(x => x.Id == 2);
            return new ObjectResult(item);
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult getItem(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                return NotFound();
            };
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult PostItem([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }


    }
}