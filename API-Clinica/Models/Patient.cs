using System.Collections.Generic;
using Newtonsoft.Json;

public class Patient : User
{
    public string DateOfBirth {get; set;}
    public string Address {get; set;}
    public string MedicalHistory {get; set;}

    [JsonIgnore]
    public virtual List<Appointment> Appointments {get; set;} // Lista de Turnos que tiene un paciente

    // Constructor sin parámetros
    public Patient(){}

    // Constructor con parámetros
    public Patient(int id, string name, string lastName, int DNI, string email, string telephoneNumber, string dateOfBirth, string address, string medicalHistory)
        : base(id, name, lastName, DNI, email, telephoneNumber) // Llama al constructor de la clase base
    {
        DateOfBirth = dateOfBirth;
        Address = address;
        MedicalHistory = medicalHistory;
        Appointments = new List<Appointment>(); // Inicializa la lista
    }


    // Override del método ToString
    public override string ToString(){
        return $"Patient: {Name} {LastName}, DNI: {DNI}, Email: {Email}, Telephone: {TelephoneNumber}, Date of Birth: {DateOfBirth}, Address: {Address}, Medical History: {MedicalHistory}";
    }
}
