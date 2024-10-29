using System.ComponentModel.DataAnnotations;

public class SpecialtyDTO{
    //agrego en el DTO los campos minimos que tengo que completar para poder crear el objeto

    [Required(ErrorMessage = "El campo Name es requerido.")]
    public string? Name {get; set;} // Nombre de la especialidad
}