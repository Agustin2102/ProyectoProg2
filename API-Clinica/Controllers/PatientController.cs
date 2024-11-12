using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase{
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;
    private readonly IAppointmentService _appointmentService;
    private readonly AccountDbService _accountService;
    public PatientController(IPatientService patientService, IAppointmentService appointmentService, AccountDbService accountService,
                            IDoctorService doctorService, ISpecialtyService specialtyService)
    {
        this._patientService = patientService;
        this._doctorService = doctorService;
        this._specialtyService = specialtyService;
        this._appointmentService = appointmentService;
        this._accountService = accountService;
    }

    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/


    [Authorize(Roles = "patient,Patient,PATIENT")]


    [HttpGet("{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult<Patient> GetPatientById(int id){
        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        Patient? patient = _patientService.GetById(id);

        if (patient == null) return NotFound("Pacient not found");
        else return Ok(patient);
    }

    [HttpPut("patient")]
    [Authorize(Roles = "patient,Patient,PATIENT")]
    public ActionResult<Patient> UpdatePatient(PatientDTO patientDTO) {

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        try{
            if(patientDTO is null) return BadRequest("Patient data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo
            
            string userName = _accountService.GetUserName();
            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            int? userId = (int)_patientService.GetId(userName);
            if(!userId.HasValue) return BadRequest("No se encontro el Id del Paciente");

            Patient? patient = _patientService.Update((int)userId, patientDTO);
            if(patient is null) NotFound(new { Message = $"No se pudo actualizar el paciente con id: {userId}" }); 

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("patient")]
    //[Authorize(Roles = "Patient")]
    public ActionResult<Doctor> GetPatient()
    {
        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        try{
            string userName = _accountService.GetUserName();
            if (string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if (patient == null) return NotFound("Patient not found");

            var _patient = new {
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("appointments")]
    public ActionResult<List<Appointment>> GetAllAppointments_Patient(){

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        try{

            //Obtengo el nombre del suario (paciente) del los claims
            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if (patient == null) return NotFound("Patient not found");
            //Verifico si el paciente tiene turnos
            if (patient.Appointments == null || !patient.Appointments.Any()) return NotFound("No appointments found for this doctor.");

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
    public ActionResult<Appointment> GetAppointmentById_Patient(int id){

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        try{

            int appointmentId = id;

            //Obtengo el nombre del suario (paciente) del los claims
            string userName = _accountService.GetUserName();

            if (string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            //Una vez tengo el nombre del usuario lo busco en la BD (al paciente se le agregan los turnos asociados, y a los turnos los demas objetos que estan asociados)
            var patient = _patientService.GetByName(userName);

            //Verifico si el paciente existe
            if (patient == null) return NotFound("Patient not found");
            //Verifico si el paciente tiene turnos
            if (patient.Appointments == null || !patient.Appointments.Any()) return NotFound("No appointments found for this doctor.");

            //Busco el turno especifico del paciente
            var appointment = patient.Appointments.FirstOrDefault(a => a.ID == appointmentId);

            //Verifico si el turno con el id espeficicado esxiste
            if (appointment == null) return NotFound("The appointment with the specified Id was not found.");

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



    [HttpGet("doctors")]
    public ActionResult<List<Doctor>> GetAllDoctors_Patient(){

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        try{

            var doctors = _doctorService.GetAll();
            if (doctors == null || !doctors.Any()) return BadRequest("No pudo acceder a la lista de doctores.");


            //Los unicos datos que se va a querer mostrar en este caso es informacion basica de los doctores
            var _doctors = doctors.Select(d => new {
                id = d.Id,
                name = d.Name,
                lastName = d.LastName,
                specialty = d.Specialties.Select(s => s.Name).ToList() // lista de nombres de especialidades
            }).ToList();

            return Ok(_doctors);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }

    [HttpGet("doctor/{id}")]
    public ActionResult<Doctor> GetDoctorById_Patient(int id){ //Obtengo un Doctor por su ID
        
        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        
        Doctor? doctor = _doctorService.GetById(id);

        if (doctor == null) return NotFound("Doctor not found");

        var _doctor = new {
            lincenseNumber = doctor.LicenseNumber,
            name = doctor.Name,
            lastName = doctor.LastName,
            email = doctor.Email,
            telephone = doctor.TelephoneNumber,
            specialty = doctor.Specialties.Select(d => d.Name).ToList() 
        };

        return Ok(_doctor);
    }




    /*----METODOS DE TURNOS PARA LOS USUARIOS----*/

    [HttpGet("specialties")] 
    public ActionResult<IEnumerable<Specialty>> GetAllSpecialties_Patient() 
    {
        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        return Ok(_specialtyService.GetAll()); // retorna una lista de especialidades
    }



    [HttpGet("{id}")] 
    [ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult<Appointment> GetAppointmentById(int id){
        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        var appointment = _appointmentService.GetById(id); // busca la cita por id
        
        if (appointment == null){ // verifica si no se encontro la cita
            return NotFound("Appointment not found"); // retorna 404 si no se encuentra
        }
        return Ok(appointment); // retorna la cita encontrada
    }

  
    [HttpPost("appointment")] 
    public ActionResult<Appointment> CreateAppointment([FromBody] AppointmentDTO appointmentDto){

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();
        
        try{

            /* string userName = _accountService.GetUserName();

            if (string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            int? userId = (int)_patientService.GetId(userName);
            if (!userId.HasValue) return BadRequest("No se encontro el Id del Paciente");

            Appointment _appointment = _appointmentService.Create(appointmentDto);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = userId }, _appointment);
        } */

            string userName = _accountService.GetUserName();
            if (string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");
            
            int? userId = (int)_patientService.GetId(userName);
            if (!userId.HasValue) return BadRequest("No se encontro el Id del Paciente");

            appointmentDto.patient_id = (int)userId; //Esto es porque en el patientService es necesario validar si el paciente tiene turnos superpuestos

            if(appointmentDto == null) return BadRequest("Appointment data is required.");
            if(!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

            Appointment _appointment = _appointmentService.Create(appointmentDto);

            if(_appointment == null) return BadRequest("Error creating appointment.");

            return CreatedAtAction(nameof(GetAppointmentById), new { id = _appointment.ID }, _appointment);

        }    
        catch (DbUpdateException ex){
            // Aquí puedes registrar el detalle de la excepción o devolver una respuesta detallada
            throw new Exception("Database update failed: " + ex.InnerException?.Message);
        }
        catch (Exception ex){
            throw new Exception("An error occurred while creating the appointment: " + ex.Message);
        }
    }



    [HttpDelete("appointment/{id}")]
    public IActionResult DeleteAppointment(int id)
    {

        if (!User.IsInRole("patient") && !User.IsInRole("Patient") && !User.IsInRole("PATIENT")) return Forbid();

        var appointment = _appointmentService.GetById(id); // busca la cita por id

        // verifica si no se encontro la cita
        if (appointment == null) return NotFound(); // devuelve 404 si no se encuentra

        _appointmentService.Delete(id); // elimina la cita

        return Ok(new { message = "El turno se eliminó correctamente." }); // retorna 200 con mensaje de exito
    }



































    /* [HttpGet("specialty/{id}")] 
    public ActionResult<Specialty> GetSpecialtyById(int id) 
    {
        var specialty = _specialtyService.GetById(id); // busca la especialidad por id
        if (specialty == null) // verifica si no se encontro la especialidad
            return NotFound("Specialty not found."); // retorna 404 si no se encuentra
        return Ok(specialty); // retorna la especialidad encontrada
    } */



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


