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
    public ActionResult<Patient> Create(PatientDTO d){
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        
        Patient _patient = _patientService.Create(d); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        
        return CreatedAtAction(nameof(GetById), new { id = _patient.Id }, _patient); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
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
    public ActionResult<Patient> UpdatePatient(int id, PatientDTO updatedPatientDto)
    {
        if (!User.IsInRole("administrator") && !User.IsInRole("Administrator") && !User.IsInRole("ADMINISTRATOR")) return Forbid();
        // Asegúrate de que el ID en la URL coincida con el ID en el DTO
        if (id != updatedPatientDto.Id) return BadRequest("El ID del paciente en la URL no coincide con el ID del paciente en el cuerpo de la solicitud.");

        // Obtener el paciente existente
        var patient = _patientService.GetById(id);
        if (patient == null) return NotFound(); // Si no se encontró el paciente, retorna 404 Not Found
        

        // Asignar valores desde el DTO al paciente
        patient.Name = updatedPatientDto.Name;
        patient.LastName = updatedPatientDto.LastName;
        patient.DNI = updatedPatientDto.DNI;
        patient.Email = updatedPatientDto.Email;
        patient.TelephoneNumber = updatedPatientDto.TelephoneNumber;
        patient.DateOfBirth = updatedPatientDto.DateOfBirth;
        patient.Address = updatedPatientDto.Address;


        // Actualizar el paciente en la base de datos
        _patientService.Update(id, patient);
        return Ok(patient); // Retorna  para indicar que la actualización fue exitosa
    }






}