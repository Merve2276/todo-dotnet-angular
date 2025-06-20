using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo_dotnet_api.Data;
using todo_dotnet_api.Models;
using todo_dotnet_api.DTOs;
using System.Linq;

namespace todo_dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
        {
            var todos = await _context.Todos.ToListAsync();

            var todoDtos = todos.Select(todo => new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            }).ToList();

            return Ok(todoDtos);
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();

            var todoDto = new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            };

            return Ok(todoDto);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoDto>> PostTodo([FromBody] TodoDto todoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = new Todo
            {
                Title = todoDto.Title,
                IsCompleted = todoDto.IsCompleted,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            todoDto.Id = todo.Id;
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todoDto);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, [FromBody] TodoDto todoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();

            todo.Title = todoDto.Title;
            todo.IsCompleted = todoDto.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}


