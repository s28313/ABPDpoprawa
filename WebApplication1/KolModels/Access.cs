using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Access
{
    [Key]
    [Column(Order = 1)]
    public int IdUser { get; set; }
    [Key]
    [Column(Order = 2)]
    public int IdProject { get; set; }
}