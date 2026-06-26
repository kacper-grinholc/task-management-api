using System.ComponentModel.DataAnnotations;
namespace TaskManagementApi.DTOs;

public class UpdateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    [Required]
    public Enums.TaskItemStatus Status { get; set; } = Enums.TaskItemStatus.Todo;
    public DateTime? DueDate { get; set; }
}
