using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Task = WebApplication1.Models.Task;

public class WebApplicationContext : DbContext
{
    public WebApplicationContext(DbContextOptions<WebApplicationContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>().HasKey(l => new { l.IdPrescription, l.IdMedicament });
        modelBuilder.Entity<Access>().HasKey(access => new { access.IdUser, access.IdProject });
    }
    //TEMPLATE
    public DbSet<Prescription> Prescription { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Medicament> Medicament { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }
    
    
    //Poprawa
    public DbSet<Access> Access { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Task> Task { get; set; }
    public DbSet<User> User { get; set; }


}