using TaskManagementApi.DTOs;
using TaskManagementApi.Data;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

public List<TaskItem> GetTasks(
    Enums.TaskItemStatus? status,
    int pageNumber,
    int pageSize,
    string? sortBy,
    string? sortDirection)
{
    var query = _context.Tasks.AsQueryable();

    if (status is not null)
    {
        query = query.Where(task => task.Status == status);
    }

    var descending = sortDirection?.ToLower() == "desc";

    query = sortBy?.ToLower() switch
    {
        "title" => descending
            ? query.OrderByDescending(task => task.Title)
            : query.OrderBy(task => task.Title),

        "duedate" => descending
            ? query.OrderByDescending(task => task.DueDate)
            : query.OrderBy(task => task.DueDate),

        "createdat" => descending
            ? query.OrderByDescending(task => task.CreatedAt)
            : query.OrderBy(task => task.CreatedAt),

        _ => query.OrderByDescending(task => task.CreatedAt)
    };

    return query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public TaskItem? GetTaskById(int id)
    {
        return _context.Tasks.FirstOrDefault(task => task.Id == id);
    }

    public TaskItem CreateTask(CreateTaskDto taskDto)
    {
        var task = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            Status = taskDto.Status,
            DueDate = taskDto.DueDate
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        return task;
    }

    public TaskItem? UpdateTask(int id, UpdateTaskDto updatedTask)
    {
        var existingTask = _context.Tasks.FirstOrDefault(task => task.Id == id);

        if (existingTask is null)
        {
            return null;
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Status = updatedTask.Status;
        existingTask.DueDate = updatedTask.DueDate;

        _context.SaveChanges();

        return existingTask;
    }

    public bool DeleteTask(int id)
    {
        var task = _context.Tasks.FirstOrDefault(task => task.Id == id);

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return true;
    }
}
