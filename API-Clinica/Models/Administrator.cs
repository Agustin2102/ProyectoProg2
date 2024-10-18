using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Administrator : User{
    [JsonIgnore]
    public virtual List<Appointment> Appointments {get; set;} // Lista de Turnos que puede acceder un administrador

    // Constructor sin parámetros
    public Administrator() {}

    // Constructor con parámetros
    public Administrator(int id, string name, string lastName, int dni, string email, string telephoneNumber)
        : base(id, name, lastName, dni, email, telephoneNumber) // Llama al constructor de la clase base
    {
        this.Appointments = new List<Appointment>(); // Inicializa la lista
    }

    // Override del método ToString
    public override string ToString()
    {
        return $"Administrator ID: {Id}, Name: {Name}, Last Name: {LastName}, DNI: {DNI}, Email: {Email}, Telephone: {TelephoneNumber}";
    }
}
