using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Doctor : User{
    public int LicenseNumber { get; set; } // Número de licencia del médico

    [JsonIgnore]
    public virtual List<Appointment> Appointments {get; set;} // Lista de Turnos que tiene un doctor
    [JsonIgnore]
    public virtual List<Specialty> Specialty {get; set;} // Lista de Especialidades que tiene un doctor


    // Constructor sin parámetros
    public Doctor() { }

    // Constructor con parámetros que incluye los de User
    public Doctor(int id, string name, string lastName, int dni, string email, string telephoneNumber, int licenseNumber)
        : base(id, name, lastName, dni, email, telephoneNumber) // Llama al constructor de la clase base
    {
        LicenseNumber = licenseNumber; // Inicializa el número de licencia
        Appointments = new List<Appointment>();
        Specialty = new List<Specialty>();
    }


    public override string ToString(){
        return $"Doctor: {Name} {LastName}, DNI: {DNI}, Email: {Email}, Telephone: {TelephoneNumber}, License Number: {LicenseNumber}";
    }

}
