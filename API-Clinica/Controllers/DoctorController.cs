using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase{
    private readonly IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService){
        this._doctorService = doctorService;
    }

    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/
 
    [HttpGet]
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
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> GetById(int id){ //Obtengo un Doctor por su ID
        Doctor? _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found");
        else return Ok(_doctor);
    }

    [HttpPost]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> Create(DoctorDTO doctor){
        Doctor _doctor = _doctorService.Create(doctor); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        return CreatedAtAction(nameof(GetById), new {id = _doctor.Id}, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult Delete(int id){
        var _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found!!!");

        _doctorService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "admin")]
    public ActionResult<Doctor> UpdateDoctor(int id, DoctorDTO d){
        try{
            Doctor? doctor = _doctorService.Update(id, d);
            if(doctor is null) return NotFound(new {Message = $"No se pudo actualizar el doctor con id: {id}"});
            return CreatedAtAction(nameof(GetById), new{id = doctor.Id}, doctor);
        }catch (System.Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("{doctorId}/appointments")]
    //[Authorize(Roles = "admin, doctor")]
    public ActionResult<List<Appointment>> GetAllAppointmentsForDoctor(int doctorId){
        try{
            var appointments = _doctorService.GetAllAppointments(doctorId).Select(a => new {
                
                //Datos propios del turno
                id = a.ID,
                date = a.appointment_date,
                status = a.status.ToString(),

                //Modifico los datos que se muestrn del doctor
                Doctor = new {
                    name = a.Doctor.Name,
                    lastName = a.Doctor.LastName,
                    contact = a.Doctor.Email
                },
                specialty = a.Doctor.Name,

                //Modifico los datos que se muestran del paciente
                Patient = new {
                    name = a.Patient.Name,
                    lastName = a.Patient.LastName,
                    contact = a.Patient.Email
                }
            }).ToList();

            if(!appointments.Any()) return NotFound("No appointments found for this doctor.");

            return Ok(appointments);
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }

    [HttpGet("{doctorID}/appointments/{appointmentID}")]
    public ActionResult<Appointment> GetAppointmentForDoctor(int doctorID, int appointmentID){
        try{
            //Busco en la BD el turno que coincida con el id del doctor y el id del turno 
            var appointment = _doctorService.GetAppointment(doctorID, appointmentID);
            
            // Si el turno no se encuentra, devuelve un 404
            if(appointment == null) return NotFound("Appointment not found for this doctor.");
            if(appointment.Doctor == null) return NotFound("Doctor not found for this Appointment.");
            if(appointment.Patient == null) return NotFound("Patient not found for this Appointment.");
            if(appointment.Specialty == null) return NotFound("Specialty not found for this Appointment.");

            //Mapeo los datos para devolver solo el nombre y contacto del doctor y el paciente
            var responseAppointment = new {
                //Datos propios del turno
                appointment.ID,
                appointment.appointment_date,
                status = appointment.status.ToString(),

                //Modifico los datos que se muestrn del doctor
                Doctor = new {
                    name = appointment.Doctor.Name,
                    lastName = appointment.Doctor.LastName,
                    contact = appointment.Doctor.Email
                },
                specialty = appointment.Specialty.Name,

                //Modifico los datos que se muestran del paciente
                Patient = new {
                    name = appointment.Patient.Name,
                    lastName = appointment.Patient.LastName,
                    contact = appointment.Patient.Email
                }
            };

            // Si se encuentra el turno, devuélvelo con un 200 OK
            return Ok(responseAppointment);

        } catch(Exception e){
            Console.WriteLine(e.Message);

            // Si ocurre un error, devuelve un mensaje con el error y un código de estado 500
            return Problem(detail: e.Message, statusCode: 500);
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
        
    }

}




/*

Para implementar la autenticación y autorización en GetAppointmentForDoctor y controlar el acceso basado en roles, el método se puede modificar de la siguiente forma:

Atributo de autorización: Se utiliza el atributo [Authorize] para restringir el acceso solo a los usuarios con roles específicos.
Uso de User.Claims: Extraemos el doctorID del token de autenticación, de modo que el doctorID que intenta acceder a un turno específico solo pueda ver sus propios turnos (en el caso de roles de "doctor").
Aquí tienes cómo quedaría la implementación:


[HttpGet("{doctorID}/appointments/{appointmentID}")]
[Authorize(Roles = "admin, doctor")]
public ActionResult<Appointment> GetAppointmentForDoctor(int doctorID, int appointmentID)
{
    try
    {
        // Obtiene el ID del usuario autenticado si el rol es "doctor"
        if (User.IsInRole("doctor"))
        {
            // Obtén el ID del doctor desde los claims en el token de autenticación
            int authenticatedDoctorID = int.Parse(User.FindFirst("doctorID").Value);
            
            // Verifica si el doctor autenticado está intentando acceder a su propio turno
            if (authenticatedDoctorID != doctorID)
            {
                return Forbid("Access denied to this appointment.");
            }
        }

        // Realiza la búsqueda del turno
        var appointment = _doctorService.GetAppointment(doctorID, appointmentID);

        // Devuelve un 404 si no se encuentra el turno
        if (appointment == null)
        {
            return NotFound("Appointment not found for this doctor.");
        }

        // Si el turno se encuentra, devuelve los detalles del turno
        return Ok(appointment);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
    }
}



Explicación de la implementación:
Atributo [Authorize(Roles = "admin, doctor")]: Restringe el acceso a usuarios con roles de "admin" o "doctor".
Verificación de acceso:
Si el usuario tiene el rol de "doctor", se extrae su doctorID del token mediante User.FindFirst("doctorID").
Se verifica si el doctorID autenticado coincide con el doctorID solicitado. Si no coincide, se devuelve un Forbid, bloqueando el acceso a turnos que no pertenecen al doctor autenticado.
Acceso de administradores: El rol "admin" pasa sin restricciones adicionales.
Esto permite que los doctores solo accedan a sus propios turnos, mientras que los administradores tienen acceso completo.

*/