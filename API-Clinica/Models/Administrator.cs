using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Administrator : User{


    [JsonIgnore] //Indico que ignore la lista de turnos cuando se realiza a serialización
    public virtual List<Appointment>? Appointments {get; set;} // Lista de Turnos que puede acceder un administrador
    /*
        La palabra clave "virtual" le indica a Entity que utilice la tecnica de "lazy loading (carga diferenciada)"
        Significa que los datos del administrador y en particular en la lista de turnos que puede tener un administrador,
        no se van a cargar de forma automatica cuando de la Base de Datos el registro de un administrador se convierta en un Objeto,
        si no que estos tipos de datos en los que utilizan la palabra reservada "virtual" se cargan cuando realmente sea necesario
        acceder a ellos.
        Se utiliza para mejorar el rendimiento evitando cargar datos de forma inecesaria de la Base de Datos
    */

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
