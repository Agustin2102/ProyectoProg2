using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

[ApiController] 
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;
    private readonly AccountDbService _accountService;
    public PatientController(IPatientService patientService, IAppointmentService appointmentService, AccountDbService accountService){
        this._patientService = patientService;
        this._appointmentService = appointmentService;
        this._accountService = accountService;
    }

    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/


    [Authorize(Roles = "patient,Patient,PATIENT")]


    [HttpGet("{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult<Patient> GetById(int id)
    {
        Patient? d = _patientService.GetById(id);

        if (d == null) return NotFound("Pacient not found");
        else return Ok(d);
    }


    [HttpPut("{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult<Patient> UpdatePatient(int id, PatientDTO updatedPatientDto)
    {
        // Asegúrate de que el ID en la URL coincida con el ID en el DTO
        if (id != updatedPatientDto.Id)
        {
            return BadRequest("El ID del paciente en la URL no coincide con el ID del paciente en el cuerpo de la solicitud.");
        }

        // Obtener el paciente existente
        var patient = _patientService.GetById(id);
        if (patient == null)
        {
            return NotFound(); // Si no se encontró el paciente, retorna 404 Not Found
        }

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


    [HttpGet("patient")]
    //[Authorize(Roles = "Patient")]
    public ActionResult<Doctor> GetPatient(){
        try{
            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if(patient == null) return NotFound("Patient not found");

            var _patient= new{
                id = patient.Id,
                name = patient.Name,
                lastName = patient.LastName,
                dni = patient.DNI,
                dateOfBirth = patient.DateOfBirth,
                email = patient.Email,
                telephone = patient.TelephoneNumber,
                address = patient.Address
            };

            return Ok(_patient);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("appointments")]
    public ActionResult<List<Appointment>> GetAllAppointments(){
        try{

            //Obtengo el nombre del suario (paciente) del los claims
            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if(patient == null) return NotFound("Patient not found");
            //Verifico si el paciente tiene turnos
            if(patient.Appointments == null || !patient.Appointments.Any()) return NotFound("No appointments found for this doctor.");

            var appointments = patient.Appointments?.Select(a => new {
                //Datos propios del turno (sin modificación)
                id = a.ID,
                date = a.appointment_date,
                status = a.status.ToString(),
                specialty = a.Specialty.Name,

                //Modifico los datos que se muestran del doctor
                Doctor = new {
                    name = a.Doctor.Name,
                    lastName = a.Doctor.LastName,
                    contact = a.Doctor.Email
                },

                //Modifico los datos que se puestran del paciente
                Patient = new {
                    name = patient.Name,
                    lastName = patient.LastName,
                    contact = patient.Email
                }

            }).ToList();

            return Ok(appointments);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }

    }

    [HttpGet("appointment/{id}")]
    public ActionResult<Appointment> GetAppointmentById(int id){
        try{
            int appointmentId = id;

            //Obtengo el nombre del suario (paciente) del los claims
            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if(patient == null) return NotFound("Patient not found");
            //Verifico si el paciente tiene turnos
            if(patient.Appointments == null || !patient.Appointments.Any()) return NotFound("No appointments found for this doctor.");

            //Busco el turno especifico del paciente
            var appointment = patient.Appointments.FirstOrDefault(a => a.ID == appointmentId);

            //Verifico si el turno con el id espeficicado esxiste
            if(appointment == null) return NotFound("The appointment with the specified Id was not found.");

            var responseAppointment = new {
                //Datos propios del turno (sin modificar)
                id = appointment.ID,
                date = appointment.appointment_date,
                status = appointment.status.ToString(),
                specialty = appointment.Specialty.Name,

                //Modifico los datos que se muestran del Doctor
                Doctor = new {
                    name = appointment.Doctor.Name,
                    lastName = appointment.Doctor.LastName,
                    contact = appointment.Doctor.Email
                },

                //Modifico los datos que se muestran del paciente

                Patient = new {
                    name = patient.Name,
                    lastName = patient.LastName,
                    contact = patient.Email
                }
            };

            return Ok(responseAppointment);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            // Si ocurre un error, devuelve un mensaje con el error y un código de estado 500
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpDelete("appointment/{id}")]
    public IActionResult DeleteAppointment(int id) 
    {
        var appointment = _appointmentService.GetById(id); // busca la cita por id
        
        // verifica si no se encontro la cita
        if (appointment == null) return NotFound(); // devuelve 404 si no se encuentra

        _appointmentService.Delete(id); // elimina la cita

        return Ok(new { message = "El turno se eliminó correctamente." }); // retorna 200 con mensaje de exito
    }







    /*[HttpGet]
    //[Authorize(Roles = "admin")]
    public ActionResult<List<Patient>> GetAllPatients()
    {
      //Console.WriteLine(_patientService.GetAll());
        return Ok(_patientService.GetAll());
    } */


    /* [HttpPost]
    //[Authorize(Roles = "admin")]
    public ActionResult<Patient> Create(PatientDTO d)
    {
        Patient _patient = _patientService.Create(d); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        return CreatedAtAction(nameof(GetById), new { id = _patient.Id }, _patient); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    } */

    /* [HttpDelete("{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult Delete(int id)
    {
        var a = _patientService.GetById(id);

        if (a == null)
        { return NotFound("Paciente no encontrado!!!"); }

        _patientService.Delete(id);
        return NoContent();
    } */

}