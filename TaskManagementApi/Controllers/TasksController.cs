using TaskManagementApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<List<TaskItem>> GetTasks(
        [FromQuery] Enums.TaskItemStatus? status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortDirection = null)
    {
        if (pageNumber < 1)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest("Page size must be between 1 and 100.");
        }

        var tasks = _taskService.GetTasks(status, pageNumber, pageSize, sortBy, sortDirection);

        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<TaskItem> CreateTask(CreateTaskDto taskDto)
    {
        var createdTask = _taskService.CreateTask(taskDto);

        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetTaskById(int id)
    {
        var task = _taskService.GetTaskById(id);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskItem> UpdateTask(int id, UpdateTaskDto updateDto)
    {
        var task = _taskService.UpdateTask(id, updateDto);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

   [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var deleted = _taskService.DeleteTask(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}