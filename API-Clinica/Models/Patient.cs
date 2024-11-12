using System.Collections.Generic;
using Newtonsoft.Json;

public class Patient : User
{
    public string? DateOfBirth {get; set;}
    public string? Address {get; set;}
    public string? MedicalHistory {get; set;}

    [JsonIgnore]//Indico que ignore la lista de turnos cuando se realiza a serialización
    public virtual List<Appointment> Appointments {get; set;} // Lista de Turnos que tiene un paciente
    /*
        La palabra clave "virtual" le indica a Entity que utilice la tecnica de "lazy loading (carga diferenciada)"
        Significa que los datos del paciente y en particular en la lista de turnos que puede tener un paciente,
        no se van a cargar de forma automatica cuando de la Base de Datos el registro de un paciente se convierta en un Objeto,
        si no que estos tipos de datos en los que utilizan la palabra reservada "virtual" se cargan cuando realmente sea necesario
        acceder a ellos.
        Se utiliza para mejorar el rendimiento evitando cargar datos de forma inecesaria de la Base de Datos
    */

    // Constructor sin parámetros
    public Patient(){}

}
