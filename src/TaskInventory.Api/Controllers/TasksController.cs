using Microsoft.AspNetCore.Mvc;
using TaskInventory.Api.Models;

namespace TaskInventory.Api.Controllers;

[ApiController]
[Route("api/tasks")]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem { Id = 1, Title = "Audit warehouse stock levels", Description = "Reconcile physical count with system database", Priority = "High", IsCompleted = false },
        new TaskItem { Id = 2, Title = "Update supplier invoices", Description = "Process Q3 hardware supplier receipts", Priority = "Medium", IsCompleted = true },
        new TaskItem { Id = 3, Title = "Review low-stock alerts", Description = "Check monitors with fewer than 15 units", Priority = "High", IsCompleted = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll([FromQuery] bool? completed = null)
    {
        if (completed.HasValue)
        {
            return Ok(Tasks.Where(t => t.IsCompleted == completed.Value));
        }
        return Ok(Tasks);
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskItem> Create([FromBody] CreateTaskDto dto)
    {
        var nextId = Tasks.Count > 0 ? Tasks.Max(t => t.Id) + 1 : 1;
        var task = new TaskItem
        {
            Id = nextId,
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        Tasks.Add(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPatch("{id:int}/complete")]
    public ActionResult<TaskItem> MarkComplete(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return NotFound();
        task.IsCompleted = true;
        return Ok(task);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return NotFound();
        Tasks.Remove(task);
        return NoContent();
    }
}
