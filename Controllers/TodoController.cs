using Microsoft.AspNetCore.Mvc;
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
    public IActionResult AddTask([FromForm] CreateTaskRequest task)
    {
        var newTask = new TaskTemplate { Title = task.Title, IsDone = false };
        _context.Add(newTask);
        _context.SaveChanges();

        return Created($"/Todo/{newTask.Title}", newTask);
    }

    /// <summary>
    /// To toggle the status with id
    /// </summary>
    [HttpPatch("{id:int}")]
    public IActionResult UpdateTask(int id)
    {
        var task = _context.Todo.Find(id);

        if (task != null)
        {
            task.IsDone = !task.IsDone;
            _context.SaveChanges();
        }
        
        return NoContent();
    }
    
    [HttpGet("all-tasks")]
    public IActionResult GetAllTasks()
    {
        return Ok(_context.Todo);
    }
}