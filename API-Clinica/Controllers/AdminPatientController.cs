using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

[ApiController] 
[Route("api/admin_patients")]
public class AdminPatientController : ControllerBase{
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;
    private readonly AccountDbService _accountService;
    public AdminPatientController(IPatientService patientService, IAppointmentService appointmentService, AccountDbService accountService){
        this._patientService = patientService;
        this._appointmentService = appointmentService;
        this._accountService = accountService;
    }


    //[Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]


    [HttpGet("patients")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<List<Patient>> GetAllPatients(){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        return Ok(_patientService.GetAll());
    }

    [HttpGet("patient/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Patient> GetById(int id){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        Patient? d = _patientService.GetById(id);

        if (d == null) return NotFound("Pacient not found");
        else return Ok(d);
    }

    [HttpPost("patient")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Patient> Create([FromBody] PatientDTO patientDTO){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        if(patientDTO is null) return BadRequest("Patient data is required.");
        if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

        Patient _patient = _patientService.Create(patientDTO);

        if(_patient is null) return BadRequest("Error creating patient.");
        
        return CreatedAtAction(nameof(GetById), new { id = _patient.Id }, _patient); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo paciente
    }

    [HttpDelete("patient/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult Delete(int id){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        var a = _patientService.GetById(id);

        if (a == null) return NotFound("Paciente no encontrado!!!"); 

        _patientService.Delete(id);
        return NoContent();
    }


    [HttpPut("patient/{id}")]
    [Authorize(Roles = "administrator,Administrator,ADMINISTRATOR")]
    public ActionResult<Patient> UpdatePatient(int id, PatientDTO patientDTO) {

        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();

        try{
            if(patientDTO is null) return BadRequest("Patient data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo
            
            Patient? patient = _patientService.Update(id, patientDTO);
            if(patient is null) NotFound(new { Message = $"No se pudo actualizar el paciente con id: {id}" }); 

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }






}