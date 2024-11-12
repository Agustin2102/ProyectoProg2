using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adm_doctor")]
public class AdminDoctorController : ControllerBase
{

    private readonly IDoctorService _doctorService;
    private readonly AccountDbService _accountService;
    public AdminDoctorController(IDoctorService doctorService, AccountDbService accountService){
        this._doctorService = doctorService;
        this._accountService = accountService;
    }

    //[Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]


    //[ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("doctors")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<List<Doctor>> GetAll(){ // Obtiene todos los Doctores de la BD
        //Falta mostrar las especialidades de los medicos
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        try{
            
            return Ok(_doctorService.GetAll());
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    [HttpGet("doctor/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Doctor> GetById(int id)
    { //Obtengo un Doctor por su ID
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        try{
            Doctor? _doctor = _doctorService.GetById(id);

            if (_doctor == null) return NotFound("Doctor not found");
            else return Ok(_doctor);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]

    [HttpPost("doctor")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Doctor> Create([FromBody] DoctorDTO doctorDTO){
        // Verificar que el usuario tenga el rol adecuado
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid(); // Devuelve un código 403 Forbidden si el usuario no tiene permiso
        
        try{
            if(doctorDTO is null) return BadRequest("Doctor data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

            Doctor _doctor = _doctorService.Create(doctorDTO); // Creo al nuevo doctor

            if (_doctor is null) return BadRequest("Error creating doctor.");

            return CreatedAtAction(nameof(GetById), new { id = _doctor.Id }, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        } 
    
    
    }


    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    [HttpDelete("doctor/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult DeleteById(int id){

        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();

        try{ 
            var _doctor = _doctorService.GetById(id);

            if (_doctor is null) return NotFound("Doctor not found!!!");

            _doctorService.Delete(id);
            return NoContent();
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }
 

    [HttpPut("doctor/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Doctor> UpdateById(int id, [FromBody]DoctorDTO doctorDTO){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        try{

            if(doctorDTO is null) return BadRequest("Doctor data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

            Doctor? doctor = _doctorService.Update(id, doctorDTO);
            if (doctor is null) return NotFound(new { Message = $"No se pudo actualizar el doctor con id: {id}" });

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);

        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }





}