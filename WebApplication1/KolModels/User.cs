using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    [Key]
    public int IdUser { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}