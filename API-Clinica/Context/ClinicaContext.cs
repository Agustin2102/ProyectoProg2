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
        modelBuilder.Entity<Doctor>(entity => {
            entity.Property(d => d.Name).HasColumnName("first_name");
            entity.Property(d => d.LastName).HasColumnName("last_name");
            entity.Property(d => d.DNI).HasColumnName("DNI");
            entity.Property(d => d.Email).HasColumnName("email");
            entity.Property(d => d.TelephoneNumber).HasColumnName("phone");
            entity.Property(d => d.LicenseNumber).HasColumnName("license_number");
        });

         modelBuilder.Entity<Patient>(entity => {
    entity.Property(p => p.Name).HasColumnName("first_name");
    entity.Property(p => p.LastName).HasColumnName("last_name");
    entity.Property(p => p.DNI).HasColumnName("DNI");
    entity.Property(p => p.Email).HasColumnName("email");
    entity.Property(p => p.TelephoneNumber).HasColumnName("phone");
    entity.Property(p => p.DateOfBirth)
          .HasColumnName("birth_date")
          .IsRequired(false); // Permite nulos en la columna birth_date
    entity.Property(p => p.Address).HasColumnName("address");
    entity.Property(p => p.MedicalHistory).HasColumnName("medical_history");
});

modelBuilder.Entity<Administrator>(entity => {
    entity.Property(a => a.Name).HasColumnName("first_name");
    entity.Property(a => a.LastName).HasColumnName("last_name");
    entity.Property(a => a.DNI).HasColumnName("DNI");
    entity.Property(a => a.Email).HasColumnName("email");
    entity.Property(a => a.TelephoneNumber).HasColumnName("phone");
});



/*
              entity.HasMany(a => a.Appointments)
           .WithMany(p => p.Patient)
           .UsingEntity(j => j.ToTable("LibroTema") );
           */
        

       
    }

    
    


}