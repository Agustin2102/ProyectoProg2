using System.ComponentModel.DataAnnotations;

public class AppointmentDTO{

    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo IdPatient es requerido.")]
    public int? IdPatient { get; set; } // ID del paciente

    [Required(ErrorMessage = "El campo IdDoctor es requerido.")]
    public int? IdDoctor { get; set; } // ID del doctor

    [Required(ErrorMessage = "El campo IdSpecialty es requerido.")]
    public int? IdSpecialty { get; set; } // ID de la especialidad

    [Required(ErrorMessage = "El campo AppointmentDate es requerido.")]
    public string? AppointmentDate { get; set; } // Fecha de la cita

    //[Required(ErrorMessage = "El campo Status es requerido.")]
    //public string? Status { get; set; } // Estado de la cita (e.g., Programada, Cancelada, Completada)
}