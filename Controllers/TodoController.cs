using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTodo.Data;

namespace MyTodo.Controllers;

public record CreateTaskRequest(string Title);

[ApiController]
[Route("Todo")]
public class TodoController(PracticeContext context) : ControllerBase
{
    [HttpPost("add-task")]
    public IActionResult AddTask([FromBody] CreateTaskRequest task)
    {
        if(task.Title == "") return BadRequest();
        var newTask = new TaskTemplate { Title = task.Title, IsDone = false };
        context.Todo.Add(newTask);
        context.SaveChanges();

        return Created($"Todo/{newTask.Title}", newTask);
    }

    [HttpPatch("toggle-task/{id:int}")]
    public IActionResult UpdateTask(int id)
    {
        var task = context.Todo.Find(id);

        if (task == null) return NotFound();

        task.IsDone = !task.IsDone;
        context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("delete-task/{id:int}")]
    public IActionResult DeleteTask(int id)
    {
        var task = context.Todo.Find(id);
        if (task == null) return NotFound();
        
        context.Todo.Remove(task);
        context.SaveChanges();
        return NoContent();
    }

    [HttpGet("all-tasks")]
    public IActionResult GetAllTasks()
    {
        Console.WriteLine("Serving all tasks.....");

        return Ok(context.Todo.AsNoTracking().OrderBy(task => task.IsDone));
    }
}
