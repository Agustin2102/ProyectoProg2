using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AppointmentDTO
{
    [Required(ErrorMessage = "El campo Id de Paciente es requerido.")]
    public int patient_id  { get; set; } // ID del paciente

    [Required(ErrorMessage = "El campo Id de Doctor es requerido.")]
    public int doctor_id { get; set; }

    [Required(ErrorMessage = "El campo Id de Especialidad es requerido.")]
    public int specialty_id { get; set; }

    [Required(ErrorMessage = "El campo Fecha del Turno es requerido.")]
    public DateTime appointment_date { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    [Required(ErrorMessage = "El campo estado del turno es requerido.")]
    public Appointment.AppointmentStatus status  { get; set; }
}
