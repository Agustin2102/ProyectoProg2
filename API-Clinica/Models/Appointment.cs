public class Appointment{
    public int Id { get; set; } // ID de la cita
    public int IdPatient { get; set; } // ID del paciente
    public int IdDoctor { get; set; } // ID del doctor
    public int IdSpecialty { get; set; } // ID de la especialidad
    public string AppointmentDate { get; set; } // Fecha de la cita
    public string Status { get; set; } // Estado de la cita (e.g., Programada, Cancelada, Completada)

    // Constructor sin parámetros
    public Appointment(){}

    // Constructor con parámetros
    public Appointment(int id, int idPatient, int idDoctor, int idSpecialty, string appointmentDate, string status){
        this.Id = id;
        this.IdPatient = idPatient;
        this.IdDoctor = idDoctor;
        this.IdSpecialty = idSpecialty;
        this.AppointmentDate = appointmentDate;
        this.Status = status;
    }

    // Override del método ToString
    public override string ToString()
    {
        return $"Appointment ID: {Id}, Patient ID: {IdPatient}, Doctor ID: {IdDoctor}, Specialty ID: {IdSpecialty}, Date: {AppointmentDate}, Status: {Status}";
    }
}
