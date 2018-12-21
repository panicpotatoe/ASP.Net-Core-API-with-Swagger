using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly MyContext _context;

        public MyController(MyContext context)
        {
            _context = context;

            if (_context.MyItems.Count() == 0)
            {
                // Create a new MyItem if collection is empty,
                // which means you can't delete all MyItems.
                _context.MyItems.Add(new MyItem { Name = "My Item 1" });
                _context.MyItems.Add(new MyItem { Name = "My Item 2" });
                _context.MyItems.Add(new MyItem { Name = "My Item 3" });
                _context.MyItems.Add(new MyItem { Name = "My Item 4" });
                _context.MyItems.Add(new MyItem { Name = "My Item 5" });
                _context.SaveChanges();
            }
        }

        // GET: api/My
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyItem>>> GetMyItems()
        {
            return await _context.MyItems.ToListAsync();
        }

        // GET: api/My/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyItem>> GetMyItem(long id)
        {
            var todoItem = await _context.MyItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }

    /*
    [Route("api/[controller]")]
    public class MyController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    */
}
