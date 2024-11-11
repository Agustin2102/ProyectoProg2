using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/doctors")] 
public class DoctorController : ControllerBase{
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;
    private readonly AccountDbService _accountService;
    public DoctorController(IDoctorService doctorService, AccountDbService accountService, IPatientService patientService){
        this._doctorService = doctorService;
        this._patientService = patientService;
        this._accountService = accountService;
    }

    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/

    [Authorize(Roles = "doctor,Doctor,DOCTOR")]


    [HttpGet("{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> GetById(int id){ //Obtengo un Doctor por su ID
        //if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();
        
        Doctor? _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found");
        else return Ok(_doctor);
    }


    //Metodo Solo Para El Doctor
    [HttpGet("doctor")]
    //[Authorize(Roles = "Doctor")]
    public ActionResult<Doctor> GetDoctor(){
        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        try{
            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            // Llama al servicio para buscar un doctor por su nombre
            Doctor? doctor = _doctorService.GetByName(userName);

            // Verifica si el doctor existe
            if (doctor == null) return NotFound("Doctor not found");
            var specialties = doctor.Specialties ?? new List<Specialty>();

            var _doctor = new{
                id = doctor.Id,
                name = doctor.Name,
                lastName = doctor.LastName,
                dni = doctor.DNI,
                licenseNumber = doctor.LicenseNumber,
                specialties = specialties.Select(s => new{
                    s.Name,
                }).ToList()// Devuelve una lista vacía si specialties es null
            };

            return Ok(_doctor);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }   


    [HttpPut("doctor")]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> UpdateDoctor(DoctorDTO d){

        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        try{

            string userName = _accountService.GetUserName();
            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            int? userId = _doctorService.GetId(userName);
            if(!userId.HasValue) return BadRequest("No se encontro el Id del Doctor");


            Doctor? doctor = _doctorService.Update((int)userId, d);

            if(doctor is null) return NotFound(new {Message = $"No se pudo actualizar el doctor con id: {userId}"});

            return CreatedAtAction(nameof(GetById), new{id = doctor.Id}, doctor);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);

            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("appointments")]
    //[Authorize(Roles = "admin, doctor")]
    public ActionResult<List<Appointment>> GetAllAppointments(){

        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        try{

            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            // Llama al servicio para buscar un doctor por su nombre
            var doctor = _doctorService.GetByName(userName);


            // Verifica si el doctor existe
            if (doctor == null) return NotFound("Doctor not found");
            // Verifica si el doctor tiene citas
            if (doctor.Appointments == null || !doctor.Appointments.Any()) return NotFound("No appointments found for this doctor.");


            var appointments = doctor.Appointments?.Select(a => new {
                
                //Datos propios del turno
                id = a.ID,
                date = a.appointment_date,
                status = a.status.ToString(),
                specialty = a.Specialty.Name,

                //Modifico los datos que se muestrn del doctor
                Doctor = new {
                    name = a.Doctor.Name,
                    lastName = a.Doctor.LastName,
                    contact = a.Doctor.Email
                },

                //Modifico los datos que se muestran del paciente
                Patient = new {
                    name = a.Patient.Name,
                    lastName = a.Patient.LastName,
                    contact = a.Patient.Email
                }
            }).ToList();

            //if(!appointments.Any()) return NotFound("No appointments found for this doctor.");

            return Ok(appointments);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }
 

    [HttpGet("appointment/{id}")]
    //[Authorize(Roles = "Doctor")]
    public ActionResult<Appointment> GetAppointmentById(int id){

        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        try{
            
            int appointmentId = id;

            //Obtengo el nombre del doctor, que se encuentra logeado
            string userName = _accountService.GetUserName();
            if(string.IsNullOrEmpty(userName)) return BadRequest("Name is required");

            var doctor = _doctorService.GetByName(userName);

            if(doctor == null) return NotFound("Doctor not found");
            // Verifica si el doctor tiene turnos
            if (doctor.Appointments == null || !doctor.Appointments.Any()) return NotFound("No appointments found for this doctor.");


            // Realiza la búsqueda del turno
            var appointment = doctor.Appointments.FirstOrDefault(a => a.ID == appointmentId); //Busco un torno espeficico
                

            // Devuelve un 404 si no se encuentra el turno
            if (appointment == null) return NotFound("The appointment with the specified Id was not found.");
            //if(appointment.Doctor == null) return NotFound("Doctor not found for this Appointment.");
            //if(appointment.Patient == null) return NotFound("Patient not found for this Appointment.");
            //if(appointment.Specialty == null) return NotFound("Specialty not found for this Appointment.");

            //Mapeo los datos para devolver solo el nombre y contacto del doctor y el paciente
            var responseAppointment = new {
                //Datos propios del turno
                id = appointment.ID,
                date = appointment.appointment_date,
                status = appointment.status.ToString(),
                specialty = appointment.Specialty.Name,

                //Modifico los datos que se muestrn del doctor
                Doctor = new {
                    name = doctor.Name,
                    lastName = doctor.LastName,
                    contact = doctor.Email
                },

                //Modifico los datos que se muestran del paciente
                Patient = new {
                    name = appointment.Patient.Name,
                    lastName = appointment.Patient.LastName,
                    contact = appointment.Patient.Email
                }
            };

            // Si se encuentra el turno, devuélvelo con un 200 OK
            return Ok(responseAppointment);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);

            // Si ocurre un error, devuelve un mensaje con el error y un código de estado 500
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("patients")]
    //[Authorize(Roles = "admin, doctor")]
    public ActionResult<List<Appointment>> GetAllPatients(){

        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        try{

            string userName = _accountService.GetUserName();

            if(string.IsNullOrEmpty(userName)) return BadRequest("Could not access user's Claims");

            // Llama al servicio para buscar un doctor por su nombre
            var doctor = _doctorService.GetByName(userName);


            // Verifica si el doctor existe
            if (doctor == null) return NotFound("Doctor not found");
            // Verifica si el doctor tiene citas
            if (doctor.Appointments == null || !doctor.Appointments.Any()) return NotFound("No appointments found for this doctor.");

            //.DistinctBy(p => p.Id) lo que hace filtrar los pacientes de forma que no se repitan
            var patientsInfo = doctor.Appointments.Select(a => a.Patient).DistinctBy(p => p.Id).Select(p => new {
                id = p.Id,
                name = p.Name,
                lastName = p.LastName,
                email = p.Email,
                telephone = p.TelephoneNumber,
                address = p.Address
            }).ToList();

            return Ok(patientsInfo);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("patient/{id}")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult<Patient> GetPatientById(int id){

        if (!User.IsInRole("doctor") && !User.IsInRole("Doctor") && !User.IsInRole("DOCTOR")) return Forbid();

        Patient? patient = _patientService.GetById(id);

        if (patient == null) return NotFound("Pacient not found");

        //Como no quiero que se agregen los turnos del usuario -> modifico los datos que se envian
        var _patientInfo = new {
            id = patient.Id,
            name = patient.Name,
            lastName = patient.LastName,
            dateOfBirth = patient.DateOfBirth,
            email = patient.Email,
            telephone = patient.TelephoneNumber,
            address = patient.Address,
            medicalHistory = patient.MedicalHistory
        };

        return Ok(_patientInfo);
        
    }







    /* Explicación.
        El mapeo de datos se refiere al proceso de transformar o seleccionar ciertos datos de una estructura (como una base de datos)
            a otra (como un DTO o un objeto anónimo que se enviará como respuesta a una API). Esto se hace comúnmente para reducir la cantidad
        de datos que se envían en la respuesta y para controlar qué información se expone a los consumidores de la API.

        ¿Por qué es posible hacer: name = appointment.Doctor.Name?
        Acceso a Propiedades de Objetos:

        En C#, puedes acceder a las propiedades de un objeto mediante la notación de punto. Por ejemplo, si appointment es un objeto que tiene una propiedad Doctor, 
        que a su vez es otro objeto con propiedades como Name y Email, puedes acceder a estas propiedades en cadena.
        La expresión appointment.Doctor.Name significa "accede a la propiedad Doctor del objeto appointment y luego accede a la propiedad Name del objeto Doctor".
                
    */





    /* [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult<List<Doctor>> GetAllDoctors(){ // Obtiene todos los Doctores de la BD
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
    } */


    //[Authorize(Roles = "admin")]
    /* [HttpGet("name")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetByName()
    {
        string nameUser = _accountService.GetUserName();
        try{

            if(string.IsNullOrEmpty(nameUser)){
                return BadRequest("Name is required");
            }

            // Llama al servicio para buscar un doctor por su nombre
            var doctor = _doctorService.GetByName(nameUser);


            // Verifica si el doctor existe
            if (doctor == null) return NotFound("Doctor not found");

            // Devuelve el doctor encontrado con un 200 OK
            return Ok(doctor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    } */
    

    /* [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> Create([FromBody] DoctorDTO doctor){
        if(doctor == null) return BadRequest("Doctor data is required.");
        if(!ModelState.IsValid) return BadRequest(ModelState); // retorna 400 si hay errores en el modelo

        Doctor _doctor = _doctorService.Create(doctor); // Creo al nuevo doctor

        if(_doctor == null) return BadRequest("Error creating doctor.");

        return CreatedAtAction(nameof(GetById), new {id = _doctor.Id}, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    } */


    /* [HttpDelete("{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = "admin")]
    public ActionResult Delete(int id){
        var _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found!!!");

        _doctorService.Delete(id);
        return NoContent();
    } */


    

}

