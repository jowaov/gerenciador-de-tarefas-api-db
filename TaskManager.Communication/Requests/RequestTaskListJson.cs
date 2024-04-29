using TaskManager.Communication.Enums;
namespace TaskManager.Communication.Requests;
public class RequestTaskListJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; }
}
