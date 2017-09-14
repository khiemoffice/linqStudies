using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LinqStudies.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.SaveChanges();
            }
            
        }

        [HttpGet]
        public List<TodoItem> getAll()
        {
            return _context.TodoItems.ToList();
        }
        [HttpGet("prod")]
        public IActionResult product()
        {
            var ord2 = _context.Orders.Include("Products").
                Where(x => x.OrderId == 2)
                .SingleOrDefault();
            return new ObjectResult(ord2);
        }

        [HttpGet("time")]
        [ResponseCache(Duration =600)]
        public IActionResult showTime()
        {

            return Content(DateTime.Now.ToShortTimeString());
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