using WebApplication1.Models;

namespace WebApplication1.DTO;

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public List<Prescription> Prescriptions { get; set; }
}