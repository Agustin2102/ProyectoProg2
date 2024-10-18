using Microsoft.EntityFrameworkCore;

public class ClinicaContext : DbContext{
    public DbSet<Administrator> Administrator {get; set;}
    public DbSet<Appointment> Appointment {get; set;}
    public DbSet<Doctor> Doctor {get; set;}
    public DbSet<Patient> Patient {get; set;}
    public DbSet<Specialty> Specialty {get; set;}


    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options){}

    //Realizo el mapeado de la base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //modelBuilder.Entity<Doctor>(entity => {
        //    entity.Property(d => d.Name);
        //    entity.Property(d => d.LastName);
        //});
    }

}