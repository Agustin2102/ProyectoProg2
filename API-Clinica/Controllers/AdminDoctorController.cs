using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adm_doctor")]
public class AdminDoctorController : ControllerBase{

    private readonly IDoctorService _doctorService;
    private readonly AccountDbService _accountService;
    public AdminDoctorController(IDoctorService doctorService, AccountDbService accountService){
        this._doctorService = doctorService;
        this._accountService = accountService;
    }


    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]


    //[ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("doctors")]
    public ActionResult<List<Doctor>> GetAll(){ // Obtiene todos los Doctores de la BD
        //Falta mostrar las especialidades de los medicos
        try
      {
        return Ok(_doctorService.GetAll());
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
      }
    }


    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    [HttpGet("doctor/{id}")]
    public ActionResult<Doctor> GetById(int id){ //Obtengo un Doctor por su ID
        Doctor? _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found");
        else return Ok(_doctor);
    }
    

    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    [HttpPost("doctor")]
    public ActionResult<Doctor> Create([FromBody] DoctorDTO doctor){
        if(doctor == null) return BadRequest("Doctor data is required.");
        if(!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

        Doctor _doctor = _doctorService.Create(doctor); // Creo al nuevo doctor

        if(_doctor == null) return BadRequest("Error creating doctor.");

        return CreatedAtAction(nameof(GetById), new {id = _doctor.Id}, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }


    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    [HttpDelete("doctor/{id}")]
    public ActionResult DeleteById(int id){
        var _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found!!!");

        _doctorService.Delete(id);
        return NoContent();
    }


    [HttpPut("doctor/{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> UpdateById(int id, DoctorDTO d){
        try{

            Doctor? doctor = _doctorService.Update(id, d);

            if(doctor is null) return NotFound(new {Message = $"No se pudo actualizar el doctor con id: {id}"});

            return CreatedAtAction(nameof(GetById), new{id = doctor.Id}, doctor);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);

            return Problem(detail: e.Message, statusCode: 500);
        }
    }





}