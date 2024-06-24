using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Prescription_Medicament
{
    [Key]
    [Column(Order = 1)]
    public int IdMedicament { get; set; }
    [Key]
    [Column(Order = 2)]
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}