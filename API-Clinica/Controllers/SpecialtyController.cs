using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

[ApiController] 
[Route("api/specialty")] 
public class SpecialtyController : ControllerBase 
{
    private readonly ISpecialtyService _specialtyService; //para acceder a los servicios de especialidad
    private readonly IDoctorService _doctorService; //para acceder a los servicios de doctor

    
    public SpecialtyController(ISpecialtyService specialtyService, IDoctorService doctorService) 
    {
        this._specialtyService = specialtyService; // inicializa el servicio de especialidad
        this._doctorService = doctorService; // inicializa el servicio de doctor
    }


    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]


    
    [HttpGet] 
    public ActionResult<IEnumerable<Specialty>> GetAll() 
    {
        return Ok(_specialtyService.GetAll()); // retorna una lista de especialidades
    }


    [HttpGet("{id}")] 
    public ActionResult<Specialty> GetById(int id) 
    {
        var specialty = _specialtyService.GetById(id); // busca la especialidad por id
        if (specialty == null) // verifica si no se encontro la especialidad
            return NotFound("Specialty not found."); // retorna 404 si no se encuentra
        return Ok(specialty); // retorna la especialidad encontrada
    }


    [HttpPost] 
    public ActionResult<Specialty> Create([FromBody] SpecialtyDTO specialtyDto) 
    {
        if (!ModelState.IsValid) // verifica si el modelo es valido
            return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

        var newSpecialty = _specialtyService.Create(specialtyDto); // crea la nueva especialidad
        return CreatedAtAction(nameof(GetById), new { id = newSpecialty.Id }, newSpecialty); // retorna 201 con la nueva especialidad
    }

  
    [HttpDelete("{id}")] 
    public IActionResult DeleteSpecialty(int id) 
    {
        var specialty = _specialtyService.GetById(id); // busca la especialidad por id
        if (specialty == null) // verifica si no se encontro la especialidad
        {
            return NotFound(); // devuelve 404 si no se encuentra
        }

        _specialtyService.Delete(id); // elimina la especialidad

        return Ok(new { message = "La especialidad se eliminó correctamente." }); // retorna 200 con mensaje de exito
    }

    // metodo para actualizar una especialidad por su id
    [HttpPut("{id}")] 
    public IActionResult Update(int id, [FromBody] SpecialtyDTO specialtyDto) 
    {
        if (!ModelState.IsValid) // verifica si el modelo es valido
            return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

        var updatedSpecialty = _specialtyService.Update(id, new Specialty(id, specialtyDto.Name)); // actualiza la especialidad
        if (updatedSpecialty == null) // verifica si la especialidad no se encontro
            return NotFound("Specialty not found."); // retorna 404 si no se encuentra

        return Ok(updatedSpecialty); // retorna la especialidad actualizada
    }
}
