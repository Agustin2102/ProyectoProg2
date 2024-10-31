using System.ComponentModel.DataAnnotations;

public class PatientDTO{

     //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int? Id {get; set;} // Nombre del Paciente
    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    public string? Name {get; set;} // Nombre del Paciente
    
    [Required(ErrorMessage = "El campo LastName es requerido.")]
    public string? LastName {get; set;} // Apellido del Paciente
    
    [Required(ErrorMessage = "El campo DNI es requerido.")]
    public int DNI {get; set;} // DNI del Paciente
    
    [Required(ErrorMessage = "El campo Email es requerido.")]
    public string? Email {get; set;} // Correo electrónico del Paciente
    
    [Required(ErrorMessage = "El campo TelephoneNumber es requerido.")]
    public string? TelephoneNumber {get; set;} // Número de teléfono del Paciente

    [Required(ErrorMessage = "El campo DateOfBirth es requerido.")]
    public string? DateOfBirth {get; set;} // Fecha de nacimiento del Paciente

    [Required(ErrorMessage = "El campo Address es requerido.")]
    public string? Address {get; set;} // Direccion del Paciente

      [Required(ErrorMessage = "El campo MedicalHistory es requerido.")]
    public string? MedicalHistory {get; set;} // Direccion del Paciente
}