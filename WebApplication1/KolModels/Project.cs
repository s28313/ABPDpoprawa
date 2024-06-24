using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Project
{
    [Key]
    public int IdProject { get; set; }
    public string Name { get; set; }
    public int IdDefaultAssignee { get; set; }
}