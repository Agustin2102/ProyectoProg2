using System.ComponentModel.DataAnnotations;

public class PatientDTO{
    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    protected string? Name {get; set;} // Nombre del Paciente
    
    [Required(ErrorMessage = "El campo LastName es requerido.")]
    protected string? LastName {get; set;} // Apellido del Paciente
    
    [Required(ErrorMessage = "El campo DNI es requerido.")]
    protected int? DNI {get; set;} // DNI del Paciente
    
    [Required(ErrorMessage = "El campo Email es requerido.")]
    protected string? Email {get; set;} // Correo electrónico del Paciente
    
    [Required(ErrorMessage = "El campo TelephoneNumber es requerido.")]
    protected string? TelephoneNumber {get; set;} // Número de teléfono del Paciente

    [Required(ErrorMessage = "El campo DateOfBirth es requerido.")]
    public string? DateOfBirth {get; set;} // Fecha de nacimiento del Paciente

    [Required(ErrorMessage = "El campo Address es requerido.")]
    public string? Address {get; set;} // Direccion del Paciente
}