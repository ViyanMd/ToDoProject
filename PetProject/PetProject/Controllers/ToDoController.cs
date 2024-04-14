using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProject.Data;
using PetProject.Models;

namespace PetProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        ApplicationContext db;
        public ToDoController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var users = await db.ToDos.ToListAsync();

            if (users.Any()) return Ok(users);

            return Ok("List is empty!");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string todo)
        {
            if (todo == null) return BadRequest();

            var newTodo = new ToDo(todo);

            await db.ToDos.AddAsync(newTodo);

            await db.SaveChangesAsync();

            return Ok(newTodo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var todo = await db.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            db.ToDos.Remove(todo);
            await db.SaveChangesAsync();

            return Ok("ToDo item deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ToDo updatedTodo)
        {
            if (id != updatedTodo.Id)
            {
                return BadRequest();
            }

            var todo = await db.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.ToDoContent = updatedTodo.ToDoContent; 

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(todo);
        }

        private bool TodoExists(Guid id)
        {
            return db.ToDos.Any(e => e.Id == id);
        }
    }
}
