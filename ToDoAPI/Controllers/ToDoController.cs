using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using ToDoAPI.Models;
namespace ToDoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    // Dependency injection of ToDoContext
    private readonly ToDoContext _context;
    public ToDoController(ToDoContext context)
    {
        _context = context;
    }

    // HTTP ROUTES
    [HttpGet]
    public IActionResult GetToDoList()
    {
        return Ok(_context.ToDoItems.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetToDoItem(int id)
    {
        var toDoItem = _context.ToDoItems.Find(id);
        if (toDoItem == null)
        {
            return NotFound(new { Message = $"ToDo item with ID {id} not found." });
        }
        return Ok(toDoItem);
    }

    [HttpPost]
    public IActionResult CreateToDoItem([FromBody] ToDoItem toDoItem)
    {
        _context.ToDoItems.Add(toDoItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateToDoItem(int id, [FromBody] ToDoItem toDoItem)
    {
        var existingItem = _context.ToDoItems.Find(id);
        if (existingItem == null)
        {
            return NotFound(new { Message = $"ToDo item with ID {id} not found." });

        }
        existingItem.Title = toDoItem.Title;
        existingItem.DueDate = toDoItem.DueDate;
        existingItem.IsCompleted = toDoItem.IsCompleted;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteToDoItem(int id)
    {
        var existingItem = _context.ToDoItems.Find(id);
        if (existingItem == null)
        {
            return NotFound(new { Message = $"ToDo item with ID {id} not found." });
        }
        _context.ToDoItems.Remove(existingItem);
        _context.SaveChanges();
        return NoContent();
    }

}

