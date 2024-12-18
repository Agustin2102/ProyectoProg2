using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/administrators")]
public class AdministratorController : ControllerBase
{

    private readonly IAdministratorService _administratorService;

    private readonly AccountDbService _accountService;

    public AdministratorController(IAdministratorService administratorService, AccountDbService accountService)
    {
        this._administratorService = administratorService;
        this._accountService = accountService;
    }


    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/

    //[Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]


    [HttpGet]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<List<Administrator>> GetAllAdministrators()
    { // Obtiene todos los Doctores de la BD
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();

        return Ok(_administratorService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Administrator> GetById(int id)
    { //Obtengo un Doctor por su ID
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        Administrator? d = _administratorService.GetById(id);

        if (d == null) return NotFound("Adminstrator not found");
        else return Ok(d);
    }

    [HttpPost]
    public ActionResult<Administrator> Create(AdministratorDTO d)
    {   
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        Administrator _administrator = _administratorService.Create(d); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        if (_administrator == null) return BadRequest("Error creating administrator.");
        return CreatedAtAction(nameof(GetById), new { id = _administrator.Id }, _administrator); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult Delete(int id)
    {
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        var a = _administratorService.GetById(id);

        if (a == null) return NotFound("Administrador no encontrado!!!");

        _administratorService.Delete(id);
        return NoContent();
    }

    [HttpPut("administrator/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Administrator> UpdateAdministratorById(int id, [FromBody] AdministratorDTO administratorDTO)
    {
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        try{
            if(administratorDTO is null) return BadRequest("Administrtor data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

            Administrator? administrator = _administratorService.Update(id, administratorDTO);
            if (administrator is null) return NotFound(); // Si no se encontró el paciente, retorna 404 Not Found

            return CreatedAtAction(nameof(GetById), new { id = administrator.Id }, administrator); // Retorna el recurso actualizado

        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpPut("administrator")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Administrator> UpdateAdministrator([FromBody]AdministratorDTO administratorDTO){
        
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        try{

            string userName = _accountService.GetUserName();
            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            int? userId = _administratorService.GetId(userName);
            if(!userId.HasValue) return BadRequest("No se encontro el Id del Administrador");

            Administrator? administrator = _administratorService.Update((int)userId, administratorDTO);
            if (administrator is null) return NotFound(new { Message = $"No se pudo actualizar el doctor con id: {userId}" }); // Si no se encontró el paciente, retorna 404 Not Found

            return CreatedAtAction(nameof(GetById), new { id = administrator.Id }, administrator); // Retorna el recurso actualizado
        }
        catch (Exception e){
            Console.WriteLine(e.Message);

            return Problem(detail: e.Message, statusCode: 500);
        }
    }



}