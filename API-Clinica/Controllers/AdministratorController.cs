using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/administrators")]
public class AdministratorController : ControllerBase{

    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService){
        this._administratorService = administratorService;
    }

    
    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/

    [HttpGet]
    public ActionResult<List<Administrator>> GetAllAdministrators(){ // Obtiene todos los Doctores de la BD
    
        return Ok(_administratorService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Administrator> GetById(int id){ //Obtengo un Doctor por su ID
        Administrator? d = _administratorService.GetById(id);

        if(d == null) return NotFound("Adminstrator not found");
        else return Ok(d);
    }

     [HttpPost]
    public ActionResult<Administrator> Create(AdministratorDTO d){
        Administrator _administrator = _administratorService.Create(d); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        return CreatedAtAction(nameof(GetById), new {id = _administrator.Id}, _administrator); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }

     [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    var a = _administratorService.GetById(id);

    if (a == null)
    { return NotFound("Administrador no encontrado!!!");}

    _administratorService.Delete(id);
    return NoContent();
  }

   [HttpPut("{id}")]
public ActionResult<Administrator> UpdateAdministrator(int id, Administrator updatedAdministrator) {
    // Asegurarse de que el ID del paciente en la solicitud coincida con el ID del parámetro
    if (id != updatedAdministrator.Id) {
        return BadRequest("El ID del administrador en la URL no coincide con el ID del administrador en el cuerpo de la solicitud.");
    }

    var administrator = _administratorService.Update(id, updatedAdministrator);

    if (administrator is null) {
        return NotFound(); // Si no se encontró el paciente, retorna 404 Not Found
    }

    return CreatedAtAction(nameof(GetById), new { id = administrator.Id }, administrator); // Retorna el recurso actualizado
}
}