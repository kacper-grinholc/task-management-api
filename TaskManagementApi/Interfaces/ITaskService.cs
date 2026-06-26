using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces;

public interface ITaskService
{
    List<TaskItem> GetTasks(
        Enums.TaskItemStatus? status,
        int pageNumber,
        int pageSize,
        string? sortBy,
        string? sortDirection);
    TaskItem? GetTaskById(int id);
    TaskItem CreateTask(CreateTaskDto taskDto);
    TaskItem? UpdateTask(int id, UpdateTaskDto updatedTask);
    bool DeleteTask(int id);
}
