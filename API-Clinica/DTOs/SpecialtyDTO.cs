using System.ComponentModel.DataAnnotations;

public class SpecialtyDTO{
    //Agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    protected string? Name {get; set;} // Nombre de la especialidad
}