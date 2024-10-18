System.ComponentModel.DataAnnotations;
public class DoctorDTO{
    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    protected string? Name {get; set;} // Nombre del Doctor
    
    [Required(ErrorMessage = "El campo LastName es requerido.")]
    protected string? LastName {get; set;} // Apellido del Doctor
    
    [Required(ErrorMessage = "El campo DNI es requerido.")]
    protected int? DNI {get; set;} // DNI del Doctor
    
    [Required(ErrorMessage = "El campo Email es requerido.")]
    protected string? Email {get; set;} // Correo electrónico del Doctor
    
    [Required(ErrorMessage = "El campo TelephoneNumber es requerido.")]
    protected string? TelephoneNumber {get; set;} // Número de teléfono del Doctor

    [Required(ErrorMessage = "El campo LicenseNumber es requerido.")]
    protected int? LicenseNumber { get; set; } // Número de licencia del Doctor
}