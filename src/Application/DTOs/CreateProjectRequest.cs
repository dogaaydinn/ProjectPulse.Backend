namespace Application.DTOs;

public class CreateProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
}