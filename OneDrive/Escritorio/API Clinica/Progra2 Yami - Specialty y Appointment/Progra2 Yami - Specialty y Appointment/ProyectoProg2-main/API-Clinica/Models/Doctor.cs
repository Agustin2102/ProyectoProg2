using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Doctor : User{ 
    public int LicenseNumber { get; set; } // Número de licencia del médico

    [JsonIgnore]//Indico que ignore la lista de turnos cuando se realiza a serialización
    public virtual List<Appointment> Appointments {get; set;} // Lista de Turnos que tiene un doctor
    
    [JsonIgnore]//Indico que ignore la lista de especialidades cuando se realiza a serialización
    public virtual List<Specialty> Specialty {get; set;} // Lista de Especialidades que tiene un doctor
    /*
        La palabra clave "virtual" le indica a Entity que utilice la tecnica de "lazy loading (carga diferenciada)"
        Significa que los datos del doctor y en particular en la lista de turnos que puede tener un doctor,
        no se van a cargar de forma automatica cuando de la Base de Datos el registro de un doctor se convierta en un Objeto,
        si no que estos tipos de datos en los que utilizan la palabra reservada "virtual" se cargan cuando realmente sea necesario
        acceder a ellos.
        Se utiliza para mejorar el rendimiento evitando cargar datos de forma inecesaria de la Base de Datos
    */

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
