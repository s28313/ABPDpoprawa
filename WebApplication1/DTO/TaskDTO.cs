using WebApplication1.Models;

namespace WebApplication1.DTO;

public class TaskDTO
{
    public int IdTask { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IdProject { get; set; }
    public User Reporter { get; set; }
    public User Assignee { get; set; }
    
    
}