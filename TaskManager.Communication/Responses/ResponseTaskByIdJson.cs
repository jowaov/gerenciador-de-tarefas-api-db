using TaskManager.Communication.Enums;

namespace TaskManager.Communication.Responses;
public class ResponseTaskByIdJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; }
}
