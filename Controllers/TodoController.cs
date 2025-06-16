using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTodo.Data;

namespace MyTodo.Controllers;

public record CreateTaskRequest(string Title);

[ApiController]
[Route("Todo")]
public class TodoController : ControllerBase
{
    private PracticeContext _context;

    public TodoController(PracticeContext context)
    {
        _context = context;
    }

    [HttpPost("add-task")]
    public IActionResult AddTask([FromBody] CreateTaskRequest task)
    {
        var newTask = new TaskTemplate { Title = task.Title, IsDone = false };
        _context.Todo.Add(newTask);
        _context.SaveChanges();

        return Created($"Todo/{newTask.Title}", newTask);
    }

    [HttpPatch("toggle-task/{id:int}")]
    public IActionResult UpdateTask(int id)
    {
        var task = _context.Todo.Find(id);

        if (task == null) return NotFound();

        task.IsDone = !task.IsDone;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("delete-task/{id:int}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Todo.Find(id);
        if (task == null) return NotFound();
        
        _context.Todo.Remove(task);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpGet("all-tasks")]
    public IActionResult GetAllTasks()
    {
        Console.WriteLine("Serving all tasks.....");

        return Ok(_context.Todo.AsNoTracking().OrderBy(task => task.IsDone));
    }
}