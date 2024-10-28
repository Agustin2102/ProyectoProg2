using Microsoft.EntityFrameworkCore;

public class ClinicaContext : DbContext
{
    public DbSet<Administrator> Administrator { get; set; } // Singular
    public DbSet<Appointment> Appointment { get; set; } // Singular
    public DbSet<Doctor> Doctor { get; set; } // Singular
    public DbSet<Patient> Patient { get; set; } // Singular
    public DbSet<Specialty> Specialty { get; set; } // Singular

    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }

    // Realizo el mapeado de la base de datos
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Mapeo para la entidad Doctor
    modelBuilder.Entity<Doctor>(entity =>
    {
        ///entity.HasKey(d => d.ID); // Configura ID como clave primaria
       // entity.Property(d => d.ID).HasColumnName("ID"); // Mapea la propiedad ID
        entity.Property(d => d.Name).HasColumnName("first_name").IsRequired(); // Mapea la propiedad FirstName y la establece como requerida
        entity.Property(d => d.LastName).HasColumnName("last_name").IsRequired(); // Mapea la propiedad LastName y la establece como requerida
        entity.Property(d => d.DNI).HasColumnName("DNI").IsRequired(); // Mapea DNI y lo establece como requerido
        entity.Property(d => d.Email).HasColumnName("email").IsRequired(); // Mapea Email y lo establece como requerido
        entity.Property(d => d.TelephoneNumber).HasColumnName("phone"); // Mapea Phone (opcional)
        entity.Property(d => d.LicenseNumber).HasColumnName("license_number").IsRequired(); // Mapea LicenseNumber y lo establece como requerido
    });

    // Mapeo para la entidad Patient
    modelBuilder.Entity<Patient>(entity =>
    {
        //entity.HasKey(p => p.ID); // Configura ID como clave primaria
        //entity.Property(p => p.ID).HasColumnName("ID"); // Mapea la propiedad ID
        entity.Property(p => p.Name).HasColumnName("first_name").IsRequired(); // Mapea FirstName y lo establece como requerido
        entity.Property(p => p.LastName).HasColumnName("last_name").IsRequired(); // Mapea LastName y lo establece como requerido
        entity.Property(p => p.DNI).HasColumnName("DNI").IsRequired(); // Mapea DNI y lo establece como requerido
        entity.Property(p => p.Email).HasColumnName("email").IsRequired(); // Mapea Email y lo establece como requerido
        entity.Property(p => p.TelephoneNumber).HasColumnName("phone"); // Mapea Phone (opcional)
        entity.Property(p => p.DateOfBirth).HasColumnName("birth_date").IsRequired(); // Mapea BirthDate y lo establece como requerido
        entity.Property(p => p.Address).HasColumnName("address"); // Mapea Address (opcional)
        entity.Property(p => p.MedicalHistory).HasColumnName("medical_history"); // Mapea MedicalHistory (opcional)
    });

    // Mapeo para la entidad Administrator
    modelBuilder.Entity<Administrator>(entity =>
    {
        // entity.HasKey(a => a.ID); // Configura ID como clave primaria
        // entity.Property(a => a.ID).HasColumnName("ID"); // Mapea la propiedad ID
        entity.Property(a => a.Name).HasColumnName("first_name").IsRequired(); // Mapea FirstName y lo establece como requerido
        entity.Property(a => a.LastName).HasColumnName("last_name").IsRequired(); // Mapea LastName y lo establece como requerido
        entity.Property(a => a.DNI).HasColumnName("DNI").IsRequired(); // Mapea DNI y lo establece como requerido
        entity.Property(a => a.Email).HasColumnName("email").IsRequired(); // Mapea Email y lo establece como requerido
        entity.Property(a => a.TelephoneNumber).HasColumnName("phone"); // Mapea Phone (opcional)
    });

    // Mapeo para la entidad Specialty
    modelBuilder.Entity<Specialty>(entity =>
    {
        //entity.HasKey(s => s.ID); // Configura ID como clave primaria
        //entity.Property(s => s.ID).HasColumnName("ID"); // Mapea la propiedad ID
        entity.Property(s => s.Name).HasColumnName("name").IsRequired(); // Mapea Name y lo establece como requerido
    });

    // Mapeo para la entidad Appointment
    modelBuilder.Entity<Appointment>(entity =>
    {
        entity.Property(e => e.ID).HasColumnName("id");
        entity.Property(e => e.patient_id).HasColumnName("patient_id");
        entity.Property(e => e.doctor_id).HasColumnName("doctor_id");
        entity.Property(e => e.specialty_id).HasColumnName("specialty_id");
        entity.Property(e => e.appointment_date).HasColumnName("appointment_date");
        entity.Property(e => e.status).HasColumnName("status");
        entity.Property(e => e.administrator_id).HasColumnName("administrator_id");
    });


}

    // Aquí es donde habilitas el logging
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         optionsBuilder.UseSqlServer("Data Source=YAMILACAVIGLION\\SQLEXPRESS01; Initial Catalog=ClinicaDb; user id=DB_USER; password=DB_PASSsss; TrustServerCertificate=true")
    //                       .LogTo(Console.WriteLine, LogLevel.Information);
    //     }
    // }
}
