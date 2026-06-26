namespace TaskManagementApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public Enums.TaskItemStatus Status { get; set; } = Enums.TaskItemStatus.Todo;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
