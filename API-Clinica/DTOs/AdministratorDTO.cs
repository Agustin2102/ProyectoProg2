using System.ComponentModel.DataAnnotations;

public class AdministratorDTO{

    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    protected string? Name {get; set;} // Nombre del administrador
    
    [Required(ErrorMessage = "El campo LastName es requerido.")]
    protected string? LastName {get; set;} // Apellido del usuario
    
    [Required(ErrorMessage = "El campo DNI es requerido.")]
    protected int? DNI {get; set;} // DNI del usuario
    
    [Required(ErrorMessage = "El campo Email es requerido.")]
    protected string? Email {get; set;} // Correo electrónico del usuario
    
    [Required(ErrorMessage = "El campo TelephoneNumber es requerido.")]
    protected string? TelephoneNumber {get; set;} // Número de teléfono del usuario

}