
//using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class Appointment
{
    public int ID { get; set; } //clave primaria

    public int patient_id { get; set; } //ID del paciente 

    public int doctor_id { get; set; } //ID del doctor 

    public int specialty_id { get; set; } //ID de la especialidad 
    public DateTime appointment_date { get; set; } //fecha de la cita

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AppointmentStatus status { get; set; } //estado de la cita

    [JsonIgnore] // Evitar serialización circular
    public virtual Administrator? Administrator { get; set; } 

    [JsonIgnore] 
    public virtual Doctor? Doctor { get; set; } //Propiedad de nevegacion hacia Doctor

    [JsonIgnore] 
    public virtual Patient? Patient { get; set; } 
    [JsonIgnore] 
    public virtual Specialty? Specialty { get; set; }


    //cconstructor sin parámetros
    public Appointment() { }

    //onstructor con parámetros
    public Appointment(int id, int patient_id, int doctor_id, int specialty_id, DateTime appointment_date, AppointmentStatus status)
    {
        this.ID = ID;
        this.patient_id = patient_id; // ID del paciente
        this.doctor_id = doctor_id;   // ID del doctor
        this.specialty_id = specialty_id; // ID de la especialidad
        this.appointment_date = appointment_date;
        this.status = status;
        
    }

    // Override del método ToString
    public override string ToString()
    {
        return $"Appointment ID: {ID}, Patient ID: {patient_id}, Doctor ID: {doctor_id}, Specialty ID: {specialty_id}, Date: {appointment_date}, Status: {status}";
    }

    // Enum p el estado del turno médico
    public enum AppointmentStatus
    {
        Scheduled = 0,    //Programada 0
        Completed = 1,    //Completada 1 
        Canceled = 2,     //Cancelada 2 
        Rescheduled = 3    //Reprogramada 3
    }
}
